import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';
import Navigation from './components/layout/Navigation';

// Auth Components
import SignUp from './components/auth/SignUp';
import VerifyCode from './components/auth/VerifyCode';
import Login from './components/auth/Login';

// Dashboard
import Dashboard from './components/dashboard/Dashboard';

// Track Components
import TrackList from './components/tracks/TrackList';
import TrackDetail from './components/tracks/TrackDetail';
import AddTrack from './components/tracks/AddTrack';
import EditTrack from './components/tracks/EditTrack';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          {/* Public Routes */}
          <Route path="/signup" element={<SignUp />} />
          <Route path="/verify-code" element={<VerifyCode />} />
          <Route path="/login" element={<Login />} />

          {/* Protected Routes */}
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute>
                <Navigation />
                <Dashboard />
              </ProtectedRoute>
            }
          />
          <Route
            path="/tracks"
            element={
              <ProtectedRoute>
                <Navigation />
                <TrackList />
              </ProtectedRoute>
            }
          />
          <Route
            path="/tracks/add"
            element={
              <ProtectedRoute>
                <Navigation />
                <AddTrack />
              </ProtectedRoute>
            }
          />
          <Route
            path="/tracks/:id"
            element={
              <ProtectedRoute>
                <Navigation />
                <TrackDetail />
              </ProtectedRoute>
            }
          />
          <Route
            path="/tracks/:id/edit"
            element={
              <ProtectedRoute>
                <Navigation />
                <EditTrack />
              </ProtectedRoute>
            }
          />

          {/* Default Redirects */}
          <Route path="/" element={<Navigate to="/dashboard" replace />} />
          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;