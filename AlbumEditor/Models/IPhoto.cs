using System;
using System.IO;
using System.ComponentModel;

using Xamarin.Forms;

using Prism.Mvvm;

namespace AlbumEditor.Models
{
    public interface IPhoto : INotifyPropertyChanged
    {
        String Name { get; }
        // Stream Thumbnail { get; }
        ImageSource Thumbnail { get; }
    }
}
