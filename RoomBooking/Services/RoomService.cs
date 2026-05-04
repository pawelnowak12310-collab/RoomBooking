using System.Collections.ObjectModel;
using RoomBooking.Models;

namespace RoomBooking.Services
{
    public static class RoomService
    {
        public static ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>
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
    }
}