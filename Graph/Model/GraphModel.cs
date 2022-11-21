using Graph.Core;
using System.Windows.Media;

namespace Graph.Model
{
	internal class GraphModel : ObservableObject
    {
		public GraphModel() { }

		public GraphModel(string header = null, SolidColorBrush background = null, SolidColorBrush foreground = null)
		{
			Header = header;
			BgColor = background;
			FgColor = foreground;
		}

		private string _header;
		public string Header
		{
			get => _header;
			set
			{
				_header = value;
				OnPropertyChanged();
			}
		}

		private SolidColorBrush bgColor;
		public SolidColorBrush BgColor
		{
			get => bgColor;
			set
			{
				bgColor = value;
				OnPropertyChanged();
			}
		}

		private SolidColorBrush _fgColor;
		public SolidColorBrush FgColor
		{
			get => _fgColor;
			set
			{
				_fgColor = value;
				OnPropertyChanged();
			}
		}
	}
}