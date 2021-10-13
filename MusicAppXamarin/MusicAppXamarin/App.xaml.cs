using System;
using MusicAppXamarin.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicAppXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LandingPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
