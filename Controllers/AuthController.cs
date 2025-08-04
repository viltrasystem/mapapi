using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Reflection;
using ViltrapportenApi.Errors;
using ViltrapportenApi.Modal;
using ViltrapportenApi.Services;

namespace ViltrapportenApi.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IWcfAuthenticationService _wcfAuthenticationService;
        private readonly ITokenService _tokenService;
        private readonly IDataService _dataService;
        private readonly IStringLocalizer<AuthController> _localizer;

        public AuthController(IWcfAuthenticationService wcfAuthenticationService, ITokenService tokenService, IDataService dataService, IStringLocalizer<AuthController> localizer)
        {
            _wcfAuthenticationService = wcfAuthenticationService;
            _tokenService = tokenService;
            _dataService = dataService;
            _localizer = localizer;
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var authResult = await _wcfAuthenticationService.WcfAuthenticateAsync(model.Username, model.Password, model.IpAddress);

            if (!authResult.IsSuccess)
            {
                return authResult.ErrorStatus switch
                {
                    "InvalidCredentials" => Unauthorized(new ApiResponse<AuthResponseDto>(
                        StatusCodes.Status401Unauthorized,
                        null,
                        authResult.Error)),
                    "TokenGenerationFailed" => StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new ApiResponse<AuthResponseDto>(
                            500,
                            null,
                            authResult.Error)),
                    _ => BadRequest(new ApiResponse<AuthResponseDto>(
                        StatusCodes.Status400BadRequest,
                        null,
                        authResult.Error))
                };
            }

            return Ok(new ApiResponse<AuthResponseDto>(
                StatusCodes.Status200OK,
                new AuthResponseDto
                {
                    UserId = (int)authResult.UserId,
                    IsAdmin = authResult.IsAdmin,
                    DisplayName = authResult.DisplayName,
                    Token = authResult.Token,
                    RefreshToken = authResult.RefreshToken
                },
                _localizer["LoginSuccess"]));
        }

        //[HttpPost(ApiRoutes.Auth.Login)]
        //public async Task<IActionResult> Login([FromBody] LoginDto model)
        //{

        //    var authResult = await _wcfAuthenticationService.WcfAuthenticateAsync(model.Username, model.Password, model.IpAddress);

        //    if (!authResult.IsSuccess)
        //    {
        //        if (authResult.ErrorStatus == "InvalidCredentials")
        //        {
        //            return Unauthorized(new ApiResponse<UserDto>(StatusCodes.Status401Unauthorized, null, _localizer["LoginFailed"]));
        //        }
        //        else if (authResult.ErrorStatus == "TokenGenerationFailed")
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<UserDto>(500, null, authResult.Error));
        //        }
        //    }

        //    return Ok(new UserDto
        //    {
        //        UserId = (int)authResult.UserId,
        //        IsAdmin = authResult.IsAdmin,
        //        DisplayName = authResult.DisplayName,
        //        Token = authResult.Token,
        //        RefreshToken = authResult.RefreshToken
        //    });
        //}

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request)
        {
            var authResult = await _tokenService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResult.IsSuccess)
                return BadRequest(new ApiResponse<AuthResponseDto>(StatusCodes.Status400BadRequest, null, authResult.Message));

            var user = await _dataService.GetDataServiceAsync(request.UserId);
            if (user == null)
                return NotFound(new ApiResponse<AuthResponseDto>(StatusCodes.Status404NotFound, null, _localizer["UserNotFound"]));

            return Ok(new ApiResponse<AuthResponseDto>(StatusCodes.Status200OK, new AuthResponseDto {
               UserId = (int)user.DnnUserId,
               IsAdmin = user.IsAdmin,
               DisplayName = user.DisplayName,
               Token = authResult.Value.Token,
               RefreshToken = authResult.Value.RefreshToken
           },
           _localizer["TokenRefreshSuccess"]));
        }

        [HttpPost(ApiRoutes.Auth.Logout)]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenDto request)
        {
            var authResult = await _tokenService.InvalidateRefreshTokenAsync(request.RefreshToken);

            if (!authResult.IsSuccess)
                return BadRequest(new ApiResponse<bool>(StatusCodes.Status400BadRequest, authResult.IsSuccess, authResult.Message));

            return Ok(new ApiResponse<bool>(StatusCodes.Status200OK, authResult.Value, _localizer["LogoutSuccess"]));
        }
    }
}
