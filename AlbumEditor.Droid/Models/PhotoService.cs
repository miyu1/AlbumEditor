using System;
using System.Collections.Generic;

using AlbumEditor.Models;

namespace AlbumEditor.Droid.Models
{
    public class PhotoService : IPhotoService
    {
        // Invokes event when this service is ready(initialized)
        public event EventHandler ServiceReady;
        
        public PhotoService()
        {
            IsServiceReady = true;
            IsAuthorized = false;
            AlbumCount = 0;
            PhotoList = new List<IPhoto>();
            
            ServiceReady?.Invoke(this, null);
        }

        public bool IsServiceReady{ get; private set; }
        public bool  IsAuthorized{ get; private set; }
        public List<IPhoto> PhotoList { get; private set; }

		public long AlbumCount { get; private set; }
    }
}
