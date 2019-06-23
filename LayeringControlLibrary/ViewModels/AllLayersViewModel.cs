using Infrastructure;
using Interfaces.Events;
using Interfaces.IServices;
using Models;
using Prism.Events;
using System;
using System.Linq;
using System.Windows.Media.Media3D;

namespace LayeringControlLibrary.ViewModels
{
    public class AllLayersViewModel : BaseViewModel
    {
        private readonly ITwoDimensionsMeshService _meshService;

        private Model3DCollection _geometryGroup = new Model3DCollection();
        public Model3DCollection GeometryGroup
        {
            get
            {
                return _geometryGroup;
            }
            set
            {
                _geometryGroup = value;
                OnPropertyChanged(nameof(GeometryGroup));
            }
        }

        public AllLayersViewModel(ITwoDimensionsMeshService meshService, IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<NewLayerAddedEvent>().Subscribe(NewLayerAdded);
            GeometryGroup.Add(new DirectionalLight
            {
                Direction = new Vector3D(-1, -1, -1)
            });

            _meshService = meshService;
        }

        private void NewLayerAdded(LayerModel layer)
        {
            if (layer == null)
            {
                throw new ArgumentException();
            }

            var geometry = new MeshGeometry3D();

            _meshService.Reset();
            _meshService.Xs = layer.X;
            _meshService.Ys = layer.Y;
            _meshService.Depths = layer.Z;

            if (!_meshService.CanDraw())
            {
                throw new ArgumentException();
            }
            
            geometry.Positions = (Point3DCollection)new Point3DCollectionConverter().ConvertFromString(_meshService.CalculatePositions());
            var triangleIndices = _meshService.CalculateTriangleIndices();

            for(var i = 0; i < triangleIndices.Count(); i++)
            {
                geometry.TriangleIndices.Add(triangleIndices[i]);
            }

            GeometryGroup.Add(new GeometryModel3D
            {
                Geometry = geometry,
                Material = new DiffuseMaterial(layer.Color)
            });

            OnPropertyChanged(nameof(GeometryGroup));
        }
    }
}
