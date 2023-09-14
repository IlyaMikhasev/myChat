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
        public buttonUser(string name,string isOnline) {
            textName = new TextBlock();
            textName.Text = name;
            textIsOnline = new TextBlock();
            textIsOnline.Text = isOnline;
            DockPanel dockPanel = new DockPanel();
            dockPanel.RenderSize = this.RenderSize;
            dockPanel.Children.Add(textName);
            dockPanel.Children.Add(textIsOnline);
            textIsOnline.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            textName.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.AddChild(dockPanel);
        }

    }
}
