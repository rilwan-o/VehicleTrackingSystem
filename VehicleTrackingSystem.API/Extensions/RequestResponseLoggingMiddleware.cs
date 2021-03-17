using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.Extensions
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            // Read and log request body data
            string requestBodyPayload = await ReadRequestBody(context.Request);

            //check for password and ommit
            var ignoreGroups = _appSettings.DatabaseSecureLogGroups;
            var grpSplit = ignoreGroups.Split("|");
            foreach (var item in grpSplit)
            {
                var bodyParameters = requestBodyPayload.ToLower();
                if (bodyParameters.Contains(item)) {
                requestBodyPayload = "";
                }
             }

            LogHelper.RequestPayload = requestBodyPayload;

            // Read and log response body data
            // Copy a pointer to the original response body stream
            var originalResponseBodyStream = context.Response.Body;

            // Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                // ...and use that for the temporary response body
                context.Response.Body = responseBody;

                // Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                // Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalResponseBodyStream);
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            HttpRequestRewindExtensions.EnableBuffering(request);
            // hiddenString = number.Substring(number.Length - 4).PadLeft(number.Length, '*');
            var body = request.Body;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            string requestBody = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return $"{requestBody}";
        }
    }
}
