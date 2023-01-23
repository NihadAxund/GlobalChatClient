﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using GlobalChatClient.user_control;

namespace GlobalChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(Start);
        }
        private void SendMessage(string text)
        {
            var writer = Task.Run(() =>
            {
                
                var bytes = Encoding.ASCII.GetBytes(text);
                socket.Send(bytes);
            });
            Client_Label cl = new Client_Label(text); cl.HorizontalAlignment= HorizontalAlignment.Right;
            cl.Margin= new Thickness(0,0,10,0); Chat_list.Children.Add(cl);

        }
        private void AddList(string msg)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                Server_Label sl = new Server_Label(msg);
                sl.HorizontalAlignment = HorizontalAlignment.Left; sl.Margin = new Thickness(10, 0, 0, 0);
                Chat_list.Children.Add(sl);
            }));
        }
        private void Start()
        {
            var IPAddres = IPAddress.Loopback;
            var port = 27009;
            var op = new IPEndPoint(IPAddres, port);
            try
            {
                socket.Connect(op);
                if (socket.Connected)
                {
                    var reader = Task.Run(() =>
                    {
                        var length = 0;
                        var bytes = new byte[1024];
                        while (true)
                        {
                            length = socket.Receive(bytes);
                            var msg = Encoding.UTF8.GetString(bytes, 0, length);
                            AddList(msg);
                        }
                    });

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Can not connct to the server >>>");
                Close();
            }
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = TXT_BOX.Text;
            if (str.Length > 0 && str != " ")
                SendMessage(str);
            else return;
        }
    }
}
