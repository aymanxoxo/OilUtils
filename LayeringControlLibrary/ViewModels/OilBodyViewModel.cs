using Infrastructure;
using Infrastructure.Extensions;
using Interfaces.Events;
using Interfaces.IServices;
using Models;
using Models.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace LayeringControlLibrary.ViewModels
{
    public class OilBodyViewModel : BaseViewModel
    {
        private readonly IThreeDimensionsMeshService _meshService;

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

        private VolumeUnits _selectedUnit = VolumeUnits.CubicMeter;
        public VolumeUnits SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }

        private Dictionary<VolumeUnits, string> _unitsList = new Dictionary<VolumeUnits, string>();
        public Dictionary<VolumeUnits, string> UnitsList
        {
            get
            {
                return _unitsList;
            }
            set
            {
                _unitsList = value;
                OnPropertyChanged(nameof(UnitsList));
            }
        }

        public OilBodyViewModel(IThreeDimensionsMeshService meshService, IEventAggregator eventAggregator)
        {
            // inject services
            _meshService = meshService;
            eventAggregator.GetEvent<RequestBodyDrawEvent>().Subscribe(DrawBody);

            // add light to the geometry
            GeometryGroup.Add(new DirectionalLight { Direction = new Vector3D(-1, -1, -1) });
            GeometryGroup.Add(new DirectionalLight { Direction = new Vector3D(-1, -1, 1) });

            // initialize converters list
            foreach(var unitValue in Enum.GetValues(typeof(VolumeUnits)))
            {
                var unit = (VolumeUnits)unitValue;
                UnitsList.Add(unit, unit.ToString());
            }
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
