using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Main.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_History : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public View_History()
        {
            InitializeComponent();
            BindingContext = new ModeView_History() { Navigation = this.Navigation };         
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e) => (BindingContext as ModeView_History).OnItemTapped(sender, e, this);
    }
}
