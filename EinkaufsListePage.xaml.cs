using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Cooked_App;

public partial class EinkaufsListePage : ContentPage
{
    private const string EinkaufslisteKey = "EinkaufsListe";

    public EinkaufsListePage()
    {
        InitializeComponent();
        LoadItems();
    }

    // Laden der Einkaufsliste aus den Preferences
    private void LoadItems()
    {
        var savedItems = Preferences.Get(EinkaufslisteKey, string.Empty);
        var items = string.IsNullOrEmpty(savedItems) ? new List<string>() : savedItems.Split(',').ToList();
        ItemList.ItemsSource = items;
    }

    // Artikel zur Einkaufsliste hinzufügen
    private void OnAddItemClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ItemEntry.Text))
        {
            var items = ItemList.ItemsSource as List<string> ?? new List<string>();
            items.Add(ItemEntry.Text);
            ItemList.ItemsSource = items;
            SaveItems(items);
            ItemEntry.Text = string.Empty; // Eingabefeld zurücksetzen
        }
    }

    // Einkaufsliste in den Preferences speichern
    private void SaveItems(List<string> items)
    {
        var savedItems = string.Join(",", items);
        Preferences.Set(EinkaufslisteKey, savedItems);
    }
}
