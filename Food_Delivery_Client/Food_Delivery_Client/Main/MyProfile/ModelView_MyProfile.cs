using Food_Delivery_Client.Authorization;
using Food_delivery_library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food_Delivery_Client.Main.MyProfile
{
   public class ModelView_MyProfile : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; }
        private User_Repository user_Repository = new User_Repository();

        
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        #region init
        public ModelView_MyProfile() { InitializeComponent(); }

        public async void InitializeComponent()
        {
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Users != null)
                    Users.Clear();
                Users = new ObservableCollection<User>(user_Repository.GetColl());
                OnPropertyChanged("Users");
            }));     
            
        }
        #endregion


        private User edidUser = ModelView_Authorization.current_user;

        public User EdidUser
        {
            get { return edidUser; }
            set { edidUser = value; OnPropertyChanged("EdidUser"); }
        }



        private Command save;

        public Command Save
        {
            get
            {
                return save ?? (save = new Command(async (act) =>
                {
                    ActivityIndicator activityIndicator = new ActivityIndicator { Color = Color.Blue };
                    var win = (act as View_MyProfile);
                    if(EdidUser.User_Name ==null|| EdidUser.User_Name =="")
                    { win.DisplayAlert("Не все поля заполнены", "Ошибка", "Ок"); return; }
                    if (EdidUser.User_Surname == null || EdidUser.User_Surname == "")
                    { win.DisplayAlert("Не все поля заполнены", "Ошибка", "Ок"); return; }
                    if (EdidUser.User_Phone == null || EdidUser.User_Phone == "")
                    { win.DisplayAlert("Не все поля заполнены", "Ошибка", "Ок"); return; }
                    await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
                    {
                        try
                        {

                            user_Repository.Update(EdidUser);
                            win.DisplayAlert("Успех", "Информация обновлена", "ОK");
                        }
                        catch (Exception)
                        {
                            win.DisplayAlert("Что-то пошло не так\nпопробуйте позже", "Ошибка", "Ок"); return;
                        }
                    }));
                    activityIndicator.IsRunning = true;

                }));
            }
        }
    }
}
