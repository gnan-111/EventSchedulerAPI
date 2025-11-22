import { useState } from 'react';
import { X, Plus } from 'lucide-react';
import './EventForm.css';

const EventForm = ({ onClose, onSubmit }) => {
  const [formData, setFormData] = useState({
    title: '', description: '', location: '', startTime: '', endTime: '',
    attendees: []
  });

  const [attendeeInput, setAttendeeInput] = useState({ name: '', email: '' });

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const addAttendee = () => {
    if (!attendeeInput.name || !attendeeInput.email) return;
    setFormData({
      ...formData,
      attendees: [...formData.attendees, attendeeInput]
    });
    setAttendeeInput({ name: '', email: '' });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if(!formData.title || !formData.startTime) return;
    onSubmit(formData);
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <div className="modal-header">
          <h2>New Event</h2>
          <button onClick={onClose}><X size={20} /></button>
        </div>

        <form onSubmit={handleSubmit} className="modal-body">
          <input name="title" placeholder="Event Title" onChange={handleChange} required />
          <textarea name="description" placeholder="Description" onChange={handleChange} rows="2" />
          <input name="location" placeholder="Location" onChange={handleChange} />
          
          <div className="grid-row">
            <div>
              <label className="label-text">Start Time</label>
              <input type="datetime-local" name="startTime" onChange={handleChange} required />
            </div>
            <div>
              <label className="label-text">End Time</label>
              <input type="datetime-local" name="endTime" onChange={handleChange} required />
            </div>
          </div>

          <div className="attendee-box">
            <h4 style={{fontSize: '0.9rem', marginBottom: '0.5rem'}}>Add Attendees</h4>
            <div className="input-group">
              <input 
                placeholder="Name" 
                value={attendeeInput.name}
                onChange={e => setAttendeeInput({...attendeeInput, name: e.target.value})}
                style={{marginBottom: 0}}
              />
              <input 
                placeholder="Email" 
                value={attendeeInput.email}
                onChange={e => setAttendeeInput({...attendeeInput, email: e.target.value})}
                style={{marginBottom: 0}}
              />
              <button type="button" onClick={addAttendee} className="btn-icon">
                <Plus size={16} />
              </button>
            </div>
            <ul className="attendee-list">
              {formData.attendees.map((att, i) => (
                <li key={i}>{att.name} ({att.email})</li>
              ))}
            </ul>
          </div>

          <button type="submit" className="btn-submit">Create Event</button>
        </form>
      </div>
    </div>
  );
};

export default EventForm;