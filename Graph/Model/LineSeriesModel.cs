using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Media;

namespace Graph.Model
{
    public class LineSeriesModel : LineSeries, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetFillColor(SolidColorBrush color, string callerMemberName)
        {
            Fill = color;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerMemberName));
        }
    }
}