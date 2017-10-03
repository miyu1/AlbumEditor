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

        private string _photoInfo;
        public string PhotoInfo
        {
            get { return _photoInfo; }
            set { SetProperty(ref _photoInfo, value); }
        }


        private Models.IPhotoService photoService;

        public MainPageViewModel(Models.IPhotoService photoService)
        {
            this.photoService = photoService;
            PhotoInfo = "IsServiceReady:" + photoService.IsServiceReady.ToString();
            if(photoService.IsServiceReady){
                PhotoInfo = "IsAuthorized:" + photoService.IsAuthorized.ToString();
                if(photoService.IsAuthorized)
                    PhotoInfo = String.Format("AlbumCount:{0}", photoService.AlbumCount);
			} else {
                photoService.ServiceReady += (sender, e) =>
                {
                    this.photoService = (Models.IPhotoService)sender;
                    PhotoInfo = String.Format("IsServiceReady:{0}, IsAuthorized:{1}",
                                               photoService.IsServiceReady,
                                               photoService.IsAuthorized);
                    if(photoService.IsAuthorized){
                        PhotoInfo = String.Format("AlbumCount:{0}", 
                                                  photoService.AlbumCount);
                    }
                };
            }
		}

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
			// if (parameters.ContainsKey("title"))
			//     Title = (string)parameters["title"] + " and Prism";
			var assembly = this.GetType().GetTypeInfo().Assembly;
			var name = assembly.GetName();

			// var count = photoService.AlbumCount;
			Title = String.Format("{0} Version {1}",
								  name.Name, App.Version);

		}

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}

