using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MusicAppXamarin.ViewModel
{
    public class BaseViewModel
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public BaseViewModel()
        {
        }
    }
}
