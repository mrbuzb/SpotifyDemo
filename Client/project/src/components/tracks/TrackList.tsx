import React, { useState, useEffect } from 'react';
import { Music, Search, Filter, Plus } from 'lucide-react';
import { Track } from '../../types';
import { trackService } from '../../services/api';
import TrackCard from './TrackCard';
import TrackFilters from './TrackFilters';
import { Link } from 'react-router-dom';

const TrackList: React.FC = () => {
  const [tracks, setTracks] = useState<Track[]>([]);
  const [filteredTracks, setFilteredTracks] = useState<Track[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');
  const [searchQuery, setSearchQuery] = useState('');
  const [showFilters, setShowFilters] = useState(false);

  useEffect(() => {
    fetchTracks();
  }, []);

  useEffect(() => {
    filterTracks();
  }, [tracks, searchQuery]);

  const fetchTracks = async () => {
    setIsLoading(true);
    setError('');

    try {
      const response = await trackService.getAllTracks();
      setTracks(response.data);
      setFilteredTracks(response.data);
    } catch (error: any) {
      setError(error.response?.data?.message || 'Failed to fetch tracks');
    } finally {
      setIsLoading(false);
    }
  };

  const filterTracks = () => {
    let filtered = tracks;

    if (searchQuery) {
      filtered = filtered.filter(track =>
        track.title.toLowerCase().includes(searchQuery.toLowerCase()) ||
        track.artistName.toLowerCase().includes(searchQuery.toLowerCase()) ||
        track.albumName.toLowerCase().includes(searchQuery.toLowerCase()) ||
        track.genre.toLowerCase().includes(searchQuery.toLowerCase())
      );
    }

    setFilteredTracks(filtered);
  };

  const handleGenreFilter = async (genre: string) => {
    if (genre === 'all') {
      setFilteredTracks(tracks);
      return;
    }

    try {
      const response = await trackService.getTracksByGenre(genre);
      setFilteredTracks(response.data);
    } catch (error: any) {
      setError('Failed to filter tracks by genre');
    }
  };

  const handleUserFilter = async () => {
    try {
      const response = await trackService.getTracksByUser();
      setFilteredTracks(response.data);
    } catch (error: any) {
      setError('Failed to filter tracks by user');
    }
  };

  const handleTrackDeleted = (deletedTrackId: number) => {
    setTracks(prev => prev.filter(track => track.id !== deletedTrackId));
    setFilteredTracks(prev => prev.filter(track => track.id !== deletedTrackId));
  };

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <Music className="w-16 h-16 text-purple-600 mx-auto mb-4 animate-spin" />
          <p className="text-gray-600 text-lg">Loading tracks...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
          <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between">
            <div className="mb-4 sm:mb-0">
              <h1 className="text-3xl font-bold text-gray-900 flex items-center">
                <Music className="w-8 h-8 text-purple-600 mr-3" />
                Music Library
              </h1>
              <p className="text-gray-600 mt-1">
                Discover and manage your music collection
              </p>
            </div>
            <Link
              to="/tracks/add"
              className="inline-flex items-center px-4 py-2 bg-gradient-to-r from-purple-600 to-blue-600 text-white rounded-lg hover:from-purple-700 hover:to-blue-700 transition-all transform hover:scale-105"
            >
              <Plus className="w-5 h-5 mr-2" />
              Add Track
            </Link>
          </div>
        </div>
      </div>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div className="mb-6 space-y-4">
          {/* Search Bar */}
          <div className="relative">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 w-5 h-5 text-gray-400" />
            <input
              type="text"
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              className="w-full pl-10 pr-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-purple-500 focus:border-transparent transition-all"
              placeholder="Search tracks, artists, albums, or genres..."
            />
          </div>

          {/* Filters */}
          <div className="flex items-center justify-between">
            <button
              onClick={() => setShowFilters(!showFilters)}
              className="flex items-center px-4 py-2 bg-white border border-gray-300 rounded-lg hover:bg-gray-50 transition-colors"
            >
              <Filter className="w-5 h-5 mr-2 text-gray-600" />
              Filters
            </button>
            <p className="text-gray-600">
              {filteredTracks.length} track{filteredTracks.length !== 1 ? 's' : ''}
            </p>
          </div>

          {showFilters && (
            <TrackFilters
              onGenreFilter={handleGenreFilter}
              onUserFilter={handleUserFilter}
              onShowAll={() => setFilteredTracks(tracks)}
            />
          )}
        </div>

        {error && (
          <div className="bg-red-50 text-red-600 p-4 rounded-lg mb-6">
            {error}
          </div>
        )}

        {filteredTracks.length === 0 ? (
          <div className="text-center py-12">
            <Music className="w-24 h-24 text-gray-300 mx-auto mb-4" />
            <h3 className="text-xl font-semibold text-gray-900 mb-2">No tracks found</h3>
            <p className="text-gray-600 mb-6">
              {searchQuery ? 'Try adjusting your search terms' : 'Start by adding your first track'}
            </p>
            <Link
              to="/tracks/add"
              className="inline-flex items-center px-6 py-3 bg-gradient-to-r from-purple-600 to-blue-600 text-white rounded-lg hover:from-purple-700 hover:to-blue-700 transition-all transform hover:scale-105"
            >
              <Plus className="w-5 h-5 mr-2" />
              Add Your First Track
            </Link>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            {filteredTracks.map((track) => (
              <TrackCard
                key={track.id}
                track={track}
                onDelete={handleTrackDeleted}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default TrackList;