using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProMVC.WebFramework.ModelBinder
{
    internal class PersianizeStringModelBinder : IModelBinder
    {
        private readonly IModelBinder _fallbackBinder;
        public PersianizeStringModelBinder(IModelBinder fallbackBinder)
        {
            if (fallbackBinder == null)
            {
                throw new ArgumentNullException(nameof(fallbackBinder));
            }
            _fallbackBinder = fallbackBinder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                var valueAsString = valueProviderResult.FirstValue;
                if (string.IsNullOrWhiteSpace(valueAsString))
                {
                    return _fallbackBinder.BindModelAsync(bindingContext);
                }

                var model = valueAsString.ApplyPersianYeKe();

                bindingContext.Result = ModelBindingResult.Success(model);
                return Task.CompletedTask;
            }

            return _fallbackBinder.BindModelAsync(bindingContext);
        }
    }
}