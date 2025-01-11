using System.Diagnostics;
using System.IO;

namespace Cooked_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Kopiere das JSON-File und stelle sicher, dass es immer die neueste Version ist
            CopyJsonFileToAppDataIfNotExists("recipes.json");
        }

        // Diese Methode wurde angepasst, um die Datei zu überschreiben oder zu löschen
        private static void CopyJsonFileToAppDataIfNotExists(string fileName)
        {
            var targetPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            try
            {
                // Lösche die alte Datei, wenn sie existiert
                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                    Debug.WriteLine($"Alte Datei gelöscht: {targetPath}");
                }

                // Kopiere die neue Datei aus den Assets
                using var stream = Android.App.Application.Context.Assets.Open(fileName);
                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();

                // Schreibe das neue JSON in das App-Datenverzeichnis
                File.WriteAllText(targetPath, content);
                Debug.WriteLine($"Neue Datei kopiert: {targetPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fehler beim Kopieren der Datei {fileName}: {ex.Message}");
            }
        }
    }
}
