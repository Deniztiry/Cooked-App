namespace Cooked_App;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    // Navigiere zur Einkaufsseite, wenn der Button geklickt wird
    private async void OnEinkaufslisteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EinkaufsListePage());
    }
    // Navigiere zur Datenschutzerklärung, wenn der Button geklickt wird
    private async void OnDatenschutzerklärungClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrivacyPolicyPage());
    }
}
