import { useEffect, useState } from 'react';
import { Plus } from 'lucide-react';
import { getEvents, createEvent, deleteEvent } from './axiosconfig';
import EventCard from './Comp/EventCard';
import EventForm from './Comp/EventForm';
import Navbar from './Comp/Navbar';
import './App.css';

function App() {
  const [events, setEvents] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [loading, setLoading] = useState(true);

  const fetchEvents = async () => {
    try {
      const data = await getEvents();
      setEvents(data);
    } catch (error) {
      console.error("Failed to fetch events", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchEvents();
  }, []);

  const handleCreate = async (eventData) => {
    try {
      await createEvent(eventData);
      setIsModalOpen(false);
      fetchEvents();
    } catch (error) {
      alert("Error creating event");
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm("Are you sure you want to delete this event?")) {
      await deleteEvent(id);
      setEvents(prev => prev.filter(e => e.id !== id));
    }
  };

  return (
    <>
      {/* Navbar */}
      <Navbar />

      {/* Main Wrapper */}
      <div className="app-container">

        {/* Header Section */}
        <div className="app-header">
          <div>
            <h1 className="app-title">Event Dashboard</h1>
            <p className="app-subtitle">Track team events and meeting schedules easily</p>
          </div>

          <button className="btn-primary" onClick={() => setIsModalOpen(true)}>
            <Plus size={20} /> Add Event
          </button>
        </div>

        {/* Events Display */}
        <div className="event-grid">
          {loading ? (
            <p className="empty-state">Fetching events...</p>
          ) : events.length === 0 ? (
            <p className="empty-state">No events yet! Add one ✨</p>
          ) : (
            events.map(event => (
              <EventCard 
                key={event.id}
                event={event}
                onDelete={handleDelete}
              />
            ))
          )}
        </div>

        {/* Modal */}
        {isModalOpen && (
          <EventForm
            onClose={() => setIsModalOpen(false)}
            onSubmit={handleCreate}
          />
        )}
      </div>
    </>
  );
}

export default App;
