using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MusicAppXamarin.Model;
using MusicAppXamarin.View;
using Xamarin.Forms;

namespace MusicAppXamarin.ViewModel
{
    public class LandingViewModel : BaseViewModel
    {
        public LandingViewModel() {

            musicList = GetMusics();
            recentMusic = musicList.Where(x => x.IsRecent == true).FirstOrDefault();
        }

        ObservableCollection<Music> musicList;
        public ObservableCollection<Music> MusicList
        {
            get { return musicList; }
            set
            {
                musicList = value;
                OnPropertyChanged();
            }
        }

        private Music recentMusic;
        public Music RecentMusic
        {
            get { return recentMusic; }
            set
            {
                recentMusic = value;
                OnPropertyChanged();
            }
        }

        private Music selectedMusic;
        public Music SelectedMusic
        {
            get { return selectedMusic; }
            set
            {
                selectedMusic = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionCommand => new Command(PlayMusic);

        private void PlayMusic()
        {
            if (selectedMusic != null)
            {
                var viewModel = new PlayerViewModel(selectedMusic, musicList);
                var playerPage = new PlayerPage { BindingContext = viewModel };

                var navigation = Application.Current.MainPage as NavigationPage;
                navigation.PushAsync(playerPage, true);
            }
        }


        private ObservableCollection<Music> GetMusics()
        {
            return new ObservableCollection<Music>
            {
                new Music { Title = "Mia", Artist = "Bad bunny feat drake", Url = "https://firebasestorage.googleapis.com/v0/b/kevin-f830b.appspot.com/o/Bad%20Bunny%20feat.%20Drake%20-%20Mia%20(%20Video%20Oficial%20).mp3?alt=media&token=02ef1b46-c4aa-4c91-81e4-23e95acd4a53", CoverImage = "https://i.ytimg.com/vi/OSUxrSe5GbI/maxresdefault.jpg", IsRecent = true},
                new Music { Title = "You are", Artist = "Charlie Wilson", Url = "https://firebasestorage.googleapis.com/v0/b/kevin-f830b.appspot.com/o/Charlie%20Wilson%20-%20You%20Are%20(HQ)%20(Lyrics).mp3?alt=media&token=e074cda8-81ff-48ca-a3b9-3476f9259499", CoverImage = "https://images.genius.com/7638611b82808f38c9fd484e824f97da.600x600x1.jpg"},
                new Music { Title = "Quiero decirte", Artist = "Hermanos Lebron", Url = "https://firebasestorage.googleapis.com/v0/b/kevin-f830b.appspot.com/o/Quiero%20decirte%20-%20Hermanos%20Lebro%CC%81n%2Bletra.mp3?alt=media&token=9b954921-1eae-4067-8044-a9f6e9e4fb0a", CoverImage = "https://caracoltv.brightspotcdn.com/dims4/default/353580b/2147483647/strip/true/crop/640x336+0+45/resize/1200x630!/quality/90/?url=https%3A%2F%2Fcaracol-brightspot.s3-us-west-2.amazonaws.com%2Fassets%2Fnoticias%2Flebron_0.jpg"},
                new Music { Title = "Rolex", Artist = "Ayo & Teo ", Url = "https://firebasestorage.googleapis.com/v0/b/kevin-f830b.appspot.com/o/rolly%20rolly%20ayo%20and%20teo.mp3?alt=media&token=457d0cfb-5dfd-45fb-828b-d6887cd7dd22", CoverImage="https://www.famousbirthdays.com/group_images/medium/ayo-teo-dancecrew.jpg"},
                new Music { Title = "Mucho coro", Artist = "El Dek", Url = "https://firebasestorage.googleapis.com/v0/b/kevin-f830b.appspot.com/o/muchocoro.mp3?alt=media&token=eed229d5-f1df-4316-9e51-eaceb893274a" , CoverImage="https://is5-ssl.mzstatic.com/image/thumb/Music124/v4/63/f1/a4/63f1a45b-1836-db2f-afe9-fae781b23065/0.jpg/400x400cc.jpg"},
            };
        }

        
    }
}
