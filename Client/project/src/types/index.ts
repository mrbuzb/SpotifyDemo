export interface User {
  userId: number;
  userName: string;
  email: string;
}

// interface TrackHistory {
//   id: number;
//   title: string;
//   artistName: string;
//   albumName: string;
//   audioUrl: string;
//   genre: string;
//   releaseDate: string;
// }

export interface Track {
  id: number;
  title: string;
  genre: string;
  artistName: string;
  albumName: string;
  releaseDate: string;
  audioUrl?: string;
  uploadedById?: number;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  user: User;
}

export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message?: string;
}

export interface TrackCreateDto {
  title: string;
  genre: string;
  artistName: string;
  releaseDate: string;
  albumName: string;
}

export interface TrackUpdateDto {
  id: number;
  title: string;
  genre: string;
  artistName: string;
  releaseDate: string;
  albumName: string;
}

export interface UserCreateDto {
  userName: string;
  email: string;
  password: string;
}

export interface UserLoginDto {
  userName: string;
  password: string;
}

export interface SendCodeRequest {
  email: string;
}

export interface ConfirmCodeRequest {
  email: string;
  code: string;
}