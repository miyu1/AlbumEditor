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
                Console.WriteLine("Name: {0}, Width:{1}, Height:{2}", 
                                  Name, thumbnail.CGImage.Width, thumbnail.CGImage.Height );
				// var data = thumbnail.CGImage.DataProvider.CopyData();
				// Console.WriteLine("Data Length: {0}", data.Length);

				// System.Console.WriteLine("CImage: {0}", );
			} );
		}

		protected UIImage thumbnail;
        /*
		protected UIImage thumbnail
        {
            get { return _thumbnail; }
            set { 
                _thumbnail = value; 
                // RaisePropertyChanged("Thumbnail");
				// RaisePropertyChanged("thumbnail");
			}
        }
        */

        public String Name { 
            get{
                var ret = asset.ValueForKey( new Foundation.NSString("filename") ); // file name
                // var ret = asset.LocalIdentifier;
                return ret?.ToString();
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
		public ImageSource Thumbnail 
        {
            get
            {
				var ret = this.thumbnail?.AsJPEG();
				var ret2 = ret.AsStream();

				return ImageSource.FromStream( () => ret2 );
            }
        }
	}
}
