using System.ComponentModel;

namespace Depra.EventSystem.Tests
{
    public class TestMyClassWithINotifyPropertyChangedInterface : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int _id;

        public int Id
        {
            get => _id;
            set { 
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
    }
}