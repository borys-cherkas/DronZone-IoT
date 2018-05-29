using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using DronZone_IoT.Business.Services;
using DronZone_IoT.Models.Area;
using DronZone_IoT.Models.Drone;
using DronZone_IoT.Models.Flights;
using ReactiveUI;

namespace DronZone_IoT.Presentation.ViewModels.Drone
{
    public enum FlightDirection
    {
        Top = 0, 
        Right = 90,
        Bottom = 180,
        Left = 270
    }

    public class DroneDetailsViewModel : ViewModelBase
    {
        private readonly IDroneService _droneService;
        private readonly IFlightService _flightService;
        
        private DroneDetailedModel _droneModel;
        private bool _isFlightInProgress;
        private FlightDirection _flightDirection;
        private double _latitude;
        private double _longitude;
        private double _speed;
        private DroneFlight _currentFlight;


        public DroneDetailsViewModel(IDroneService droneService, IFlightService flightService)
        {
            _droneService = droneService;
            _flightService = flightService;

            StartFlightCommand = ReactiveCommand.CreateFromTask(async () => await StartFlightExecuteAsync());
            FinishFlightCommand = ReactiveCommand.CreateFromTask(async () => await FinishFlightExecuteAsync());
            
            Speed = 1;



            FlightDirection = FlightDirection.Top;

            Init();
        }

        private IDisposable _flightSubscription;

        private async Task StartFlightExecuteAsync()
        {
            FlightInProgress = true;

            await CreateFlightAsync();

            _flightSubscription?.Dispose();
            _flightSubscription = Observable.Interval(TimeSpan.FromSeconds(3))
                .Subscribe(async _ => await RequestMovingAsync());
        }

        private Task FinishFlightExecuteAsync()
        {
            _flightSubscription?.Dispose();
            _flightSubscription = null;
            CurrentFlight = null;
            FlightInProgress = false;

            return Task.CompletedTask;
        }

        private async Task RequestMovingAsync()
        {
            var speed = Speed;
            var currentLatitude = Latitude;
            var currentLongitude = Longitude;
            
            double latitudeDelta;
            double longitudeDelta;
            double coef = 0.0004;
            if (FlightDirection == FlightDirection.Top)
            {
                latitudeDelta = 1 * speed * coef;
                longitudeDelta = 0;
            }
            else if (FlightDirection == FlightDirection.Right)
            {
                latitudeDelta = 0;
                longitudeDelta = 1 * speed * coef;
            }
            else if (FlightDirection == FlightDirection.Bottom)
            {
                latitudeDelta = -1 * speed * coef;
                longitudeDelta = 0;
            }
            else if (FlightDirection == FlightDirection.Left)
            {
                latitudeDelta = 0;
                longitudeDelta = -1 * speed * coef;
            } else
            {
                latitudeDelta = 0;
                longitudeDelta = 0;
            }

            var requestedLatitude = currentLatitude + latitudeDelta;
            var requestedLongitude = currentLongitude + longitudeDelta;

            var isAllowed = await _flightService.TryMovingAsync(CurrentFlight.Id, requestedLatitude, requestedLongitude);
            if (!isAllowed)
            {
                await FinishFlightExecuteAsync();
                await ShowErrorAsync("You tried to get restricted zone");
            }
            else
            {
                Latitude = requestedLatitude;
                Longitude = requestedLongitude;
            }
        }

        public ReactiveCommand StartFlightCommand { get; }
        public ReactiveCommand FinishFlightCommand { get; }

        public double Latitude
        {
            get => _latitude;
            set => this.RaiseAndSetIfChanged(ref _latitude, value);
        }
        public double Longitude
        {
            get => _longitude;
            set => this.RaiseAndSetIfChanged(ref _longitude, value);
        }

        public double Speed
        {
            get => _speed;
            set => this.RaiseAndSetIfChanged(ref _speed, value);
        }

        public DroneFlight CurrentFlight
        {
            get => _currentFlight;
            set => this.RaiseAndSetIfChanged(ref _currentFlight, value);
        }

        public bool FlightInProgress
        {
            get => _isFlightInProgress;
            set => this.RaiseAndSetIfChanged(ref _isFlightInProgress, value);
        }

        public FlightDirection FlightDirection
        {
            get => _flightDirection;
            set => this.RaiseAndSetIfChanged(ref _flightDirection, value);
        }

        public DroneDetailedModel DroneModel
        {
            get => _droneModel;
            set => this.RaiseAndSetIfChanged(ref _droneModel, value);
        }

        public Geopoint MapCenter => new Geopoint(new BasicGeoposition()
        {
            Latitude = Latitude,
            Longitude = Longitude
        });

        private async void Init()
        {
            await LoadDroneDetailsAsync();
        }

        private async Task LoadDroneDetailsAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                DroneModel = MenuContentViewModel.Param as DroneDetailedModel;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }
        }

        private async Task CreateFlightAsync()
        {
            OnIsInProgressChanges(true);

            try
            {
                var flight = await _flightService.StartFlightAsync(_droneModel.Id);
                CurrentFlight = flight;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                OnIsInProgressChanges(false);
            }
        }
    }
}