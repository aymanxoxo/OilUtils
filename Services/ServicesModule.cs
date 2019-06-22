using Infrastructure.StringConstants;
using Interfaces.IServices;
using Prism.Ioc;
using Unity;

namespace Services
{
    public class ServicesModule : Prism.Modularity.IModule
    {
        private IUnityContainer _container;

        public ServicesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _container.RegisterType<ITwoDimensionsMeshService, TwoDimensionsMeshService>();
            _container.RegisterType<IThreeDimensionsMeshService, ThreeDimensionsMeshService>();
            _container.RegisterType<ILayerReaderService, IntervalReaderService>(UnityServiceNames.IntervalReaderService);
            _container.RegisterType<ILayerReaderService, RelativePointsReaderService>(UnityServiceNames.RelativePointsReaderService);
        }
    }
}
