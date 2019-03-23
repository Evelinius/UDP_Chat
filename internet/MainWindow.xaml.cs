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
        public MainWindow()
        {
            InitializeComponent();
            Listen();
        }

        public void SendMessage(string text)
        {
            UdpClient Client = new UdpClient();
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            Client.Send(bytes, bytes.Length, "127.0.0.1", 3333);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.Now;
            Chat.Items.Add(time.ToString() + ": " + InputTB.Text);
            SendMessage(InputTB.Text);
            InputTB.Text = "";
        }

        public void Listen()
        {
            UdpClient client = new UdpClient(3333);
            Task t = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        IPEndPoint iPEndPoint = null;
                        byte[] bytes = client.Receive(ref iPEndPoint);
                        Thread.Sleep(2000);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                }
            });

            t.Start();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
