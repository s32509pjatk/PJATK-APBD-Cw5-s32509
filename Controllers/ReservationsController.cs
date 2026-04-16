using Microsoft.AspNetCore.Mvc;
using PJATK_APBD_Cw5_s32509.Models; 

namespace PJATK_APBD_Cw5_s32509.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFiltered([FromQuery] DateTime? date, [FromQuery] string status, [FromQuery] int? roomId)
        {
            var query = DataStore.Reservations.AsQueryable();
            
            if (date.HasValue) query = query.Where(r => r.Date.Date == date.Value.Date);
            if (!string.IsNullOrEmpty(status)) query = query.Where(r => r.Status == status);
            if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create(Reservation res)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == res.RoomId);
            
            if (room == null) return BadRequest("Room does not exist.");
            if (!room.IsActive) return BadRequest("Room is inactive.");


            bool isOverlapping = DataStore.Reservations.Any(r => 
                r.RoomId == res.RoomId && 
                r.Date.Date == res.Date.Date &&
                res.StartTime < r.EndTime && r.StartTime < res.EndTime);

            if (isOverlapping) return Conflict("Time slot is already occupied.");

            res.Id = DataStore.Reservations.Count > 0 ? DataStore.Reservations.Max(r => r.Id) + 1 : 1;
            DataStore.Reservations.Add(res);
            
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Reservation updatedRes)
        {
            var existingRes = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (existingRes == null) return NotFound();

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == updatedRes.RoomId);
            if (room == null) return BadRequest("Room does not exist.");
            if (!room.IsActive) return BadRequest("Room is inactive.");

            bool isOverlapping = DataStore.Reservations.Any(r => 
                r.Id != id && 
                r.RoomId == updatedRes.RoomId && 
                r.Date.Date == updatedRes.Date.Date &&
                updatedRes.StartTime < r.EndTime && r.StartTime < updatedRes.EndTime);

            if (isOverlapping) return Conflict("Time slot is already occupied.");

          
            existingRes.RoomId = updatedRes.RoomId;
            existingRes.OrganizerName = updatedRes.OrganizerName;
            existingRes.Topic = updatedRes.Topic;
            existingRes.Date = updatedRes.Date;
            existingRes.StartTime = updatedRes.StartTime;
            existingRes.EndTime = updatedRes.EndTime;
            existingRes.Status = updatedRes.Status;

            return Ok(existingRes);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            
            DataStore.Reservations.Remove(res);
            return NoContent();
        }
    }
}