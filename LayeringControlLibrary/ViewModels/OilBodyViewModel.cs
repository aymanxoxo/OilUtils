using Infrastructure;
using Infrastructure.Extensions;
using Interfaces.Events;
using Interfaces.IServices;
using Models;
using Prism.Events;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace LayeringControlLibrary.ViewModels
{
    public class OilBodyViewModel : BaseViewModel
    {
        private readonly IThreeDimensionsMeshService _meshService;
        private DirectionalLight _light = new DirectionalLight { Direction = new Vector3D(-1, -1, -1)};

        private string _lightDirection = "-1, -1, -1";
        public string LightDirection
        {
            get
            {
                return _lightDirection;
            }
            set
            {
                _lightDirection = value;

                try
                {
                    var directionArr = _lightDirection.ExtractDoubles();
                    if (directionArr.Length == 3)
                    {
                        _light.Direction = new Vector3D(directionArr[0], directionArr[1], directionArr[2]);
                    }
                }
                catch (Exception)
                {
                }

                OnPropertyChanged(nameof(LightDirection));
                OnPropertyChanged(nameof(GeometryGroup));
            }
        }

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

        private double _volume;
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        public OilBodyViewModel(IThreeDimensionsMeshService meshService, IEventAggregator eventAggregator)
        {
            GeometryGroup.Add(_light);

            _meshService = meshService;
            eventAggregator.GetEvent<RequestBodyDrawEvent>().Subscribe(DrawBody);
        }

        private void DrawBody(BodyModel model)
        {
            if (model == null)
            {
                throw new ArgumentException();
            }

            var geometry = new MeshGeometry3D();

            _meshService.Reset();
            _meshService.Xs = model.X;
            _meshService.Ys = model.Y;
            _meshService.Z1s = model.UpperZ;
            _meshService.Z2s = model.LowerZ;

            geometry.Positions = (Point3DCollection)new Point3DCollectionConverter().ConvertFromString(_meshService.CalculatePositions());
            var triangleIndices = _meshService.CalculateTriangleIndices();

            for (var i = 0; i < triangleIndices.Count(); i++)
            {
                geometry.TriangleIndices.Add(triangleIndices[i]);
            }

            GeometryGroup.Add(new GeometryModel3D
            {
                Geometry = geometry,
                Material = new DiffuseMaterial(Brushes.PaleVioletRed)
            });

            Volume = _meshService.Volume();

            OnPropertyChanged(nameof(GeometryGroup));
        }
    }
}
