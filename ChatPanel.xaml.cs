using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace myChat
{
    /// <summary>
    /// Interaction logic for ChatPanel.xaml
    /// </summary>
    public partial class ChatPanel : Window
    {
        User _myProfile;
        DBConnect _dbConnect = new DBConnect("Mychat");
        ListBox list;
        List<User> _users;

        public ChatPanel(User myProfile, List<User> users)
        {
            InitializeComponent();
            this._myProfile = myProfile;
            this._users = users;
            FillUsersStack();
        }
        private void FillUsersStack() {
            stc_Users.Children.Clear();
            foreach (User user in _users)
            {
                string online = string.Empty;
                if (user.isOnline)
                {
                    online = "online";
                }
                else
                    online = "ofline";
                buttonUser button = new buttonUser(user.Name,online);
                stc_Users.Children.Add(button);
            }
        }

        private void btn_status_Click(object sender, RoutedEventArgs e)
        {
            Brush color = this.Background;
            list= new ListBox{Background = color};
            list.Items.Add(new ListBoxItem { Content = "online", Background = Brushes.Green });
            list.Items.Add(new ListBoxItem { Content = "ofline", Background = Brushes.Red });
            MenuStrip.Children.RemoveAt(0);
            MenuStrip.Children.Insert(0,list);
            list.Visibility = Visibility.Visible;
            list.FocusableChanged += List_FocusableChanged;
        }

        private void List_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (list.SelectedItem.ToString() == "online")
            {
                _myProfile.isOnline = true;
            }
            else
                _myProfile.isOnline = false;
        }      

        private void btn_setings_Click(object sender, RoutedEventArgs e)
        {
           _myProfile.Name= tb_username.Text;
            FillUsersStack();
        }

        

    }
}
