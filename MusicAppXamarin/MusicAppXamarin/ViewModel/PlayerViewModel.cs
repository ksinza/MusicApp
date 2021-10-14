using System;
using System.Collections.ObjectModel;
using MusicAppXamarin.Model;

namespace MusicAppXamarin.ViewModel
{
    public class PlayerViewModel : BaseViewModel
    {
        private Music selectedMusic;
        private ObservableCollection<Music> musicList;

        public PlayerViewModel()
        {
        }

        public PlayerViewModel(Music selectedMusic, ObservableCollection<Music> musicList)
        {
            this.selectedMusic = selectedMusic;
            this.musicList = musicList;
        }
    }
}
