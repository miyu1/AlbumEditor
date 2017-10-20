using System;
using System.IO;

using Xamarin.Forms;

using Prism.Mvvm;

using UIKit;
using Photos;
using CoreGraphics;

using AlbumEditor.Models;

namespace AlbumEditor.iOS.Models
{
    public class Photo : BindableBase, IPhoto
    {
        protected PHAsset asset;

        public Photo(PHAsset asset)
        {
            this.asset = asset;

			var size = new CGSize(100, 100);
            // var manager = PHImageManager.DefaultManager;
            var manager = PHCachingImageManager.DefaultManager;

			manager.RequestImageForAsset(asset, size, PHImageContentMode.AspectFill, 
                                         null, (result, info) => {
                this.thumbnail = result;
				// var data = thumbnail.CGImage.DataProvider.CopyData();
				// Console.WriteLine("Data Length: {0}", data.Length);

				// System.Console.WriteLine("CImage: {0}", );
			} );
		}

        public String Name { 
            get{
                // var ret = asset.ValueForKey( new Foundation.NSString("filename") ); // file name
                // var ret = asset.LocalIdentifier;
                var ret = DateTime.Now;
                return ret.ToString();
            }
        }

		protected UIImage _thumbnail;
		protected UIImage thumbnail
        {
            get { return _thumbnail; }
            set { 
                SetProperty(ref _thumbnail, value);

				var ret = _thumbnail?.AsJPEG();
				var ret2 = ret.AsStream();
                Thumbnail = ImageSource.FromStream( () => {
                    return ret2;   
                } );

				// RaisePropertyChanged("Thumbnail");
				// RaisePropertyChanged("thumbnail");
                // RaisePropertyChanged( "Stream" );
            }
        }

		/*
        public Stream Thumbnail { 
            get {
                var ret = this.thumbnail?.AsJPEG();
                var ret2 = ret.AsStream();
                return ret2;
            }
        }*/

        private ImageSource _Thumbnail;
		public ImageSource Thumbnail 
        {
            get
            {
                // Console.WriteLine( "PropertyName:{0}", StreamImageSource.StreamProperty.PropertyName );
                return _Thumbnail;
            }

            private set
            {
                /*
                Console.WriteLine( "Raise: Name: {0}, Width:{1}, Height:{2}",
                  Name, _thumbnail.CGImage.Width, _thumbnail.CGImage.Height );
                  */
                SetProperty( ref _Thumbnail, value, "Stream" );
                // SetProperty( ref _Thumbnail, value );
            }
        }
	}
}
