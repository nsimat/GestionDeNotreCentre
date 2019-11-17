using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeNotreCentre.App.Utility
{
    public sealed class MyMessenger<TMessage> where TMessage : class
    {
        #region Pattern Singleton
        private static MyMessenger<TMessage> _instance;

        public static MyMessenger<TMessage> Instance
        {
            get
            {
                return _instance ?? (_instance = new MyMessenger<TMessage>()); ;
            }
        }

        private MyMessenger()
        {
            _boxes = new Dictionary<string, Action<TMessage>>();
        }
        #endregion

        private Action<TMessage> _broadcast;
        private Dictionary<string, Action<TMessage>> _boxes;

        public void Register(Action<TMessage> method)
        {
            _broadcast += method;
        }

        public void Register(string box, Action<TMessage> method)
        {
            if (!_boxes.ContainsKey(box))
                _boxes.Add(box, null);

            _boxes[box] += method;
        }

        public void Send(TMessage message)
        {
            _broadcast?.Invoke(message);
        }

        public void Send(string box, TMessage message)
        {
            if (_boxes.ContainsKey(box))
                _boxes[box]?.Invoke(message);
        }
    }
}
