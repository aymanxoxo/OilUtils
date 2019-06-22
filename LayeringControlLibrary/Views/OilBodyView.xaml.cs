using LayeringControlLibrary.ViewModels;
using System.Windows.Controls;

namespace LayeringControlLibrary.Views
{
    public partial class OilBodyView : UserControl
    {
        public OilBodyView(OilBodyViewModel viewModel)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = viewModel;
            };
        }
    }
}
