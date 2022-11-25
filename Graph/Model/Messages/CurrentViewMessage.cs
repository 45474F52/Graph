namespace Graph.Model.Messages
{
    internal sealed class CurrentViewMessage
    {
        public CurrentViewMessage(string viewModelName)
        {
            CurrentView = viewModelName;
            App.Messenger.Send(this);
        }

        internal string CurrentView { get; private set; }
    }
}