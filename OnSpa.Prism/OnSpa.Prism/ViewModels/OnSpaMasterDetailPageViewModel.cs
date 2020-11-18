using Concierto.Common.Models;
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

        public OnSpaMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
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
                Title = Languages.Login
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
