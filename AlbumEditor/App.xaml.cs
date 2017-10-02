using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DryIoc;
using Prism.DryIoc;
using AlbumEditor.Views;

namespace AlbumEditor
{
    public partial class App : PrismApplication
    {
        public static String Version = "0.1"; 

        public App(){}
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            // NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
            NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
        }
    }
}

