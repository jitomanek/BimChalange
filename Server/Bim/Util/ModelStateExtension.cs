using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bim.Util
{
    public static class ModelStateExtension
    {
        public static IDictionary<string, string[]> GetStateErrors(this ModelStateDictionary modelState)
        {
            return modelState
                    .ToDictionary(x => x.Key,x=> x.Value?.Errors?.Select(q => q.ErrorMessage).ToArray() ?? new string[] { });
        }
    }
}