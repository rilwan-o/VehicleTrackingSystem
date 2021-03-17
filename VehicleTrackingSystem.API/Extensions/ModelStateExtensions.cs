using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using VehicleTrackingSystem.Domain.DTO;

namespace VehicleTrackingSystem.API.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }

        public static ApiResponse GetApiResponse(this ModelStateDictionary dictionary)
        {
            return new ApiResponse() { Code = "57", Description = GetErrorMessages(dictionary).FirstOrDefault() };
        }
    }
}
