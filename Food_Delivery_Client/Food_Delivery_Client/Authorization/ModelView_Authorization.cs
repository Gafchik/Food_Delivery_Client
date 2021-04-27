using Food_delivery_library;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Food_Delivery_Client.Authorization
{
    public class ModelView_Authorization : INotifyPropertyChanged
    {

        public ObservableCollection<User> Users { get; set; }
        private User_Repository user_Repository = new User_Repository();

        public ObservableCollection<Blocked_user> Blocked_Users { get; set; }
        private Blocked_users_Repository blocked_Users_Repository = new Blocked_users_Repository();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        #region init
        public ModelView_Authorization() { InitializeComponent(); }

        public async void InitializeComponent()
        {
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Users != null)
                    Users.Clear();
                Users = new ObservableCollection<User>(user_Repository.GetColl());
                OnPropertyChanged("Users");
            }));
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Blocked_Users != null)
                    Blocked_Users.Clear();
                Blocked_Users = new ObservableCollection<Blocked_user>(blocked_Users_Repository.GetColl());
                OnPropertyChanged("Blocked_Users");
            }));
        }
        #endregion

        private string temp_phone;
        public string Temp_Phone
        {
            get { return temp_phone; }
            set { temp_phone = value; OnPropertyChanged("temp_login"); }
        }


        private string temp_password;
        public string Temp_password
        {
            get { return temp_password; }
            set { temp_password = value; OnPropertyChanged("temp_password"); }
        }

        /* private RelayCommand sing_in;

         public RelayCommand Sing_In
         {
             get
             {
                 return sing_in ?? (sing_in = new RelayCommand(() =>
                 {

                 }));
             }
         }*/







    }
}