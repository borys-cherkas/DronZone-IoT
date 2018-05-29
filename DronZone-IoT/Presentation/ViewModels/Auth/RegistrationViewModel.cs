﻿using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DronZone_IoT.Business.Services;
using DronZone_IoT.Models.Auth;
using DronZone_IoT.Presentation.Views.AppMenuContainer;
using ReactiveUI;

namespace DronZone_IoT.Presentation.ViewModels.Auth
{
    public class RegistrationViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;

        public RegistrationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            RegisterCommand = ReactiveCommand.CreateFromTask(SignIn);

            RegistrationModel = new RegistrationModel();
        }

        public ReactiveCommand RegisterCommand { get; }

        public RegistrationModel RegistrationModel { get; }

        private async Task SignIn()
        {
            IsBusy = true;
            try
            {
                bool isSuccess = await _authenticationService.RegisterAsync(RegistrationModel);
                if (isSuccess)
                {
                    isSuccess = await _authenticationService.LoginAsync(RegistrationModel.Email, RegistrationModel.Password, false);
                }

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
    }
}
