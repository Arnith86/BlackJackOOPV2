using Avalonia.Controls;

namespace BlackJackV2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Width = 1024;
			this.Height = 768;
            this.MinHeight = 800;
			this.MinWidth = 600;
		}
    }
}