import axios from 'axios';

const API_URL = "https://localhost:7124/api";

// EVENTS API CALLS
export const getEvents = async () => {
    const response = await axios.get(`${API_URL}/events`);
    return response.data;
};

export const getEventById = async (id) => {
    const response = await axios.get(`${API_URL}/events/${id}`);
    return response.data;
};

export const createEvent = async (eventData) => {
    const response = await axios.post(`${API_URL}/events`, eventData);
    return response.data;
};

export const deleteEvent = async (id) => {
    await axios.delete(`${API_URL}/events/${id}`);
};

// Optionally — for editing events in UI in future
export const updateEvent = async (id, eventData) => {
    const response = await axios.put(`${API_URL}/events/${id}`, eventData);
    return response.data;
};


// USERS API CALLS
export const registerUser = async (userData) => {
    const response = await axios.post(`${API_URL}/users/register`, userData);
    return response.data;
};

export const loginUser = async (loginData) => {
    const response = await axios.post(`${API_URL}/users/login`, loginData);
    return response.data;
};
