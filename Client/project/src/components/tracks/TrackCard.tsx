import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Edit, Trash2, Calendar, User, Disc3 } from 'lucide-react';
import { Track } from '../../types';
import { trackService } from '../../services/api';
import { useAuth } from '../../context/AuthContext';

interface TrackCardProps {
  track: Track;
  onDelete: (trackId: number) => void;
}

const TrackCard: React.FC<TrackCardProps> = ({ track, onDelete }) => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [isDeleting, setIsDeleting] = useState(false);
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);

  const handleDelete = async () => {
    setIsDeleting(true);
    try {
      await trackService.deleteTrack(track.id);
      onDelete(track.id);
    } catch (error: any) {
      console.error('Delete failed:', error);
    } finally {
      setIsDeleting(false);
      setShowDeleteConfirm(false);
    }
  };

  // Play bosilganda detail sahifaga o'tkazish
  const handlePlayClick = () => {
    navigate(`/tracks/${track.id}`);
  };

  const isOwner = user?.userId === track.uploadedById;

  return (
    <div className="bg-white rounded-xl shadow-sm border border-gray-200 overflow-hidden hover:shadow-lg transition-all duration-300 transform hover:-translate-y-1">
      {/* Track Image Placeholder */}
      <div className="h-48 bg-gradient-to-br from-purple-400 to-blue-500 flex items-center justify-center relative group">
        <Disc3 className="w-16 h-16 text-white/70" />
        <div className="absolute inset-0 bg-black/20 opacity-0 group-hover:opacity-100 transition-opacity flex items-center justify-center">
          <button
            onClick={handlePlayClick}
            className="p-3 bg-white/90 rounded-full hover:bg-white transition-colors"
          >
          </button>
        </div>
      </div>

      <div className="p-5">
        <div className="mb-3">
          <h3 className="font-bold text-lg text-gray-900 mb-1 truncate">
            {track.title}
          </h3>
          <p className="text-gray-600 text-sm flex items-center">
            <User className="w-4 h-4 mr-1" />
            {track.artistName}
          </p>
        </div>

        <div className="space-y-2 mb-4">
          <div className="flex items-center text-sm text-gray-500">
            <Disc3 className="w-4 h-4 mr-2" />
            <span>{track.albumName}</span>
          </div>
          <div className="flex items-center text-sm text-gray-500">
            <Calendar className="w-4 h-4 mr-2" />
            <span>{new Date(track.releaseDate).getFullYear()}</span>
          </div>
        </div>

        <div className="mb-4">
          <span className="inline-block px-3 py-1 bg-purple-100 text-purple-800 text-sm font-medium rounded-full">
            {track.genre}
          </span>
        </div>

        <div className="flex items-center justify-between">
          <Link
            to={`/tracks/${track.id}`}
            className="text-blue-600 hover:text-blue-700 font-medium text-sm"
          >
            View Details
          </Link>

          {isOwner && (
            <div className="flex space-x-2">
              <Link
                to={`/tracks/${track.id}/edit`}
                className="p-2 text-gray-600 hover:text-blue-600 hover:bg-blue-50 rounded-lg transition-colors"
              >
                <Edit className="w-4 h-4" />
              </Link>
              <button
                onClick={() => setShowDeleteConfirm(true)}
                className="p-2 text-gray-600 hover:text-red-600 hover:bg-red-50 rounded-lg transition-colors"
                disabled={isDeleting}
              >
                <Trash2 className="w-4 h-4" />
              </button>
            </div>
          )}
        </div>
      </div>

      {/* Delete Confirmation Modal */}
      {showDeleteConfirm && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-lg p-6 max-w-md w-full">
            <h3 className="text-lg font-semibold text-gray-900 mb-4">
              Delete Track
            </h3>
            <p className="text-gray-600 mb-6">
              Are you sure you want to delete "{track.title}"? This action cannot be undone.
            </p>
            <div className="flex space-x-3">
              <button
                onClick={() => setShowDeleteConfirm(false)}
                className="flex-1 px-4 py-2 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-colors"
              >
                Cancel
              </button>
              <button
                onClick={handleDelete}
                disabled={isDeleting}
                className="flex-1 px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 disabled:opacity-50 transition-colors"
              >
                {isDeleting ? 'Deleting...' : 'Delete'}
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default TrackCard;
