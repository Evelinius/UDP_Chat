using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace internet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        UdpClient udp;

        private ChatViewModel ChatViewModel => DataContext as ChatViewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChatViewModel();

            using (StreamReader sr = new StreamReader("./messages.json", Encoding.ASCII))
            {
                string Strmessages = sr.ReadToEnd();
                ObservableCollection<Message> messages = JsonConvert.DeserializeObject<ObservableCollection<Message>>(Strmessages);
                ChatViewModel.Messages = messages;
            }
        }

        private async void Listen()
        {
            while (true)
            {
                var message = await udp.ReceiveAsync();
                string decodedmes = Encoding.UTF8.GetString(message.Buffer);
                ChatViewModel.Messages.Add(new Message(decodedmes, false));
                SaveMessages();
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ChatViewModel.MessageToSend != "")
            {
                byte[] CodeMes = Encoding.UTF8.GetBytes(ChatViewModel.MessageToSend);
                await udp.SendAsync(CodeMes, CodeMes.Length, ChatViewModel.IP, ChatViewModel.SendPort);
                ChatViewModel.Messages.Add(new Message(ChatViewModel.MessageToSend, true));
                SaveMessages();
                ChatViewModel.MessageToSend = "";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var context = DataContext as ChatViewModel;
            if(context.RecievePort.ToString() == "")
            {
                MessageBox.Show("Insert Port");
            }
            else
            {
                if(udp == null)
                {
                    udp = new UdpClient(context.RecievePort);
                    Listen();
                }
                else
                {
                    udp = new UdpClient(context.RecievePort);
                }
            }
          
        }

        private void SaveMessages()
        {
            ObservableCollection<Message> messages = ChatViewModel.Messages;
            string serialized = JsonConvert.SerializeObject(messages, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter("./messages.json", false, Encoding.ASCII))
            {
                sw.Write(serialized);
                sw.Close();
            }
        }

        private void DeleteAllButton(object sender, RoutedEventArgs e)
        {
            ChatViewModel.Messages.Clear();
            SaveMessages();
        }
    }
}
