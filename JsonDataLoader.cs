namespace Cooked_App
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;

    public static class JsonDataLoader
    {
        public static async Task<List<Recipe>> LoadRecipesAsync()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Cooked_App.Resources.recipes.json";

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<List<Recipe>>(json);
        }
    }
}
