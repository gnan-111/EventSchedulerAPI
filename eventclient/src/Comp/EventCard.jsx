import { Calendar, Clock, MapPin, Trash2, User } from 'lucide-react';
import { format } from 'date-fns';
import './EventCard.css';

const EventCard = ({ event, onDelete }) => {
  const startDate = event.startDate ? new Date(event.startDate) : null;
  const endDate = event.endDate ? new Date(event.endDate) : null;

  return (
    <div className="event-card">
      <div className="card-header">
        <div>
          <h3 className="card-title">{event.title}</h3>
          <p className="card-desc">{event.description}</p>
        </div>
        <button onClick={() => onDelete(event.id)} className="delete-btn">
          <Trash2 size={20} />
        </button>
      </div>

      <div className="card-body">
        <div className="info-row">
          <Calendar size={16} className="icon-primary" />
          <span>
            {startDate ? format(startDate, 'MMM dd, yyyy') : 'N/A'}
          </span>
        </div>

        <div className="info-row">
          <Clock size={16} className="icon-primary" />
          <span>
            {startDate && endDate
              ? `${format(startDate, 'h:mm a')} - ${format(endDate, 'h:mm a')}`
              : 'Time not available'}
          </span>
        </div>

        <div className="info-row">
          <MapPin size={16} className="icon-primary" />
          <span>{event.location || 'Remote'}</span>
        </div>
      </div>

      <div className="attendees-section">
        <div className="attendee-header">
          <User size={16} />
          <span>Attendees</span>
        </div>

        <div className="tags-container">
          {event.attendees && event.attendees.length > 0 ? (
            event.attendees.map((att, index) => (
              <span key={index} className="attendee-tag">
                {att.name}
              </span>
            ))
          ) : (
            <span className="card-desc no-attendees">
              No attendees
            </span>
          )}
        </div>
      </div>
    </div>
  );
};

export default EventCard;
