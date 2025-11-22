using EventScheduler.Data;
using EventSchedulerAPI.DTOs;
using EventSchedulerAPI.Models;
using EventSchedulerAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EventScheduler.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<EventDto>> GetEventsAsync()
        {
            return await _context.Events
                .Select(e => new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    Location = e.Location,
                    Description = e.Description
                })
                .ToListAsync();
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null) return null;
            return new EventDto
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Location = e.Location,
                Description = e.Description
            };
        }

        public async Task<EventDto> CreateEventAsync(EventDto dto)
        {
            var e = new Event
            {
                Title = dto.Title,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                Description = dto.Description
            };

            _context.Events.Add(e);
            await _context.SaveChangesAsync();
            dto.Id = e.Id;
            return dto;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null) return false;

            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}