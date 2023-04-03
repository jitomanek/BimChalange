using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bim.Util
{
    public static class ModelStateExtension
    {
        public static KeyValuePair<string, string[]>[] GetStateErrors(this ModelStateDictionary modelState)
        {
            return modelState
                    .Select(x => new KeyValuePair<string, string[]>(
                        x.Key,
                        x.Value?.Errors?.Select(q => q.ErrorMessage).ToArray() ?? new string[] { })
                    ).ToArray();
        }
    }
}