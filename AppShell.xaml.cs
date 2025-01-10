
using System.Diagnostics;

namespace Cooked_App
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            CopyJsonFileToAppDataIfNotExists("recipes.json");
        }

        private static void CopyJsonFileToAppDataIfNotExists(string fileName)
        {
            var targetPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (!File.Exists(targetPath))
            {
                try
                {
                    using var stream = Android.App.Application.Context.Assets.Open(fileName);
                    using var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();

                    File.WriteAllText(targetPath, content);
                    Debug.WriteLine($"File copied to: {targetPath}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error copying file {fileName}: {ex.Message}");
                }
            }
            else
            {
                Debug.WriteLine($"File already exists: {targetPath}");
            }
        }
    }
}
