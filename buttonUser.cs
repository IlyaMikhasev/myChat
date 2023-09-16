using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace myChat
{
    public class buttonUser : Button
    {
        TextBlock textName ;
        TextBlock textIsOnline;
        DockPanel dockPanel;
        public buttonUser(string name,string isOnline) {
            textName = new TextBlock();
            textName.Text = name;
            textIsOnline = new TextBlock();
            textIsOnline.Text =  "  "+isOnline;
            dockPanel = new DockPanel();
            dockPanel.RenderSize = this.RenderSize;
            dockPanel.Children.Add(textName);
            textName.SetValue(DockPanel.DockProperty, Dock.Left);// почему то не работает
            dockPanel.Children.Add(textIsOnline);
            textIsOnline.SetValue(DockPanel.DockProperty, Dock.Right);// почему то не работает
            this.Content = dockPanel;
        }

    }
}
