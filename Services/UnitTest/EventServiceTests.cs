using System;
using System.Linq;
using System.Threading.Tasks;
using EventScheduler.Data;
using EventScheduler.Services;
using EventSchedulerAPI.DTOs;
using EventSchedulerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EventScheduler.Tests.Services
{
    public class EventServiceTests
    {
        // Helper method to create a unique In-Memory database for each test
        // This ensures tests don't interfere with each other's data.
        private AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetEventsAsync_ShouldReturnAllEvents()
        {
            // Arrange
            using var context = GetInMemoryContext();
            // Note: Removed explicit Id=1 and Id=2 to rely on auto-generation
            context.Events.Add(new Event
            {
                Title = "Test Event 1",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddHours(1),
                Location = "Room A", // Required property is set
                Description = "Desc 1"
            });
            context.Events.Add(new Event
            {
                Title = "Test Event 2",
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(1).AddHours(1),
                Location = "Room B", // Required property is set
                Description = "Desc 2"
            });
            await context.SaveChangesAsync();

            var service = new EventService(context);

            // Act
            var result = await service.GetEventsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var firstEvent = result.First();
            Assert.Equal("Test Event 1", firstEvent.Title);
            Assert.Equal("Room A", firstEvent.Location);
        }

        [Fact]
        public async Task GetEventByIdAsync_ShouldReturnEvent_WhenIdExists()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var expectedEvent = new Event
            {
                // Removed explicit Id=10. Let the DB generate it.
                Title = "Specific Event",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
                Location = "Online", // Required property is set
                Description = "Testing ID"
            };
            context.Events.Add(expectedEvent);
            await context.SaveChangesAsync();
            // Get the ID the DB assigned
            var generatedId = expectedEvent.Id;

            var service = new EventService(context);

            // Act
            var result = await service.GetEventByIdAsync(generatedId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(generatedId, result.Id); // Assert against the generated ID
            Assert.Equal("Specific Event", result.Title);
        }

        [Fact]
        public async Task GetEventByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var service = new EventService(context);

            // Act
            var result = await service.GetEventByIdAsync(999); // ID doesn't exist

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateEventAsync_ShouldAddEventToDatabase()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var service = new EventService(context);

            var newEventDto = new EventDto
            {
                Title = "New Created Event",
                Description = "Created via Test",
                Location = "Lab 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            // Act
            var result = await service.CreateEventAsync(newEventDto);

            // Assert
            // 1. Verify the return object
            Assert.NotNull(result);
            Assert.True(result.Id > 0); // ID should be generated
            Assert.Equal("New Created Event", result.Title);

            // 2. Verify the database state directly
            var dbEvent = await context.Events.FindAsync(result.Id);
            Assert.NotNull(dbEvent);
            Assert.Equal("Lab 1", dbEvent.Location);
        }

        [Fact]
        public async Task DeleteEventAsync_ShouldReturnTrue_WhenEventExists()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var eventToDelete = new Event
            {
                // Removed explicit Id=5. Let the DB generate it.
                Title = "To Delete",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "Meeting Room" // <-- FIX: Added the required 'Location' property
            };
            context.Events.Add(eventToDelete);
            await context.SaveChangesAsync();
            // Capture the actual ID assigned by the database
            var generatedId = eventToDelete.Id;

            var service = new EventService(context);

            // Act
            var result = await service.DeleteEventAsync(generatedId);

            // Assert
            Assert.True(result); // Method returns true

            // Verify it is actually deleted from DB
            var deletedEvent = await context.Events.FindAsync(generatedId);
            Assert.Null(deletedEvent);
        }

        [Fact]
        public async Task DeleteEventAsync_ShouldReturnFalse_WhenEventDoesNotExist()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var service = new EventService(context);

            // Act
            var result = await service.DeleteEventAsync(100); // ID 100 doesn't exist

            // Assert
            Assert.False(result);
        }
    }
}