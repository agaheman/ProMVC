using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ProMVC.WebFramework
{
    public static class Extensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }


        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
        public static T ToModel<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }


}
