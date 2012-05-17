using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using Filip.ChatModels;

namespace Filip.ChatBusiness
{
    public class ChatController : ApiController
    {
        public void Post(MessageReceiver simpleMessage)
        {
            MessageArrived(new Message(simpleMessage));
        }

        public delegate void EventHandler(object sender, MessageEventArgs args);
        public static event EventHandler ThrowMessageArrivedEvent = delegate { };

        public void MessageArrived(Message m)
        {
            ThrowMessageArrivedEvent(this, new MessageEventArgs(m));
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(Message m)
        {
            this.Message = m;
        }
        public Message Message;
    }
}
