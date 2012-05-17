using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Filip.ChatModels
{
    public class Message
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public FormUrlEncodedContent serializedMessage { get; set; }

        public Message(MessageReceiver m)
        {
            Username = m.Username;
            Text = m.Text;
            Sent = DateTime.Now;
        }

        public Message(string username, string text)
        {
            Username = username;
            Text = text;
            Sent = DateTime.Now;

            var dict = new Dictionary<string, string>();
            dict.Add("Text", text);
            dict.Add("Username", username);
            dict.Add("Sent", DateTime.Now.ToString());

            serializedMessage = new FormUrlEncodedContent(dict);
        }
    }
}
