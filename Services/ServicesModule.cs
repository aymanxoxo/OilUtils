using Interfaces.IFactories;
using Interfaces.IServices;
using Models;
using Prism.Ioc;
using Services.Factories;
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
            _container.RegisterType<IReaderFactory<RelativePointsReaderSettings>, RelativePointsReaderServiceFactory>();
            _container.RegisterType<IReaderFactory<IntervalReaderSettings>, IntervalReaderServiceFactory>();
            _container.RegisterType<IReaderFactory<FileReaderSettings>, FileReaderServiceFactory>();
        }
    }
}
