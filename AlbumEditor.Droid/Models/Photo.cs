using System;
using System.IO;

using Prism.Mvvm;
using Xamarin.Forms;

using AlbumEditor.Models;

namespace AlbumEditor.Droid.Models
{
    public class Photo : BindableBase, IPhoto
    {
        public Photo()
        {
        }

        public String Name { get; } = "(Unkown)";
        public ImageSource Thumbnail { get; } = null;
    }
}
