import React, { useState, useEffect } from 'react';
import { useParams, Link, useNavigate } from 'react-router-dom';
import { ArrowLeft, Edit, Trash2,User,Calendar, Disc3, Music } from 'lucide-react';
import { Track } from '../../types';
import { trackService } from '../../services/api';
import { useAuth } from '../../context/AuthContext';
import api from "../../services/api";
import TrackPlayer from "../../components/tracks/TrackPlayer"; // ✅ Yangi qo‘shildi

const TrackDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { user } = useAuth();
  const [track, setTrack] = useState<Track | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [isDeleting, setIsDeleting] = useState(false);
  const [showDeleteConfirm, setShowDeleteConfirm] = useState(false);

  useEffect(() => {
    if (id) {
      fetchTrack(parseInt(id));
    }
  }, [id]);

  const fetchTrack = async (trackId: number) => {
    setIsLoading(true);
    setError('');

    try {
      const response = await trackService.getTrackById(trackId);
      setTrack(response.data);
    } catch (error: any) {
      setError(error.response?.data?.message || 'Failed to fetch track');
    } finally {
      setIsLoading(false);
    }
  };

  const addToHistory = async (trackId: number) => {
    try {
      await api.post(`/api/user-track-history/add-play/${trackId}`);
      console.log("Track play saved");
    } catch (error) {
      console.error("Failed to save track play:", error);
    }
  };

  const handleDelete = async () => {
    if (!track) return;
    
    setIsDeleting(true);
    try {
      await trackService.deleteTrack(track.id);
      navigate('/tracks');
    } catch (error: any) {
      setError(error.response?.data?.message || 'Failed to delete track');
    } finally {
      setIsDeleting(false);
      setShowDeleteConfirm(false);
    }
  };

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <Music className="w-16 h-16 text-purple-600 mx-auto mb-4 animate-spin" />
          <p className="text-gray-600 text-lg">Loading track...</p>
        </div>
      </div>
    );
  }

  if (error || !track) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <div className="bg-red-100 w-16 h-16 rounded-full flex items-center justify-center mx-auto mb-4">
            <Music className="w-8 h-8 text-red-600" />
          </div>
          <h2 className="text-2xl font-bold text-gray-900 mb-2">Track Not Found</h2>
          <p className="text-gray-600 mb-6">{error || 'The track you\'re looking for doesn\'t exist.'}</p>
          <Link
            to="/tracks"
            className="inline-flex items-center px-4 py-2 bg-purple-600 text-white rounded-lg hover:bg-purple-700 transition-colors"
          >
            <ArrowLeft className="w-4 h-4 mr-2" />
            Back to Tracks
          </Link>
        </div>
      </div>
    );
  }

  const isOwner = user?.userId === track.uploadedById;

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-gradient-to-r from-purple-600 to-blue-600 text-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          <Link
            to="/tracks"
            className="inline-flex items-center text-white/80 hover:text-white mb-6 transition-colors"
          >
            <ArrowLeft className="w-5 h-5 mr-2" />
            Back to Tracks
          </Link>

          <div className="flex flex-col lg:flex-row lg:items-end lg:space-x-8">
            {/* Track Artwork */}
            <div className="flex-shrink-0 mb-6 lg:mb-0">
              <div className="w-64 h-64 bg-white/10 rounded-lg flex items-center justify-center backdrop-blur-sm">
                <Disc3 className="w-32 h-32 text-white/70" />
              </div>
            </div>

            {/* Track Info */}
            <div className="flex-1">
              <p className="text-white/80 text-sm uppercase tracking-wide font-medium mb-2">
                Track
              </p>
              <h1 className="text-4xl lg:text-6xl font-bold mb-4">{track.title}</h1>
              <div className="flex items-center space-x-4 text-white/90">
                <span className="font-semibold">{track.artistName}</span>
                <span>•</span>
                <span>{track.albumName}</span>
                <span>•</span>
                <span>{new Date(track.releaseDate).getFullYear()}</span>
              </div>
              <div className="mt-4">
                <span className="inline-block px-3 py-1 bg-white/20 backdrop-blur-sm text-white text-sm font-medium rounded-full">
                  {track.genre}
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Action Buttons */}
        <div className="flex flex-wrap items-center gap-4 mb-8">
          {/* ✅ Endi TrackPlayer ishlatilyapti */}
          {track.audioUrl && (
            <TrackPlayer
  url={track.audioUrl}
  onPlay={() => addToHistory(track.id)} // endi bu aniq ishlaydi
/>
          )}

          {isOwner && (
            <>
              <Link
                to={`/tracks/${track.id}/edit`}
                className="flex items-center px-6 py-3 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-semibold transition-all transform hover:scale-105"
              >
                <Edit className="w-5 h-5 mr-2" />
                Edit Track
              </Link>
              <button
                onClick={() => setShowDeleteConfirm(true)}
                className="flex items-center px-6 py-3 bg-red-600 hover:bg-red-700 text-white rounded-lg font-semibold transition-all transform hover:scale-105"
              >
                <Trash2 className="w-5 h-5 mr-2" />
                Delete Track
              </button>
            </>
          )}
        </div>

        {/* Track Details */}
        <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
          <h2 className="text-2xl font-bold text-gray-900 mb-6">Track Information</h2>
          
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div className="space-y-4">
              <div className="flex items-center">
                <Music className="w-5 h-5 text-gray-400 mr-3" />
                <div>
                  <p className="text-sm text-gray-500">Title</p>
                  <p className="font-semibold text-gray-900">{track.title}</p>
                </div>
              </div>

              <div className="flex items-center">
                <User className="w-5 h-5 text-gray-400 mr-3" />
                <div>
                  <p className="text-sm text-gray-500">Artist</p>
                  <p className="font-semibold text-gray-900">{track.artistName}</p>
                </div>
              </div>

              <div className="flex items-center">
                <Disc3 className="w-5 h-5 text-gray-400 mr-3" />
                <div>
                  <p className="text-sm text-gray-500">Album</p>
                  <p className="font-semibold text-gray-900">{track.albumName}</p>
                </div>
              </div>
            </div>

            <div className="space-y-4">
              <div className="flex items-center">
                <Calendar className="w-5 h-5 text-gray-400 mr-3" />
                <div>
                  <p className="text-sm text-gray-500">Release Date</p>
                  <p className="font-semibold text-gray-900">
                    {new Date(track.releaseDate).toLocaleDateString()}
                  </p>
                </div>
              </div>

              <div>
                <p className="text-sm text-gray-500 mb-1">Genre</p>
                <span className="inline-block px-3 py-1 bg-purple-100 text-purple-800 text-sm font-medium rounded-full">
                  {track.genre}
                </span>
              </div>
            </div>
          </div>
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

export default TrackDetail;
