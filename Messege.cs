using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace myChat
{    
    public class Messege
    {
        public string _userName;
        public string _messege = string.Empty;
        public DateTime _time;
        public Messege(string mess,DateTime time, string userName) {
            _messege = mess;
            _time = time;
            _userName = userName;
        }
    }
    public class MessegeForm : DockPanel
    {
        TextBlock textName;
        TextBlock textTime;
        TextBlock textMesege;
        public MessegeForm(string name, string messege, string time)
        {
            SolidColorBrush myBrush = new SolidColorBrush(Colors.Gray);
            textName = new TextBlock();
            textName.Text = name ;
            textTime = new TextBlock();
            textTime.Text = time;
            textMesege = new TextBlock();
            textMesege.Text = messege;
            this.Children.Add(textTime);
            this.Children.Add(textName);
            this.Children.Add(textMesege);
            textName.SetValue(DockPanel.DockProperty, Dock.Top);
            textTime.SetValue(DockPanel.DockProperty, Dock.Right);
            textMesege.SetValue(DockPanel.DockProperty, Dock.Left);
            this.Background = myBrush;
            this.Margin = new Thickness(0, 3, 0, 0);
        }

    }
}
