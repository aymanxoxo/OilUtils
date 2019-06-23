using Infrastructure;
using Infrastructure.Command;
using Interfaces.Events;
using Interfaces.IServices;
using Models;
using Models.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Unity;

namespace LayeringControlLibrary.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly NewLayerAddedEvent _layerAddedEvent;
        private readonly RequestBodyDrawEvent _requestBodyDrawEvent;
        private readonly ILayerReaderService<IntervalReaderSettings> _intervalReaderService;
        private readonly ILayerReaderService<FileReaderSettings> _fileReaderService;
        private readonly IUnitConverter _converterService;

        public SettingsViewModel(IEventAggregator eventAggregator,
            ILayerReaderService<IntervalReaderSettings> intervalReaderService,
            ILayerReaderService<FileReaderSettings> fileReaderService,
            IUnitConverter converterService)
        {
            _intervalReaderService = intervalReaderService;
            _fileReaderService = fileReaderService;
            _converterService = converterService;
            _layerAddedEvent = eventAggregator.GetEvent<NewLayerAddedEvent>();
            _requestBodyDrawEvent = eventAggregator.GetEvent<RequestBodyDrawEvent>();
            RunDemo();
        }

        #region Properties
        private List<LayerModel> _upperLayers = new List<LayerModel>();
        public List<LayerModel> UpperLayers
        {
            get
            {
                return _upperLayers;
            }
            set
            {
                _upperLayers = value;
                OnPropertyChanged(nameof(UpperLayers));
            }
        }

        private List<LayerModel> _lowerLayers = new List<LayerModel>();
        public List<LayerModel> LowerLayers
        {
            get
            {
                return _lowerLayers;
            }
            set
            {
                _lowerLayers = value;
                OnPropertyChanged(nameof(LowerLayers));
            }
        }
        #endregion

        #region Commands
        private ICommand _drawCommand;
        public ICommand DrawCommand
        {
            get
            {
                return (_drawCommand ?? (_drawCommand = new Command(DrawOilBody)));
            }
        }
        #endregion

        #region Private Methods
        private void RunDemo()
        {
            // Add Top Horizon
            var topHorizonX = new IntervalReaderSettings
            {
                Interval = _converterService.Convert(200, LengthUnits.Foot, LengthUnits.Meter),
                PointsCount = 16,
                StartPoint = 0
            };
            var topHorizonY = new IntervalReaderSettings
            {
                Interval = _converterService.Convert(200, LengthUnits.Foot, LengthUnits.Meter),
                PointsCount = 26,
                StartPoint = 0
            };
            var topHorizonZ = new FileReaderSettings
            {
                FilePath = AppDomain.CurrentDomain.BaseDirectory + "Content/TopHorizonDepths.txt"
            };

            var topHorizon = new LayerModel
            {
                Name = "Top Horizon",
                Color = new SolidColorBrush(Color.FromArgb(180, 33, 175, 17)),
                X = _intervalReaderService.ReadPoints(topHorizonX),
                Y = _intervalReaderService.ReadPoints(topHorizonY),
                Z = _fileReaderService.ReadPoints(topHorizonZ).Select(p =>
                {
                    return _converterService.Convert(p * -1, LengthUnits.Foot, LengthUnits.Meter);
                }).ToArray()
            };
            AddLayer(topHorizon, UpperLayers, nameof(UpperLayers));

            // Add Base Horizon
            var baseHorizon = new LayerModel
            {
                Name = "Base Horizon",
                Color = new SolidColorBrush(Color.FromRgb(235, 179, 25)),
                X = topHorizon.X,
                Y = topHorizon.Y,
                Z = topHorizon.Z.Select(p => p - 100).ToArray()
            };
            AddLayer(baseHorizon, LowerLayers, nameof(LowerLayers));

            // Add Fluid Body
            var fluidBody = new LayerModel
            {
                Name = "Fluid Body",
                Color = new SolidColorBrush(Color.FromRgb(148, 192, 230)),
                X = topHorizon.X,
                Y = topHorizon.Y,
                Z = _intervalReaderService.ReadPoints(new IntervalReaderSettings
                {
                    Interval = 0,
                    PointsCount = topHorizon.Z.Length,
                    StartPoint = -3000
                })
            };
            AddLayer(fluidBody, LowerLayers, nameof(LowerLayers));
        }

        private void AddLayer(LayerModel layer, List<LayerModel> target, string propName)
        {
            target.Add(layer);

            OnPropertyChanged(propName);

            _layerAddedEvent.Publish(layer);
        }

        private void DrawOilBody()
        {
            if (!UpperLayers.Any() || !LowerLayers.Any())
            {
                throw new ArgumentException();
            }

            var x = UpperLayers.FirstOrDefault().X;
            var y = UpperLayers.FirstOrDefault().Y;
            var upperZ = new List<double>();
            var lowerZ = new List<double>();

            // assuming that all layers have the same x, y
            for (var i = 0; i < UpperLayers.FirstOrDefault().Z.Length; i++)
            {
                var upperMin = UpperLayers.Select(l => l.Z[i]).Min();
                var lowerMax = LowerLayers.Select(l => l.Z[i]).Max();

                upperZ.Add(lowerMax > upperMin ? lowerMax : upperMin);
                lowerZ.Add(lowerMax);
            }

            var oilBody = new BodyModel
            {
                X = x,
                Y = y,
                UpperZ = upperZ.ToArray(),
                LowerZ = lowerZ.ToArray()
            };

            _requestBodyDrawEvent.Publish(oilBody);
        }
        #endregion
    }
}
