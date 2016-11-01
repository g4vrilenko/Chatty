using Chatty.BLL.CommunicationEntities;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginPage : Page
    {        
        private Window _mainWindow;
        private string _defaultRemoteHost { get; set; }
        private IClientManager _clientManager { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            _mainWindow = Window.GetWindow(this);
            _clientManager = (IClientManager)Application.Current.Properties["ClientManager"];            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //var ip = "127.0.0.1";
            //var port = 8081;
            //var serverManager = new ServerManager();
            //var clientManger = new WPFClientManager(new ClientManager(), Application.Current.Dispatcher);
            //serverManager.StartServer(ip, port);
            //clientManger.ConnecToServer(ip, port);
            //ResponseStatus status = ResponseStatus.Error;
            //clientManger.SendMessage(new BLL.Entities.Message(1, 1, "Hello"), (res) =>
            //{
            //    status = res.Status;
            //    btnLogin.Content = status.ToString();
            //    //Application.Current.Dispatcher.Invoke(() => btnLogin.Content = status.ToString());                
            //});

        }
    }
}
