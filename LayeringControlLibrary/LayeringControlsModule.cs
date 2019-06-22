using Infrastructure.StringConstants;
using LayeringControlLibrary.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LayeringControlLibrary
{
    public class LayeringControlsModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public LayeringControlsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsViewRegion, typeof(SettingsView));
        }
    }
}
