﻿using System;
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

namespace GlobalChatClient.user_control
{
    /// <summary>
    /// Interaction logic for Server_Label.xaml
    /// </summary>
    public partial class Server_Label : UserControl
    {
        public Server_Label(string msg)
        {
            InitializeComponent();
            lbl_txt.Text = msg;
        }
    }
}
