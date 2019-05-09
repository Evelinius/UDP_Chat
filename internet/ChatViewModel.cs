using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Net.Sockets;

namespace internet
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public UdpClient udp;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public int RecievePort { set; get; }

        public int SendPort { set; get; }
        public string IP { set; get; }

        private string messageToSend;
        public string MessageToSend
        {
            set
            {
                messageToSend = value;
                OnPropertyChanged();
            }
            get
            {
                return messageToSend;
            }
        }

        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();

        public ChatViewModel()
        {
            SendPort = 8000;
            RecievePort = 8000;
            IP = "127.0.0.1";
        }
    }
}
