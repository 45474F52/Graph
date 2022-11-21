using Graph.Core;
using System.Windows;

namespace Graph
{
    public partial class App : Application
    {
        private static IMessenger _messenger;
        internal static IMessenger Messenger
        {
            get
            {
                if (_messenger == null)
                {
                    _messenger = new Messenger();
                }
                return _messenger;
            }
        }
    }
}