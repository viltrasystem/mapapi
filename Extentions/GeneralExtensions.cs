﻿using System.Runtime.CompilerServices;

namespace ViltrapportenApi.Extentions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User == null)
            {
                return string.Empty;
            }

            return httpContext.User.Claims.Single(claim => claim.Type == "userId").Value;
        }
    }
}
