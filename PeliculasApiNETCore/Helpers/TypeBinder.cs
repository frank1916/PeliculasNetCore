using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApiNETCore.Helpers
{
    public class TypeBinder <T>: IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var provedorValores = bindingContext.ValueProvider.GetValue(nombrePropiedad);
            if (provedorValores == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var deserializado = JsonConvert.DeserializeObject<T>(provedorValores.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializado);
            } catch
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "valor no válido para tipo List<int>");
            }
            return Task.CompletedTask;
        }

    }
}
