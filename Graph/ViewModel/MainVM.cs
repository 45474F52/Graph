using Graph.Core;
using Graph.Model.Messages;
using System;
using System.Windows;
using System.Windows.Data;

namespace Graph.ViewModel
{
    internal class MainVM : ObservableObject
    {
		private object _currentView;
		public object CurrentView
		{
			get => _currentView;
			set
			{
				_currentView = value;
				OnPropertyChanged();
			}
		}

		public RelayCommand ToHome { get; private set; }
		public RelayCommand ToGraph1 { get; private set; }

		public HomeVM HomeVM { get; private set; }
		public Graph1VM Graph1VM { get; private set; }

		public MainVM()
		{
			App.Messenger.Subscribe<CurrentViewMessage>(this, (obj) =>
			{
				if (obj is CurrentViewMessage message)
				{
                    if (Enum.TryParse(message.CurrentView, out ViewModels viewModel))
                    {
                        switch (viewModel)
                        {
                            case ViewModels.Graph1VM:
								ToGraph1.Execute(null);
                                break;
                            case ViewModels.Graph2VM:
                                break;
                            case ViewModels.Graph3VM:
                                break;
                            default:
                                MessageBox.Show($"ViewModel с названием {message.CurrentView} не существует");
								break;
                        }
                    }
					else
					{
						MessageBox.Show("Не получилось найти ViewModel с таким именем");
					}
                }
			});

			HomeVM = new HomeVM();
			Graph1VM = new Graph1VM();

			CurrentView = HomeVM;

			ToHome = new RelayCommand(obj =>
			{
				CurrentView = HomeVM;
			});

			ToGraph1 = new RelayCommand(obj =>
			{
				CurrentView = Graph1VM;
			});
		}

		~MainVM()
		{
			App.Messenger.Unsubscribe<CurrentViewMessage>(this);
		}

		private enum ViewModels
		{
			Graph1VM,
			Graph2VM,
			Graph3VM
		}
	}
}