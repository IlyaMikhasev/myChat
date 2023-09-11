using System;
using System.Collections.Generic;
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

namespace myChat
{
    /// <summary>
    /// Interaction logic for ChatPanel.xaml
    /// </summary>
    public partial class ChatPanel : Window
    {
        User _myProfile;
        public ChatPanel(User myProfile)
        {
            InitializeComponent();
            this._myProfile = myProfile;
        }

        private void btn_status_Click(object sender, RoutedEventArgs e)
        {
            Brush color = this.Background;
            ListBox list= new ListBox{Background = color};
            list.Items.Add(new ListBoxItem { Content = "online", Background = Brushes.Green });
            list.Items.Add(new ListBoxItem { Content = "ofline", Background = Brushes.Red });
            MenuStrip.Children.RemoveAt(0);
            MenuStrip.Children.Insert(0,list);
            list.Visibility = Visibility.Visible;
        }
    }
}
