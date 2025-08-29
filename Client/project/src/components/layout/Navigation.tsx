import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { Music, Home, Plus, User, LogOut } from 'lucide-react';
import { useAuth } from '../../context/AuthContext';

const Navigation: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const handleLogout = async () => {
    await logout();
    navigate('/login');
  };

  const isActive = (path: string) => location.pathname === path;

  return (
    <nav className="bg-white shadow-sm border-b">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-16">
          {/* Logo */}
          <Link to="/dashboard" className="flex items-center space-x-2">
            <div className="bg-gradient-to-r from-purple-600 to-blue-600 p-2 rounded-lg">
              <Music className="w-6 h-6 text-white" />
            </div>
            <span className="text-xl font-bold text-gray-900">MusicApp</span>
          </Link>

          {/* Navigation Links */}
          <div className="hidden md:flex items-center space-x-8">
            <Link
              to="/dashboard"
              className={`flex items-center px-3 py-2 rounded-lg transition-colors ${
                isActive('/dashboard')
                  ? 'bg-purple-100 text-purple-700'
                  : 'text-gray-600 hover:text-gray-900'
              }`}
            >
              <Home className="w-5 h-5 mr-2" />
              Dashboard
            </Link>
            <Link
              to="/tracks"
              className={`flex items-center px-3 py-2 rounded-lg transition-colors ${
                isActive('/tracks') || location.pathname.startsWith('/tracks')
                  ? 'bg-purple-100 text-purple-700'
                  : 'text-gray-600 hover:text-gray-900'
              }`}
            >
              <Music className="w-5 h-5 mr-2" />
              Music Library
            </Link>
            <Link
              to="/tracks/add"
              className={`flex items-center px-3 py-2 rounded-lg transition-colors ${
                isActive('/tracks/add')
                  ? 'bg-purple-100 text-purple-700'
                  : 'text-gray-600 hover:text-gray-900'
              }`}
            >
              <Plus className="w-5 h-5 mr-2" />
              Add Track
            </Link>
          </div>

          {/* User Menu */}
          <div className="flex items-center space-x-4">
            <div className="flex items-center space-x-2">
              <User className="w-5 h-5 text-gray-600" />
              <span className="text-gray-700 font-medium">{user?.userName}</span>
            </div>
            <button
              onClick={handleLogout}
              className="flex items-center px-3 py-2 text-gray-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors"
            >
              <LogOut className="w-5 h-5 mr-2" />
              Logout
            </button>
          </div>
        </div>
      </div>

      {/* Mobile Navigation */}
      <div className="md:hidden border-t">
        <div className="flex items-center justify-around py-2">
          <Link
            to="/dashboard"
            className={`flex flex-col items-center py-2 px-3 ${
              isActive('/dashboard') ? 'text-purple-700' : 'text-gray-600'
            }`}
          >
            <Home className="w-5 h-5" />
            <span className="text-xs mt-1">Home</span>
          </Link>
          <Link
            to="/tracks"
            className={`flex flex-col items-center py-2 px-3 ${
              isActive('/tracks') || location.pathname.startsWith('/tracks')
                ? 'text-purple-700'
                : 'text-gray-600'
            }`}
          >
            <Music className="w-5 h-5" />
            <span className="text-xs mt-1">Library</span>
          </Link>
          <Link
            to="/tracks/add"
            className={`flex flex-col items-center py-2 px-3 ${
              isActive('/tracks/add') ? 'text-purple-700' : 'text-gray-600'
            }`}
          >
            <Plus className="w-5 h-5" />
            <span className="text-xs mt-1">Add</span>
          </Link>
        </div>
      </div>
    </nav>
  );
};

export default Navigation;