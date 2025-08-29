import React from 'react';
import { Filter, User, Grid } from 'lucide-react';

interface TrackFiltersProps {
  onGenreFilter: (genre: string) => void;
  onUserFilter: () => void;
  onShowAll: () => void;
}

const TrackFilters: React.FC<TrackFiltersProps> = ({
  onGenreFilter,
  onUserFilter,
  onShowAll,
}) => {
  const genres = [
    'Rock', 'Pop', 'Jazz', 'Classical', 'Hip Hop', 'Electronic', 
    'Country', 'R&B', 'Indie', 'Alternative', 'Blues', 'Reggae'
  ];

  return (
    <div className="bg-white p-4 rounded-lg border border-gray-200 space-y-4">
      <div className="flex items-center mb-3">
        <Filter className="w-5 h-5 text-gray-600 mr-2" />
        <h3 className="font-semibold text-gray-900">Filter Options</h3>
      </div>

      <div className="space-y-4">
        {/* Quick Filters */}
        <div>
          <h4 className="text-sm font-medium text-gray-700 mb-2">Quick Filters</h4>
          <div className="flex flex-wrap gap-2">
            <button
              onClick={onShowAll}
              className="flex items-center px-3 py-1.5 bg-gray-100 hover:bg-gray-200 text-gray-700 rounded-full text-sm transition-colors"
            >
              <Grid className="w-4 h-4 mr-1" />
              All Tracks
            </button>
            <button
              onClick={onUserFilter}
              className="flex items-center px-3 py-1.5 bg-blue-100 hover:bg-blue-200 text-blue-700 rounded-full text-sm transition-colors"
            >
              <User className="w-4 h-4 mr-1" />
              My Tracks
            </button>
          </div>
        </div>

        {/* Genre Filters */}
        <div>
          <h4 className="text-sm font-medium text-gray-700 mb-2">Filter by Genre</h4>
          <div className="flex flex-wrap gap-2">
            {genres.map((genre) => (
              <button
                key={genre}
                onClick={() => onGenreFilter(genre)}
                className="px-3 py-1.5 bg-purple-100 hover:bg-purple-200 text-purple-700 rounded-full text-sm transition-colors"
              >
                {genre}
              </button>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default TrackFilters;