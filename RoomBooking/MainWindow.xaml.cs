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

            // Zaktualizowana, dużo większa lista sal
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

            // 1. Sprawdzamy czy wybrana sala jest wolna
            if (sala.Status != "WOLNA")
            {
                MessageBox.Show("Ta sala jest już zajęta!");
                return;
            }

            // 2. Pobieramy i czyścimy ze spacji imię i nazwisko
            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();

            if (string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko))
            {
                MessageBox.Show("Najpierw wpisz imię i nazwisko!");
                return;
            }

            // Tworzymy dokładny napis, jakiego będziemy szukać u innych sal
            string sprawdzanyStatus = "Zajęta przez: " + imie + " " + nazwisko;

            // 3. Sprawdzamy, czy TA KONKRETNA OSOBA ma już salę
            foreach (var r in Rooms)
            {
                // Jeśli jakakolwiek sala ma dokładnie taki sam status (czyli to samo imię i nazwisko)
                if (r.Status == sprawdzanyStatus)
                {
                    MessageBox.Show("Masz już zarezerwowaną inną salę!");
                    return; // Przerywamy, nie pozwalamy zarezerwować
                }
            }

            // 4. Sukces: Zapisujemy salę na tę osobę
            sala.Status = sprawdzanyStatus;

            // Czyścimy formularz
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