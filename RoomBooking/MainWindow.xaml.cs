using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace RoomBooking
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Room> Rooms { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Rooms = new ObservableCollection<Room>
    {
        new Room { Name = "Sala A320", Status = "WOLNA" },
        new Room { Name = "Sala B313", Status = "WOLNA" },
        new Room { Name = "Aula L120", Status = "WOLNA" },
        new Room { Name = "Laboratorium Komputerowe", Status = "WOLNA" },
        new Room { Name = "Hala sportowa", Status = "WOLNA" },
        new Room { Name = "Laboratorium Chemiczne", Status = "WOLNA" },
        new Room { Name = "Pokój Konsultacyjny 10", Status = "WOLNA" },
        new Room { Name = "Pokój Konsultacyjny 11", Status = "WOLNA" },
        new Room { Name = "Sala Konferencyjna (Rektorat)", Status = "WOLNA" },
        new Room { Name = "Mała Sala Spotkań", Status = "WOLNA" }
    };

            this.DataContext = this;
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            var sala = RoomsList.SelectedItem as Room;
            if (sala == null) return;

            //czy wybrana sala jest wolna
            if (sala.Status != "WOLNA")
            {
                MessageBox.Show("Ta sala jest już zajęta!");
                return;
            }

            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();

            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko))
            {
                MessageBox.Show("Najpierw wpisz imię i nazwisko!");
                return;
            }

            string sprawdzanyStatus = "Zajęta przez: " + imie + " " + nazwisko;

            //Sprawdzamy czy OSOBA ma już salę
            foreach (var r in Rooms)
            {
                if (r.Status == sprawdzanyStatus)
                {
                    MessageBox.Show("Masz już zarezerwowaną inną salę!");
                    return; 
                }
            }

            //Zapis sali na osobę
            sala.Status = sprawdzanyStatus;
            txtImie.Clear();
            txtNazwisko.Clear();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var sala = RoomsList.SelectedItem as Room;
            if (sala != null)
            {
                sala.Status = "WOLNA";
            }
        }
    }

    public class Room : INotifyPropertyChanged
    {
        private string _status;
        public string Name { get; set; }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
