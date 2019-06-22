using LayeringControlLibrary.ViewModels;
using System.Windows.Controls;

namespace LayeringControlLibrary.Views
{
    public partial class AllLayersView : UserControl
    {
        public AllLayersView(AllLayersViewModel viewModel)
        {
            InitializeComponent();
            
            Loaded += (s, e) =>
            {
                DataContext = viewModel;
            };
        }
    }
}
