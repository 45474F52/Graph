using Graph.Core;
using Graph.Model.Messages;

namespace Graph.ViewModel
{
    internal class HomeVM : ObservableObject
    {
        public HomeVM()
        {
            ToGraph1 = new RelayCommand(obj =>
            {
                App.Messenger.Send(new CurrentViewMessage("Graph1VM"));
            });

            ToGraph2 = new RelayCommand(obj =>
            {
                App.Messenger.Send(new CurrentViewMessage("Graph2VM"));
            });

            ToGraph3 = new RelayCommand(obj =>
            {
                App.Messenger.Send(new CurrentViewMessage("Graph3VM"));
            });
        }

        public string Title => "Home";

        public RelayCommand ToGraph1 { get; private set; }
        public RelayCommand ToGraph2 { get; private set; }
        public RelayCommand ToGraph3 { get; private set; }
    }
}