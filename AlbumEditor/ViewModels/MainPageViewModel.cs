using System;
using System.Windows.Input; // ICommand
// using System.Collections.Generic;
// using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace AlbumEditor.ViewModels
{
    using Models;

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

		// public ObservableCollection<PhotoViewModel> PhotoList { get; } = new ObservableCollection<PhotoViewModel>();
		public ObservableCollection<IPhoto> PhotoList { get; } = new ObservableCollection<IPhoto>();

		private IPhotoService photoService;

        public ICommand RefreshCommand { get; }
                                          
        public MainPageViewModel(IPhotoService photoService)
        {
            this.photoService = photoService;
            PhotoInfo = "IsServiceReady:" + photoService.IsServiceReady.ToString();
            if(photoService.IsServiceReady){
                PhotoInfo = "IsAuthorized:" + photoService.IsAuthorized.ToString();
                if(photoService.IsAuthorized)
                    PhotoInfo = String.Format("AlbumCount:{0}", photoService.AlbumCount);
                    ResetPhotoInfo();
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
                        ResetPhotoInfo();
                    }
                };
            }

            RefreshCommand = new DelegateCommand( () => {
                ResetPhotoInfo();
            } );
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

        protected void ResetPhotoInfo()
        {
            PhotoList.Clear();
            foreach( var p in photoService.PhotoList ){
				// PhotoList.Add(new PhotoViewModel(p));
				PhotoList.Add(p);
			}
        }
	}

    public class PhotoViewModel : BindableBase {

        protected readonly IPhoto photoModel;

        public PhotoViewModel(Models.IPhoto photoModel){
            this.photoModel = photoModel;
        }

        /*
        public ImageSource Thumbnail => 
            ImageSource.FromStream( () => photoModel.Thumbnail );
        */

        public String Name => photoModel.Name;

    }
}

