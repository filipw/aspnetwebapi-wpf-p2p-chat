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
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Windows.Threading;
using Filip.ChatModels;

namespace Filip.ChatGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Filip.ChatBusiness.ChatProxy _cp { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ShowMessage(Message m)
        {
            chatArea.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            chatArea.Text += ("[" + m.Sent + "] " + m.Username + ": " + m.Text);
                            chatArea.Text += Environment.NewLine;
                            chatArea.ScrollToEnd();
                        }
                ));
        }

        public void ShowStatus(string txt)
        {
            chatArea.Dispatcher.Invoke(
                DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            MessageBox.Show(txt);
                        }
                ));
        }

        private void userInputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                if (_cp != null)
                {
                    if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(inputText.Text))
                        sendMessage(new Message(userName.Text, inputText.Text));
                    else
                        ShowStatus("Nothing to send!");
                } else {
                    ShowStatus("Chat not started!");
                }
            }
        }

        private void click_sendMessage(object sender, RoutedEventArgs e)
        {
            if (_cp != null)
            {
                if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(inputText.Text))
                    sendMessage(new Message(userName.Text, inputText.Text));
                else
                    ShowStatus("Nothing to send!");
            }
            else
            {
                ShowStatus("Chat not started!");
            }
        }

        private void sendMessage(Message m)
        {
            _cp.SendMessage(m);
            inputText.Clear();
        }

        private void startChat(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxMyPort.Text) && !string.IsNullOrWhiteSpace(textBoxPartnerAddress.Text))
            {
                _cp = new Filip.ChatBusiness.ChatProxy(this.ShowMessage, this.ShowStatus, textBoxMyPort.Text, textBoxPartnerAddress.Text);
                if (_cp.Status)
                {
                    chatArea.Text += ("Ready to chat!");
                    chatArea.Text += Environment.NewLine;
                }
            }
            else
            {
                ShowStatus("Please fill in all the fields!");
            }
        }
    }
}
