using EventSchedulerAPI.DTOs;
using EventSchedulerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetEvents()
        {
            return Ok(await _eventService.GetEventsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var evt = await _eventService.GetEventByIdAsync(id);
            if (evt == null) return NotFound();
            return Ok(evt);
        }

        [HttpPost]
        public async Task<ActionResult<EventDto>> CreateEvent(EventDto eventDto)
        {
            var created = await _eventService.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEvent), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var removed = await _eventService.DeleteEventAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}