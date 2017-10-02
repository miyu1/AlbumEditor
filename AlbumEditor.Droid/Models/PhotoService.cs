using System;

using AlbumEditor.Models;

namespace AlbumEditor.Droid.Models
{
    public class PhotoService : IPhotoService
    {
        public PhotoService()
        {
            AlbumCount = 0;
        }

		public int AlbumCount { get; private set; }

    }
}
