using LiveCharts;
using LiveCharts.Defaults;

namespace Graph.Model
{
    internal static class SetDefaultGraph
    {
        internal static void InitializeSeries(ref SeriesCollection series, ref LineSeriesModel line, ref ChartValues<ObservablePoint> Points)
        {
            Points = new ChartValues<ObservablePoint>
            {
                new ObservablePoint(-3, 9),
                new ObservablePoint(-2, 4),
                new ObservablePoint(-1, 1),
                new ObservablePoint(0, 0),
                new ObservablePoint(1, 1),
                new ObservablePoint(2, 4),
                new ObservablePoint(3, 9)
            };

            line = new LineSeriesModel
            {
                Values = Points,
                Title = "Name"
            };

            series = new SeriesCollection() { line };
        }
    }
}