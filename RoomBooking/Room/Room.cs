using System.ComponentModel;

namespace RoomBooking.Models
{
    public class Room : INotifyPropertyChanged
    {
        private string _status;
        public string Name { get; set; }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
