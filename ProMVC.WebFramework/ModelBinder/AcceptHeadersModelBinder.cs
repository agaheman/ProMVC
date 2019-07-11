using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Linq;

namespace ProMVC.WebFramework.ModelBinder
{
    public class AcceptHeadersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var acceptHeaders = bindingContext.HttpContext.Request.Headers
                .Where(h => h.Key.StartsWith("accept-")).ToList();



           return Task.CompletedTask;
        }
    }
}
