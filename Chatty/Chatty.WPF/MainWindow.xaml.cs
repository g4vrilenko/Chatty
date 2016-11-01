using Chatty.BLL.Contracts;
using Chatty.BLL.Network;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chatty.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IClientManager ClientManager { get; set; }
        public Page Page1 { get; set; }
        public Page Page2 { get; set; }
        public Page LoginPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ClientManager = new ClientManager();
            Page1 = new Page1();
            Page2 = new Page2();
            LoginPage = new LoginPage();
            Application.Current.Properties["ClientManager"] = ClientManager;
            MainFrame.Content = LoginPage;
        }

        

        private void Bnt_ClickP1(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = Page1;
        }

        private void Bnt_ClickP2(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = Page2;
        }
    }
}
