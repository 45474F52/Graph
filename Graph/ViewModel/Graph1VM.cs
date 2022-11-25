using Graph.Core;
using Graph.Model;
using Graph.Model.MathExpression;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Graph.ViewModel
{
    internal class Graph1VM : ObservableObject
    {
        public Graph1VM()
        {
            Graph = new GraphModel();
            Editor = new GraphVisualEditorVM();
            SetDefaultGraph.InitializeSeries(ref _series, ref _line, ref _points);
            OnPropertyChanged(nameof(Series));

            EditGraphVisual = new RelayCommand(EditorInteractionsLogic);

            Analysis = new RelayCommand(obj =>
            {
                AnalysisVisible = !_analysisVisible;
                AnalysisName = _analysisVisible ? "Graph" : "Analysis";
            });

            RefreshGraph = new RelayCommand(obj =>
            {
                if (obj is CartesianChart chart)
                {
                    chart.Update(true);
                }
            });

            Run = new RelayCommand(CreatingAndDisplayingGraph);

            Clear = new RelayCommand(obj =>
            {
                AValue = null;
                BValue = null;
                CValue = null;

                Points?.Clear();
                FunctionAnalysis?.Clear();
            });

            _pattern = @"[-+]?\d*[Xx]\^[-+]?\d+[-+]?\d*[Xx][-+]\d+";
        }

        private LineSeriesModel _line;

        private ChartValues<ObservablePoint> _points;
        private ChartValues<ObservablePoint> Points { get => _points; set => _points = value; }

        private GraphVisualEditorVM Editor { get; set; }
        private readonly string _pattern;

        public string Title => "Graph_1";

        public GraphModel Graph { get; private set; }

        public RelayCommand EditGraphVisual { get; private set; }
        public RelayCommand Analysis { get; private set; }
        public RelayCommand RefreshGraph { get; private set; }
        public RelayCommand Clear { get; private set; }
        public RelayCommand Run { get; private set; }

        private FunctionsEnum.FunctionType _functionType;

        private FunctionAnalysisModel _functionalAnalysis;
        public FunctionAnalysisModel FunctionAnalysis
        {
            get => _functionalAnalysis;
            set
            {
                _functionalAnalysis = value;
                OnPropertyChanged();
            }
        }

        private SeriesCollection _series;
        public SeriesCollection Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyChanged();
            }
        }

        private bool _analysisVisible = default;
        public bool AnalysisVisible
        {
            get => _analysisVisible;
            set
            {
                _analysisVisible = value;
                OnPropertyChanged();
            }
        }

        private string _analysisName;
        public string AnalysisName
        {
            get => _analysisName;
            set
            {
                _analysisName = value;
                OnPropertyChanged();
            }
        }

        private string _expr;
        public string Expr
        {
            get => _expr;
            set
            {
                _expr = value;
                OnPropertyChanged();
                //value = Regex.Replace(value, @"\s+", string.Empty);

                //if (Regex.IsMatch(value, _pattern, RegexOptions.IgnoreCase))
                //{
                //    _expr = value;
                //    OnPropertyChanged();
                //    _finishedExpr = value;
                //}
            }
        }

        private bool _aVisible;
        public bool AVisible
        {
            get => _aVisible;
            set
            {
                _aVisible = value;
                OnPropertyChanged();
            }
        }

        private double? _aValue;
        public double? AValue
        {
            get => _aValue;
            set
            {
                _aValue = value;
                OnPropertyChanged();
            }
        }

        private bool _bVisible;
        public bool BVisible
        {
            get => _bVisible;
            set
            {
                _bVisible = value;
                OnPropertyChanged();
            }
        }

        private double? _bValue;
        public double? BValue
        {
            get => _bValue;
            set
            {
                _bValue = value;
                OnPropertyChanged();
            }
        }

        private bool _cVisible;
        public bool CVisible
        {
            get => _cVisible;
            set
            {
                _cVisible = value;
                OnPropertyChanged();
            }
        }

        private double? _cValue;
        public double? CValue
        {
            get => _cValue;
            set
            {
                _cValue = value;
                OnPropertyChanged();
            }
        }

        private string _selectedExpression;
        private ComboBoxItem _selectedItem;
        public ComboBoxItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value.Content != null)
                {
                    _selectedItem = value;
                    _selectedExpression = value.Content.ToString();
                    SetFieldsVisibility(ref _selectedExpression);
                }
            }
        }

        private void EditorInteractionsLogic(object obj)
        {
            if (Editor.ShowEditorDialog())
            {
                if (Editor.IsDirty)
                {
                    Graph.Header = Editor.Header;

                    if (Editor.ThemeValue == GraphVisualEditorVM.Theme.Customized)
                    {
                        Graph.BgColor = Editor.BgColor;
                        Graph.FgColor = Editor.FbColor;

                        if (Editor.FillColor != null)
                        {
                            _line.SetFillColor(new SolidColorBrush(Editor.FillColor.Color) { Opacity = .4 }, nameof(Series));
                        }
                    }
                    else if (Editor.ThemeValue == GraphVisualEditorVM.Theme.Light)
                    {
                        Graph.BgColor = Brushes.White;
                        Graph.FgColor = Brushes.Black;

                        _line.SetFillColor(new SolidColorBrush(Brushes.LightSkyBlue.Color) { Opacity = .4 }, nameof(Series));
                    }
                    else
                    {
                        Graph.BgColor = new SolidColorBrush(Color.FromArgb(255, 0x22, 0x20, 0x2f));//#22202f
                        Graph.FgColor = Brushes.White;

                        _line.SetFillColor(new SolidColorBrush(Brushes.Red.Color) { Opacity = .4 }, nameof(Series));
                    }
                }
            }
        }

        private void CreatingAndDisplayingGraph(object obj)
        {
            try
            {
                Tuple<(double[], double[]), FunctionAnalysisModel> answer = MathExpressionAnalyzer.SolveFormula(new FunctionAnalysisModel(),
                    _functionType, 7, (double)_aValue, (double)_bValue, (double)_cValue);

                (double[], double[]) XYPoints = answer.Item1;
                FunctionAnalysis = answer.Item2;

                Points = new ChartValues<ObservablePoint>();
                for (int i = 0; i < XYPoints.Item1.Length; i++)
                {
                    Points.Add(new ObservablePoint(XYPoints.Item1[i], XYPoints.Item2[i]));
                }

                _line.Values = Points;
                Series = new SeriesCollection { _line };
            }
            catch (ArgumentException ex)
            {
                ErrorHandler.ShowMessage(ex.Message, ex.ParamName);
            }
            catch (InvalidOperationException)
            {
                ErrorHandler.ShowMessage("Поля аргументов не могут быть пустыми", "Ошибка");
            }
            catch (NotImplementedException)
            {
                ErrorHandler.ShowMessage("Обработка пользовательского уравнения не реализована", "Упс...");
            }
        }

        private void SetFieldsVisibility(ref string expression)
        {
            _functionType = FunctionsEnum.GetFunctionType(ref expression);
            switch (_functionType)
            {
                case FunctionsEnum.FunctionType.Quadratic:
                case FunctionsEnum.FunctionType.Power:
                    AVisible = true;
                    BVisible = true;
                    CVisible = true;
                    break;
                case FunctionsEnum.FunctionType.Custom:
                    AVisible = false;
                    BVisible = false;
                    CVisible = false;
                    break;
                default:
                    AVisible = true;
                    BVisible = true;
                    CVisible = false;
                    break;
            }
        }
    }
}