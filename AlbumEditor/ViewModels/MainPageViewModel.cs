using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlbumEditor.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainPageViewModel(Models.IPhotoService photoService)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var name = assembly.GetName();

            var count = photoService.AlbumCount;
            Title = String.Format( "{0} Version {1} Count:{2}",
                                  name.Name, App.Version, count );
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            // if (parameters.ContainsKey("title"))
            //     Title = (string)parameters["title"] + " and Prism";
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}

