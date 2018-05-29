using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DronZone_IoT.Business.Services;
using DronZone_IoT.Models.Auth;
using DronZone_IoT.Presentation.Views.AppMenuContainer;
using DronZone_IoT.Presentation.Views.Auth;
using ReactiveUI;

namespace DronZone_IoT.Presentation.ViewModels.Auth
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        
        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            SignInCommand = ReactiveCommand.CreateFromTask(SignIn);
            GoToRegisterFormCommand = ReactiveCommand.Create(GoToRegisterPage);

            LoginModel = new LoginModel
            {
                Username = "user1@test.com",
                Password = "Test123!",
                RememberMe = true
            };
        }

        public LoginModel LoginModel { get; }

        public ReactiveCommand SignInCommand { get; }

        public ReactiveCommand GoToRegisterFormCommand { get; }

        private async Task SignIn()
        {
            IsBusy = true;
            try
            {
                bool isSuccess = await _authenticationService.LoginAsync(LoginModel.Username, LoginModel.Password, LoginModel.RememberMe);
                if (isSuccess)
                {
                    GoToMenuContainerPage();
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void GoToMenuContainerPage()
        {
            var frame = Window.Current.Content as Frame;
            frame?.Navigate(typeof(AppMenuContainerPage));
        }

        private void GoToRegisterPage()
        {
            var frame = Window.Current.Content as Frame;
            frame?.Navigate(typeof(RegistrationPage));
        }
    }
}
