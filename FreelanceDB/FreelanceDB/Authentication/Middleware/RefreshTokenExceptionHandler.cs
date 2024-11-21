using FreelanceDB.Authentication.Abstractions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace FreelanceDB.Authentication.Middleware
{
    public class RefreshTokenExceptionHandler : ExceptionHandlerMiddleware
    {
        private readonly ITokenService tokenService;

        public RefreshTokenExceptionHandler(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<ExceptionHandlerOptions> options, DiagnosticListener diagnosticListener, ITokenService tokenService) : base(next, loggerFactory, options, diagnosticListener)
        {
            this.tokenService = tokenService;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    try
        //    {
        //        await Next(context);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is SecurityTokenExpiredException)
        //        {
        //            // Токен доступа истек. Попробуйте его обновить.
        //            await HandleRefreshTokenAsync(context);
        //        }
        //        else
        //        {
        //            // Передать исключение вниз по конвейеру обработки.
        //            throw;
        //        }
        //    }
        //}

        //private async Task HandleRefreshTokenAsync(HttpContext context)
        //{
        //    // Получите токен обновления из заголовков запроса.
        //    string refreshToken = context.Request.Headers["Authorization"];

        //    // Обновите токен доступа и токен обновления.
        //    var newTokens = await tokenService.RefreshAccessToken(refreshToken);

        //    // Установите новые токены в заголовки ответа.
        //    context.Response.Headers.Add("Authorization", $"Bearer {newTokens.AccessToken}");
        //    context.Response.Headers.Add("RefreshToken", newTokens.RefreshToken);

        //    // Перенаправьте запрос на исходный ресурс.
        //    context.Response.StatusCode = 200;
        //    await Next(context);
        //}

    }
}
