import axios from 'axios';

const Api = axios.create({
    baseURL: 'http://localhost:5167/Api', // use HTTP sem o S
});

// Adicionar token JWT nas requisições (se necessário futuramente)
Api.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default Api;
