import axios, { AxiosResponse } from 'axios';
import { 
  Track, 
  TrackCreateDto, 
  TrackUpdateDto, 
  UserCreateDto, 
  UserLoginDto, 
  AuthResponse,
  SendCodeRequest,
  ConfirmCodeRequest
} from '../types';




const API_BASE_URL = 'https://localhost:7064'; // Replace with your actual API URL



const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor to add auth token
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Response interceptor for token refresh
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      const refreshToken = localStorage.getItem('refreshToken');
      if (refreshToken) {
        try {
          const response = await authService.refreshToken(refreshToken);
          localStorage.setItem('accessToken', response.data.accessToken);
          localStorage.setItem('refreshToken', response.data.refreshToken);
          return api.request(error.config);
        } catch {
          localStorage.removeItem('accessToken');
          localStorage.removeItem('refreshToken');
          window.location.href = '/login';
        }
      }
    }
    return Promise.reject(error);
  }
);

// Auth Service
export const authService = {
  signUp: async (userData: UserCreateDto): Promise<AxiosResponse> => {
    return api.post('/api/auth/sign-up', userData);
  },

  sendCode: async (data: SendCodeRequest): Promise<AxiosResponse> => {
    return api.post('/api/auth/send-code', data);
  },

  confirmCode: async (data: ConfirmCodeRequest): Promise<AxiosResponse<AuthResponse>> => {
    return api.post('/api/auth/confirm-code', data);
  },

  login: async (credentials: UserLoginDto): Promise<AxiosResponse<AuthResponse>> => {
    return api.post('/api/auth/login', credentials);
  },

  refreshToken: async (refreshToken: string): Promise<AxiosResponse<AuthResponse>> => {
    return api.put('/api/auth/refresh-token', {
      refreshToken,
      accessToken: localStorage.getItem('accessToken')
    });
  },

  logout: async (refreshToken: string): Promise<AxiosResponse> => {
    return api.delete(`/api/auth/log-out?refreshToken=${refreshToken}`);
  }
};

// Track Service
export const trackService = {
  getAllTracks: async (): Promise<AxiosResponse<Track[]>> => {
    return api.get('/api/tracks');
  },

  getTrackById: async (id: number): Promise<AxiosResponse<Track>> => {
    return api.get(`/api/tracks/${id}`);
  },

  getTracksByUser: async (): Promise<AxiosResponse<Track[]>> => {
    return api.get('/api/tracks/by-user');
  },

  getTracksByGenre: async (genre: string): Promise<AxiosResponse<Track[]>> => {
    return api.get(`/api/tracks/by-genre/${genre}`);
  },

  createTrack: async (trackData: TrackCreateDto, audioFile: File) => {
    const formData = new FormData();

    Object.entries(trackData).forEach(([key, value]) => {
        formData.append(key, value as any);
    });

    formData.append("file", audioFile);

    return api.post("/api/tracks", formData, {
        headers: { "Content-Type": "multipart/form-data" }
    });
},


  updateTrack: async (trackData: TrackUpdateDto): Promise<AxiosResponse<Track>> => {
    return api.put('/api/tracks/update', trackData);
  },

  deleteTrack: async (id: number): Promise<AxiosResponse> => {
    return api.delete(`/api/tracks/${id}`);
  }
};

export default api;