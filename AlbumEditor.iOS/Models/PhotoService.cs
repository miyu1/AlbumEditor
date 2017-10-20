using System;
using System.Collections.Generic;

using Foundation;
using Photos;

using AlbumEditor.Models;

namespace AlbumEditor.iOS.Models
{
    public class PhotoService : IPhotoService
    {
        // Invokes event when this service is ready(initialized)
        public event EventHandler ServiceReady;

        private PHAuthorizationStatus authorizationStatus;

        private void ResetPhotoInfo(){
            var fetchReslt = PHAsset.FetchAssets(null);

            PhotoList.Clear();

            foreach ( PHAsset p in fetchReslt ){
                var photo = new Photo(p);
                PhotoList.Add(photo);
            }
        }
                                                         
        public PhotoService()
        {
            IsServiceReady = false;

            authorizationStatus = PHPhotoLibrary.AuthorizationStatus;
            if(authorizationStatus == PHAuthorizationStatus.NotDetermined){
                PHPhotoLibrary.RequestAuthorization(
                    stat =>
                    {
                        authorizationStatus = stat;
                        IsServiceReady = true;
                        ResetPhotoInfo();
                    }
                );
            } else {
                IsServiceReady = true;
                ResetPhotoInfo();
            }
        }

        private bool _isServiceReady = false;
        public bool IsServiceReady
        {
            get { return _isServiceReady; }
            private set
            {
                if (_isServiceReady == false && value == true)
					ServiceReady?.Invoke(this, null);

                _isServiceReady = value;
            }
        }

        public bool IsAuthorized
        {
            get
            {
                return (authorizationStatus == PHAuthorizationStatus.Authorized);
            }
        }

        public List<IPhoto> PhotoList { get; } = new List<IPhoto>();
        /*
        {
            get {
                var fetchReslt = PHAsset.FetchAssets(null);
                var ret = new List<IPhoto>();

                foreach ( PHAsset p in fetchReslt ){
                    var photo = new Photo(p);

                    ret.Add(photo);
                }

                return ret;
            }            
        }
        */


		public long AlbumCount { 
            get
            {
                if( !IsAuthorized ) return 0;

                var option = new PHFetchOptions();
                option.IncludeAssetSourceTypes = PHAssetSourceType.UserLibrary;

                // list of album and folder
                var col = PHCollection.FetchTopLevelUserCollections(option);
                // list of shared album
                col = PHAssetCollection.FetchAssetCollections( PHAssetCollectionType.Album,
                                                               PHAssetCollectionSubtype.AlbumCloudShared,
                                                               null );
                // list of photos 
                col = PHAsset.FetchAssets(null);
                return col.Count;
            }
        }
    }
}
