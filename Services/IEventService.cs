using EventSchedulerAPI.DTOs;

namespace EventSchedulerAPI.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetEventsAsync();
        Task<EventDto?> GetEventByIdAsync(int id);
        Task<EventDto> CreateEventAsync(EventDto eventDto);
        Task<bool> DeleteEventAsync(int id);
    }
}