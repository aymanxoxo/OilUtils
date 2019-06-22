using Infrastructure.StringConstants;
using LayeringControlLibrary.ViewModels;
using LayeringControlLibrary.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace LayeringControlLibrary
{
    public class LayeringControlsModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public LayeringControlsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _container.RegisterType<AllLayersViewModel>();
            _container.RegisterType<OilBodyViewModel>();
            _container.RegisterType<SettingsViewModel>();

            _regionManager.RegisterViewWithRegion(RegionNames.AllLayers, typeof(AllLayersView));
            _regionManager.RegisterViewWithRegion(RegionNames.OilLayer, typeof(OilBodyView));
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsViewRegion, typeof(SettingsView));
        }
    }
}
