using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows.Media;

namespace Graph.Model
{
    public class LineSeriesModel : LineSeries, INotifyPropertyChanged
    {
        public void SetFillColor(SolidColorBrush color, string callerMemberName)
        {
            Fill = color;
            OnPropertyChanged(callerMemberName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}