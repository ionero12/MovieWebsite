import axios from 'axios';

const API_URL = 'http://localhost:5007/api';

const api = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
    withCredentials: true,
});

export const login = async (credentials) => {
    const response = await api.post('/user/login', credentials);
    return response.data;
};

export const logout = async () => {
    const response = await api.post('/user/logout');
    return response.data;
};

export const getMovies = async () => {
    const response = await api.get('/movie');
    return response.data;
};

export const getMoviesByStatus = async (status) => {
    const response = await api.get('/UserMovie/list', {
        params: { status }
    });
    return response.data;
}

export const getMovieById = async (movieId) => {
    const response = await api.get(`/movie/${movieId}`);
    return response.data;
};

export const setUserMovie = async (movieId, status) => {
    const response = await api.post('/UserMovie/add', null, {
        params: { movieId, status }
    });
    return response.data;
}

export const removeUserMovie = async (movieId, status) => {
    const response = await api.delete(`/UserMovie/remove`, {
        params: { movieId, status }
    });
    return response.data;
}

export const addMovieScore = async (movieId, score) => {
    const response = await api.post(`/UserMovie/score`, null, {
        params: { movieId, score }
        });
    return response.data;
}

export default api;