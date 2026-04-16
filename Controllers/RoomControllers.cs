using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s32509.Models;

namespace PJATK_APBD_Cw5_s32509.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var rooms = DataStore.Rooms.AsQueryable();
            
            if (minCapacity.HasValue) rooms = rooms.Where(r => r.Capacity >= minCapacity);
            if (hasProjector.HasValue) rooms = rooms.Where(r => r.HasProjector == hasProjector);
            if (activeOnly == true) rooms = rooms.Where(r => r.IsActive);
            
            return Ok(rooms.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public IActionResult GetByBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));
            return Ok(rooms.ToList());
        }

        [HttpPost]
        public IActionResult Create(Room room)
        {
            room.Id = DataStore.Rooms.Count > 0 ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);
            
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Room updatedRoom)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Capacity = updatedRoom.Capacity;
            room.Floor = updatedRoom.Floor;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            if (DataStore.Reservations.Any(res => res.RoomId == id))
            {
                return Conflict("Cannot delete room with existing reservations.");
            }

            DataStore.Rooms.Remove(room);
            return NoContent();
        }
    }
}