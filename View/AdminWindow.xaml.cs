using FoxVill.ViewModel;
using System.Windows;

namespace FoxVill.View
{
    public partial class AdminWindow : Window
    {
        private readonly AdminViewModel _viewModel;

        public AdminWindow(AdminViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}
