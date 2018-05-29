using System;
using DronZone_IoT.Presentation.Views.Auth;
using ReactiveUI;

namespace DronZone_IoT.Utils
{
    public class MenuNavigationHelper : ReactiveObject
    {
        private Type _currentPageType;
        private object _param;


        public void NavigateToLoginPage()
        {
            Param = null;
            CurrentPageType = typeof(LoginPage);
        }

        public void NavigateTo(Type pageType, object param = null)
        {
            Param = param;
            CurrentPageType = pageType;
        }

        public Type CurrentPageType
        {
            get => _currentPageType;
            private set => this.RaiseAndSetIfChanged(ref _currentPageType, value);
        }

        public object Param
        {
            get => _param;
            private set => this.RaiseAndSetIfChanged(ref _param, value);
        }
    }
}
