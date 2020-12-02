using Newtonsoft.Json;
using OnSpa.Common.Helpers;
using OnSpa.Common.Models;
using OnSpa.Common.Responses;
using OnSpa.Prism.Helpers;
using OnSpa.Prism.ItemViewModels;
using OnSpa.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnSpa.Prism.ViewModels
{
    public class OnSpaMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private static OnSpaMasterDetailPageViewModel _instance;
        private User _user;

        public OnSpaMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }

        public static OnSpaMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }


        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public void LoadUser()
        {
            if (Settings.IsLogin)
            {
                LoginResponse token = JsonConvert.DeserializeObject<LoginResponse>(Settings.Token);
                User = token.User;
            }
        }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                Icon = "ic_login",
                PageName = $"{nameof(LoginPage)}",
                Title = Settings.IsLogin ? Languages.Logout : Languages.Login
            },
            new Menu
            {
                Icon = "ic_services",
                PageName = $"{nameof(ServiceTypePage)}",
                Title = Languages.ServiceType
            },

            new Menu
            {
                Icon = "ic_person",
                PageName = $"{nameof(ModifyUserPage)}",
                Title = Languages.ModifyUser,
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_agenda",
                PageName = $"{nameof(ReservePage)}",
                Title = Languages.Reserve,
                IsLoginRequired = true
            },
            new Menu
            {
            Icon = "ic_location_on",
            PageName = $"{nameof(MapPage)}",
            Title = Languages.Customer
            },

            new Menu
            {
                Icon = "ic_history",
                PageName = $"{nameof(RecordPage)}",
                Title = Languages.Record,
                IsLoginRequired = true
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }

    }
}
