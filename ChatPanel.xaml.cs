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
        List<string> _smileList = new List<string> { "😀", "😎", "🙂", "😑", "🤐", "😫", "😴", "🤩", "😘",
            "🤮","🤑","😛","💀","👿","😇","🥳","💩","😷","😜","👻","😻","🦍","🐘","🐍","🐥","💑","🚗","✈","🎁","💞"};
        List<Messege> _messeges;
        User _myProfile;
        DBConnect _dbConnect = new DBConnect("Mychat");
        ListBox list;
        List<User> _users;
        Messege _messege;
        Window ws;
        public ChatPanel(User myProfile, List<User> users)
        {
            InitializeComponent();
            this._myProfile = myProfile;
            _dbConnect.AddUserID(_myProfile);
            _myProfile.isOnline = true;
            this._users = users;
            this.WindowStartupLocation=WindowStartupLocation.CenterScreen;
            tb_username.Text = _myProfile.Name;
            FillUsersStack();
            FillMessegeStack();
        }
        private void FillMessegeStack() {
            sp_messeges.Items.Clear();
            _messeges = _dbConnect.messegeHistory();
            foreach (var item in _messeges)
            {
                MessegeForm mbut = new MessegeForm(item._userName, item._messege, item._time.ToString("HH.mm.ss"));
                sp_messeges.Items.Add(mbut);
            }
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
            FillUsersStack();
        }      

        private void btn_setings_Click(object sender, RoutedEventArgs e)
        {
           _myProfile.Name= tb_username.Text;
            _dbConnect.updateUserName(_myProfile);
            FillUsersStack();
        }

        private void btn_smile_click(object sender, RoutedEventArgs e) {
           
                smile_win_create();
        }
        private void smile_win_create() {
            ws = new Window();
            ws.Height = 100;
            ws.Width = 60;
            WrapPanel wrapPanel = new WrapPanel();
            ws.Content = wrapPanel;
            foreach (string sm in _smileList) { 
                Button btn = new Button();
                btn.Height = 20;
                btn.Width = 20;
                btn.Content = sm;
                btn.Click += btn_select_smile_click;
                wrapPanel.Children.Add(btn);
            }
            ws.SizeToContent = SizeToContent.Height;
            ws.Topmost = true;
            ws.WindowStartupLocation = WindowStartupLocation.Manual;
            ws.ShowInTaskbar = false;
            ws.ResizeMode=ResizeMode.CanMinimize;
            ws.Top = 500;
            ws.Left = 550;
            ws.Owner = this;
            ws.Show();
            
        }
        private void btn_select_smile_click(object sender, RoutedEventArgs e) {
            tb_messegeText.AppendText(((Button)sender).Content.ToString());
        }

        private void b_messege_Click(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(tb_messegeText.Document.ContentStart, tb_messegeText.Document.ContentEnd);

            string allText = range.Text;
            Messege messege = new Messege(allText,DateTime.Now,_myProfile.Name);
            _dbConnect.messege(allText, _myProfile.Id, 1);
            FillMessegeStack();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
