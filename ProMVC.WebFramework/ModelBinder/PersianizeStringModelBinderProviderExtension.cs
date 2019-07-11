using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Linq;

namespace ProMVC.WebFramework.ModelBinder
{
    public static class PersianizeStringModelBinderProviderExtension
    {
        public static MvcOptions UsePersianizeStringModelBinder(this MvcOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var simpleTypeModelBinder = options.ModelBinderProviders.FirstOrDefault(x => x.GetType() == typeof(SimpleTypeModelBinderProvider));
            if (simpleTypeModelBinder == null)
            {
                return options;
            }

            var simpleTypeModelBinderIndex = options.ModelBinderProviders.IndexOf(simpleTypeModelBinder);
            options.ModelBinderProviders.Insert(simpleTypeModelBinderIndex, new PersianizeStringModelBinderProvider());
            return options;
        }
    }
}
