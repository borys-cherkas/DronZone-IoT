using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Maps;
using Autofac;
using DronZone_IoT.Presentation.ViewModels.Drone;
using ReactiveUI;

namespace DronZone_IoT.Presentation.Views.Drones
{
    public sealed partial class DroneDetailsPage : IViewFor<DroneDetailsViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel),
                typeof(DroneDetailsViewModel),
                typeof(DroneDetailsPage),
                new PropertyMetadata(default(DroneDetailsViewModel)));

        public DroneDetailsPage()
        {
            InitializeComponent();
            ViewModel = App.Container.Resolve<DroneDetailsViewModel>();

            this.WhenActivated(CreateBindings);
        }

        private void CreateBindings(Action<IDisposable> d)
        {
            d(this.OneWayBind(ViewModel, vm => vm.IsBusy, v => v.Preloader.IsLoading));

            d(this.OneWayBind(ViewModel, vm => vm.DroneModel.Id, v => v.DroneIdTextBlock.Text));

            d(this.BindCommand(ViewModel, vm => vm.StartFlightCommand, v => v.StartFlightButton));
            d(this.BindCommand(ViewModel, vm => vm.FinishFlightCommand, v => v.StopFlightButton));

            d(this.OneWayBind(ViewModel, vm => vm.FlightInProgress, v => v.FlightContolStackPanel.Visibility,
                flightInProgress => flightInProgress ? Visibility.Visible : Visibility.Collapsed));
            d(this.OneWayBind(ViewModel, vm => vm.FlightInProgress, v => v.NonFlightContolStackPanel.Visibility,
                flightInProgress => flightInProgress ? Visibility.Collapsed : Visibility.Visible));

            d(ViewModel.ObservableForProperty(x => x.Longitude)
                .Merge(ViewModel.ObservableForProperty(x => x.Latitude))
                .Throttle(TimeSpan.FromSeconds(1))
                .Where(x => x != null)
                .ObserveOnDispatcher()
                .Subscribe(UpdateMapArea));
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (DroneDetailsViewModel)value;
        }

        public DroneDetailsViewModel ViewModel
        {
            get => (DroneDetailsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.FlightDirection = FlightDirection.Top;
        }

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.FlightDirection = FlightDirection.Right;
        }

        private void BottomButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.FlightDirection = FlightDirection.Bottom;
        }

        private void LeftButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.FlightDirection = FlightDirection.Left;
        }

        private void AreaMapControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            AreaMapControl.ZoomLevel = 14;
            AreaMapControl.Style = MapStyle.Road;
            AreaMapControl.MapProjection = MapProjection.WebMercator;
            AreaMapControl.StyleSheet = MapStyleSheet.RoadLight();
        }

        private void UpdateMapArea(object _ = null)
        {
            if (AreaMapControl.Center == ViewModel.MapCenter) return;

            AreaMapControl.MapElements.Clear();

            AreaMapControl.Center = ViewModel.MapCenter;

            MapIcon mapIcon = new MapIcon
            {
                Location = ViewModel.MapCenter,
                ZIndex = 1
            };

            AreaMapControl.MapElements.Add(mapIcon);
        }

        private bool _inited = false;
        private void AreaMapControl_OnCenterChanged(MapControl sender, object args)
        {
            if (!_inited)
            {
                sender.Center = new Geopoint(new BasicGeoposition()
                {
                    Longitude = 36.227283788751,
                    Latitude = 50.016684062646874
                });
                ViewModel.Longitude = 36.227283788751;
                ViewModel.Latitude = 50.016684062646874;

                _inited = true;
                return;
            }

            if (ViewModel.FlightInProgress)
            {
                return;
            }

            ViewModel.Longitude = sender.Center.Position.Longitude;
            ViewModel.Latitude = sender.Center.Position.Latitude;
        }
    }
}
