using Graph.Core;
using System.Windows;

namespace Graph
{
    public partial class App : Application
    {
        private static IMessenger _messenger;
        internal static IMessenger Messenger => _messenger ?? (_messenger = new Messenger());
    }
}