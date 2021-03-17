﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.IO;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.DTO;

namespace VehicleTrackingSystem.API.Extensions
{
    public static class LogHelper
	{
		public static string RequestPayload = "";

		public static async void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
		{
			var request = httpContext.Request;

			diagnosticContext.Set("RequestBody", RequestPayload);

			string responseBodyPayload = await ReadResponseBody(httpContext.Response);
			diagnosticContext.Set("ResponseBody", responseBodyPayload);
			ApiResponse responseBody = (ApiResponse)JsonConvert.DeserializeObject(responseBodyPayload, typeof(ApiResponse));
			if(responseBody != null)
            {
				diagnosticContext.Set("ResponseCode", responseBody.Code);
				diagnosticContext.Set("Description", responseBody.Description);
			}

			// Set all the common properties available for every request
			diagnosticContext.Set("Host", request.Host);
			diagnosticContext.Set("Protocol", request.Protocol);
			diagnosticContext.Set("Scheme", request.Scheme);

			// Only set it if available. You're not sending sensitive data in a querystring right?!
			if (request.QueryString.HasValue)
			{
				diagnosticContext.Set("QueryString", request.QueryString.Value);
			}

			// Set the content-type of the Response at this point
			diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

			// Retrieve the IEndpointFeature selected for the request
			var endpoint = httpContext.GetEndpoint();
			if (endpoint is object) // endpoint != null
			{
				diagnosticContext.Set("EndpointName", endpoint.DisplayName);
			}
		}

		private static async Task<string> ReadResponseBody(HttpResponse response)
		{
			response.Body.Seek(0, SeekOrigin.Begin);
			string responseBody = await new StreamReader(response.Body).ReadToEndAsync();
			response.Body.Seek(0, SeekOrigin.Begin);

			return $"{responseBody}";
		}
	}
}
