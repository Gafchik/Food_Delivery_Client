using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Main.MyProfile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_MyProfile : ContentPage
    {
        public View_MyProfile()
        {
            InitializeComponent();
            BindingContext = new ModelView_MyProfile();
            checkEdit.CheckedChanged += CheckedChanged;
            
        }

        private void CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if((sender as CheckBox).IsChecked)
            {
                foreach (VisualElement el in stack.Children)
                {
                    if (el is Entry)
                        (el as Entry).IsEnabled = true;
                    if (el is Button)
                        (el as Button).IsEnabled = true;
                }
            }
            else
            {
                foreach (VisualElement el in stack.Children)
                {
                    if (el is Entry)
                        (el as Entry).IsEnabled = false;
                    if (el is Button)
                        (el as Button).IsEnabled = false;
                }
            }
            
        }      
    }
}