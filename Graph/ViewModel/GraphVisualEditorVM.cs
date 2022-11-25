using Graph.Core;
using Graph.View;
using System;
using System.Windows;
using System.Windows.Media;

namespace Graph.ViewModel
{
    internal class GraphVisualEditorVM : ObservableObject
    {
        public string Title => "Graph Visual Editor";
        public bool IsDirty { get; private set; } = default;

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand ReturnCommand { get; private set; }
        public RelayCommand ChangeThemeCommand { get; private set; }

        public GraphVisualEditorVM()
        {
            SaveCommand = new RelayCommand(obj =>
            {
                if (obj is Window dialogWindow)
                {
                    dialogWindow.DialogResult = true;
                }
            });

            ReturnCommand = new RelayCommand(obj =>
            {
                if (obj is Window dialogWindow)
                {
                    dialogWindow.DialogResult = false;
                }
            });

            ChangeThemeCommand = new RelayCommand(obj =>
            {
                if (obj is string rbContent)
                {
                    if (Enum.TryParse(rbContent, out Theme theme))
                    {
                        ThemeValue = theme;
                    }
                    else
                    {
                        throw new ArgumentException($"Темы {rbContent} не существует");
                    }
                }
            });
        }

        internal bool ShowEditorDialog() =>
            (bool)new GraphVisualEditorView() { Owner = Application.Current.MainWindow, DataContext = this }.ShowDialog();

        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _fbColor;
        public SolidColorBrush FbColor
        {
            get => _fbColor;
            set
            {
                _fbColor = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _bgColor;
        public SolidColorBrush BgColor
        {
            get => _bgColor;
            set
            {
                _bgColor = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        private SolidColorBrush _fillColor;
        public SolidColorBrush FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                IsDirty = true;
                OnPropertyChanged();
            }
        }

        private Theme _themeValue = Theme.Customized;
        public Theme ThemeValue
        {
            get => _themeValue;
            set
            {
                _themeValue = value;
                IsDirty = true;
            }
        }

        public enum Theme
        {
            Light,
            Dark,
            Customized
        }
    }
}