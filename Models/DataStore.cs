using PJATK_APBD_Cw5_s32509.Models;

namespace PJATK_APBD_Cw5_s32509
{
    public static class DataStore
    {
        public static List<Room> Rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 15, HasProjector = true, IsActive = true },
            new Room { Id = 2, Name = "Aula Magna", BuildingCode = "A", Floor = 1, Capacity = 100, HasProjector = true, IsActive = true },
            new Room { Id = 3, Name = "Salka B", BuildingCode = "B", Floor = 2, Capacity = 8, HasProjector = false, IsActive = true },
            new Room { Id = 4, Name = "Magazyn", BuildingCode = "C", Floor = 0, Capacity = 50, HasProjector = false, IsActive = false }
        };

        public static List<Reservation> Reservations = new List<Reservation>
        {
            new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "Wstęp do C#", Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "confirmed" },
            new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "Wykład REST API", Date = DateTime.Parse("2026-05-10"), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 30, 0), Status = "planned" }
        };
    }
}