using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using RoomBooking.Models;
using RoomBooking.Services;
using System.Linq;

namespace RoomBooking
{
    public partial class MainWindow : Window
    {
        private ICollectionView _roomsView;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = RoomService.Rooms;
            RoomsList.ItemsSource = RoomService.Rooms;
            _roomsView = CollectionViewSource.GetDefaultView(RoomService.Rooms);

            this.DataContext = this;
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsList.SelectedItem is not Room sala)
            {
                MessageBox.Show("Proszę najpierw wybrać salę z listy po lewej stronie.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (sala.Status != "WOLNA")
            {
                MessageBox.Show("Wybrana sala jest już zajęta. Wybierz inną.", "Sala zajęta", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string imie = txtImie.Text.Trim();
            string nazwisko = txtNazwisko.Text.Trim();

            if (string.IsNullOrWhiteSpace(imie))
            {
                MessageBox.Show("Proszę wpisać imię.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtImie.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(nazwisko))
            {
                MessageBox.Show("Proszę wpisać nazwisko.", "Brak danych", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNazwisko.Focus(); 
                return;
            }

            string sprawdzanyStatus = $"Zajęta przez: {imie} {nazwisko}";
            bool maJuzSale = RoomService.Rooms.Any(r => r.Status.Equals(sprawdzanyStatus, System.StringComparison.OrdinalIgnoreCase));

            if (maJuzSale)
            {
                MessageBox.Show("Masz już zarezerwowaną inną salę! Najpierw anuluj poprzednią rezerwację.", "Limit rezerwacji", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            sala.Status = sprawdzanyStatus;

            txtImie.Clear();
            txtNazwisko.Clear();

            _roomsView.Refresh();

            MessageBox.Show($"Sala {sala.Name} została pomyślnie zarezerwowana dla: {imie} {nazwisko}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var sala = RoomsList.SelectedItem as Room;
            if (sala != null)
            {
                sala.Status = "WOLNA";
                _roomsView.Refresh();
            }
        }

        private void FilterCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_roomsView == null) return;

            string filter = (FilterCombo.SelectedItem as ComboBoxItem)?.Content.ToString();

            _roomsView.Filter = obj =>
            {
                var room = obj as Room;
                if (room == null) return false;

                if (filter == "Wszystkie") return true;
                if (filter == "WOLNA") return room.Status == "WOLNA";

                return room.Status != "WOLNA";
            };
        }
    }
}
