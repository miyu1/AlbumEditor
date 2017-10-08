using System;
using System.Collections.Generic;

namespace AlbumEditor.Models
{
    public interface IPhotoService
    {
        // Invokes event when this service is ready(initialized)
        event EventHandler ServiceReady; 

        // Returns if this service is ready or not
        // Ready does not always means that authorized to access photo info
        bool IsServiceReady{ get; }

        // Returns if authorized to access photo info
        bool IsAuthorized{ get; }

		long AlbumCount { get; }

        List<IPhoto> PhotoList { get; }
	}
}
