using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Cooked_App;

public partial class EinkaufsListePage : ContentPage
{
    private const string EinkaufslisteKey = "EinkaufsListe";
    public ObservableCollection<string> Items { get; set; }

    public EinkaufsListePage()
    {
        InitializeComponent();
        Items = new ObservableCollection<string>();
        LoadItems();
        BindingContext = this; // Damit die Bindings funktionieren
    }

    // Laden der Einkaufsliste aus den Preferences
    private void LoadItems()
    {
        var savedItems = Preferences.Get(EinkaufslisteKey, string.Empty);
        var items = string.IsNullOrEmpty(savedItems) ? new List<string>() : savedItems.Split(',').ToList();
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    // Artikel zur Einkaufsliste hinzufügen
    private void OnAddItemClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ItemEntry.Text))
        {
            Items.Add(ItemEntry.Text);
            SaveItems();
            ItemEntry.Text = string.Empty; // Eingabefeld zurücksetzen
        }
    }

    // Artikel aus der Einkaufsliste löschen
    public Command<string> DeleteItemCommand => new Command<string>((item) =>
    {
        Items.Remove(item);
        SaveItems(); // Aktualisiert die gespeicherten Items
    });

    // Einkaufsliste in den Preferences speichern
    private void SaveItems()
    {
        var savedItems = string.Join(",", Items);
        Preferences.Set(EinkaufslisteKey, savedItems);
    }
}
