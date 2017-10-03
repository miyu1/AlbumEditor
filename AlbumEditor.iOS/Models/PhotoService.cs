using System;

using Foundation;
using UIKit;
using Photos;

using AlbumEditor.Models;

namespace AlbumEditor.iOS.Models
{
    public class PhotoService : IPhotoService
    {
        // Invokes event when this service is ready(initialized)
        public event EventHandler ServiceReady; 

        private PHAuthorizationStatus authorizationStatus;
        // private PHFetchResult col;

        public PhotoService()
        {
            IsServiceReady = false;

            authorizationStatus = PHPhotoLibrary.AuthorizationStatus;
            if( authorizationStatus == PHAuthorizationStatus.NotDetermined)
                PHPhotoLibrary.RequestAuthorization( 
                    stat => {
                        authorizationStatus = stat;
                        IsServiceReady = true;
                    }
                );
            else                        
                IsServiceReady = true;
        }

        private bool _isServiceReady = false;
        public bool IsServiceReady{ 
            get{ return _isServiceReady; }
            private set
            {
                bool invoke = false;
                if( _isServiceReady == false && value == true )
                    invoke = true;
                    
                _isServiceReady = value;

                if( invoke )
                    ServiceReady?.Invoke(this, null);
            }
        }

        public bool IsAuthorized{
            get{ 
                return(authorizationStatus==PHAuthorizationStatus.Authorized); 
            } 
        }


		public long AlbumCount { 
            get
            {
                if( !IsAuthorized ) return 0;

                var col = PHCollection.FetchTopLevelUserCollections(null);
                // var col2 = PHCollection.FetchTopLevelUserCollections(null);
                // int ret = (int)col2.Count;
                // return ret;
                return col.Count;
            }
        }
    }
}
