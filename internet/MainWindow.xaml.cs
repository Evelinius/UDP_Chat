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
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace internet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        UdpHelper UdpHelper;

        public MainWindow()
        {
            InitializeComponent();
            UdpHelper = new UdpHelper(int.Parse(PortInput.Text), IPInput.Text, int.Parse(ListeningPortInput.Text));
            UdpHelper.OnMessageRecieved += HandleMessageRecieved;
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.Now;
            Chat.Items.Add(time.ToString() + ": " + InputTB.Text);
            UdpHelper.SendMessage(InputTB.Text);
            InputTB.Text = "";
        }

       
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListeningPortInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void HandleMessageRecieved(object sender, EventArgs args)
        {

        }
    }

    public class UdpHelper
    {
        UdpClient Client;
        int port;
        string IP;


        public UdpHelper(int _port, string _IP, int _listeningport)
        {
            port = _port;
            IP = _IP;
            Client = new UdpClient(_listeningport);
            Listen();
        }

        public void SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            Client.Send(bytes, bytes.Length, this.IP, this.port);    
        }

        public event EventHandler OnMessageRecieved;

        private void Listen()
        {
            Task t = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint iPEndPoint = null;
                        byte[] bytes = Client.Receive(ref iPEndPoint);
                        OnMessageRecieved.Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                }
            });

            t.Start();
        }


    }
}
