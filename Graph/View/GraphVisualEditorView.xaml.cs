using System.Windows;
using System.Windows.Input;

namespace Graph.View
{
    public partial class GraphVisualEditorView : Window
    {
        public GraphVisualEditorView()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}