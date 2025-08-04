using LoginValidatorServiceReference;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Data.SystemModels;
using ViltrapportenApi.Errors;
using ViltrapportenApi.Middleware;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Profiles;
using ViltrapportenApi.Repositories;
using ViltrapportenApi.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ProblemDetails = ViltrapportenApi.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder.WithOrigins("https://test.viltrapporten.no/", "https://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                // Preserve the original casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
builder.Services.AddDbContext<ViltrapportenSystemContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("ViltrapportenSystemConnection"));
},
contextLifetime: ServiceLifetime.Scoped,
optionsLifetime: ServiceLifetime.Scoped);

// Add PostgreSQL DbContext
builder.Services.AddDbContext<ViltrapportenMapContext>(options =>
{
    options.UseNpgsql(config.GetConnectionString("MapConnection"));
}, ServiceLifetime.Scoped);

builder.Services.AddSingleton<ILoggerService, LoggerService>();
builder.Services.AddScoped<ITokenService, TokenService>();
//builder.Services.AddSingleton<LanguageService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IMapDataService, MapDataService>();
builder.Services.AddScoped<ILandService, LandService>();
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddScoped<ILandTeigService, LandTeigService>();
builder.Services.AddScoped<MainRepository>();
//builder.Services.AddScoped<LandRepository>();
builder.Services.AddScoped<ILandRepository, LandRepository>();
builder.Services.AddScoped<IWcfAuthenticationService, WcfAuthenticationService>();


builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

//builder.Services.AddLogging(logging =>
//{
//    logging.AddConsole();
//});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("nb-NO") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    // options.RequestCultureProviders.Add(new QueryStringRequestCultureProvider());
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray()
            );

        // Create ProblemDetails
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Error",
            Detail = "One or more validation errors occurred",
            Extensions = { ["errors"] = errors }
        };

        // Wrap in your standard ApiResponse
        var response = new ApiResponse<ProblemDetails>(
            StatusCodes.Status400BadRequest,
            problemDetails,
            "Validation errors occurred"
        );

        return new BadRequestObjectResult(response);
    };
});


//JwtSettings jwtSettings = new();
//config.Bind(nameof(jwtSettings), jwtSettings);
//builder.Services.AddSingleton(jwtSettings);

// JwtSettings binding with validation
builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration(nameof(JwtSettings))
    .ValidateDataAnnotations()
    .Validate(jwt => !string.IsNullOrWhiteSpace(jwt.Key) &&
                     !string.IsNullOrWhiteSpace(jwt.Issuer) &&
                     !string.IsNullOrWhiteSpace(jwt.Audience),
        "JWT configuration is invalid.");
var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    RequireExpirationTime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtSettings.Issuer,
    ValidAudience = jwtSettings.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
};

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
        // Suppress the default 401 response
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Unauthorized access - please log in."
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Message = "Access is forbidden - insufficient permissions."
                }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            }
        };
    });

// Load the service URL from configuration
var serviceSettings = config.GetSection("ServiceSettings").Get<ServiceSettings>();
if (serviceSettings != null && !string.IsNullOrWhiteSpace(serviceSettings.LoginValidatorEndpoint))
{
    string loginValidatorEndpoint = serviceSettings.LoginValidatorEndpoint;

    // Create the service client using the dynamic endpoint
    var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
    var endpoint = new EndpointAddress(serviceSettings.LoginValidatorEndpoint);
    var loginValidatorClient = new LoginValidatorClient(binding, endpoint);

    // Register the client as a singleton or scoped service if needed
    builder.Services.AddSingleton(loginValidatorClient);
}
else
{
    throw new Exception("ServiceSettings:LoginValidatorEndpoint is not configured.");
}

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
app.UseStaticFiles(); // Serves files from the wwwroot folder
app.UseHttpsRedirection();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors("AllowReactApp");
app.UseRequestLocalization();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


//string secretKey = GenerateRandomSecretKey();
//Console.WriteLine($"Generated Secret Key: {secretKey}");
//static string GenerateRandomSecretKey()
//{
//    const int keyLength = 32; // Choose a length based on  security requirements
//    using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
//    {
//        byte[] randomBytes = new byte[keyLength];
//        rng.GetBytes(randomBytes);
//        return Convert.ToBase64String(randomBytes);
//    }
//}