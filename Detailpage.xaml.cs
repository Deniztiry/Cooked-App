using Microsoft.Maui.Controls;

namespace Cooked_App
{
    public partial class Detailpage : ContentPage
    {
        public Detailpage(Recipe recipe)
        {
            InitializeComponent();
            BindingContext = recipe;  // Rezept als BindingContext setzen
        }
    }
}
