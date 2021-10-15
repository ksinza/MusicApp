using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaManager;
using MusicAppXamarin.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MusicAppXamarin.ViewModel
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(Music selectedMusic, ObservableCollection<Music> musicList)
        {
            this.selectedMusic = selectedMusic;
            this.musicList = musicList;
            PlayMusic(selectedMusic);
            isPlaying = true;
        }

        #region Properties
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

        private TimeSpan duration;
        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan position;
        public TimeSpan Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged();
            }
        }

        double maximum = 100f;
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > 0)
                {
                    maximum = value;
                    OnPropertyChanged();
                }
            }
        }


        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayIcon));

            }
        }

        public string PlayIcon { get => isPlaying ? "pause.png" : "play.png"; }

        #endregion

        public ICommand PlayCommand => new Command(Play);
        public ICommand ChangeCommand => new Command(ChangeMusic);
        public ICommand BackCommand => new Command(async () => {
            await CrossMediaManager.Current.Stop();
           await Application.Current.MainPage.Navigation.PopAsync();
        });
        public ICommand ShareCommand => new Command(() => Share.RequestAsync(selectedMusic.Url, selectedMusic.Title));


        private async void Play()
        {
            if (isPlaying)
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = false; ;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                IsPlaying = true; ;
            }
        }

        private async void ChangeMusic(object obj)
        {
            if ((string)obj == "P")
               await PreviousMusicAsync();
            else if ((string)obj == "N")
                await NextMusicAsync();
        }

        private async void PlayMusic(Music music)
        {
            var mediaInfo = CrossMediaManager.Current;
            await mediaInfo.Play(music?.Url);
            IsPlaying = true;

            mediaInfo.MediaItemFinished += async (sender, args) =>
            {
                IsPlaying = false;
                await NextMusicAsync();
            };

            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Duration = mediaInfo.Duration;
                Maximum = duration.TotalSeconds;
                Position = mediaInfo.Position;
                return true;
            });
        }

        private async Task NextMusicAsync()
        {
            var currentIndex = musicList.IndexOf(selectedMusic);

            if (currentIndex < musicList.Count - 1)
            {
                await CrossMediaManager.Current.Stop();
                IsPlaying = false; ;
                SelectedMusic = musicList[currentIndex + 1];
                PlayMusic(selectedMusic);
            }
        }

        private async Task PreviousMusicAsync()
        {
            var currentIndex = musicList.IndexOf(selectedMusic);

            if (currentIndex > 0)
            {
                await CrossMediaManager.Current.Stop();
                IsPlaying = false; ;
                SelectedMusic = musicList[currentIndex - 1];
                PlayMusic(selectedMusic);
            }
        }
    }
}
