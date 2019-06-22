using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace OilUtils
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return new MainWindow();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }
    }
}
