using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class ModelStateExtension
    {
        public static void AddModelErrorList(this ModelStateDictionary modelStateDictionary, List<string> errors)
        {
            errors.ForEach(x =>
            {
                modelStateDictionary.AddModelError(string.Empty, x);
            });
        }
    }
}
