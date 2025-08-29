import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Music, Plus, TrendingUp } from 'lucide-react';
import { useAuth } from '../../context/AuthContext';
import api from "../../services/api";
import { Track } from '../../types';

interface TrackHistory {
  id: number;
  title: string;
  artistName: string;
  albumName: string;
  audioUrl: string;
  genre: string;
  releaseDate: string;
}



const Dashboard: React.FC = () => {
  const { user } = useAuth();
  const [history, setHistory] = useState<TrackHistory[]>([]);
  const [loading, setLoading] = useState(true);

  const [recommendedTracks, setRecommendedTracks] = useState<Track[]>([]);

  useEffect(() => {
    console.log("useEffect for recommendations ishladi ✅");
    api.get("/api/recommendations")
      .then((res) => {
        console.log("Recommended tracks:", res.data);
        setRecommendedTracks(res.data);
      })
      .catch((err) => {
        console.error("Failed to fetch recommendations:", err);
      });
  }, []);

  useEffect(() => {
    const fetchHistory = async () => {
      try {
        const response = await api.get("/api/user-track-history/history");
        setHistory(response.data);
      } catch (err) {
        console.error("Failed to fetch history:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchHistory();
  }, []);


  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-gradient-to-r from-purple-600 to-blue-600 text-white">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
          <h1 className="text-4xl font-bold mb-4">
            Welcome back, {user?.userName}!
          </h1>
          <p className="text-xl text-white/90">
            Manage your music library and discover new tracks
          </p>
        </div>
      </div>

      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Quick Actions */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-8">
          <Link
            to="/tracks"
            className="bg-white rounded-xl shadow-sm border border-gray-200 p-6 hover:shadow-lg transition-all transform hover:-translate-y-1 group"
          >
            <div className="flex items-center">
              <div className="bg-purple-100 p-3 rounded-lg group-hover:bg-purple-200 transition-colors">
                <Music className="w-8 h-8 text-purple-600" />
              </div>
              <div className="ml-4">
                <h3 className="text-lg font-semibold text-gray-900">Music Library</h3>
                <p className="text-gray-600">Browse all tracks</p>
              </div>
            </div>
          </Link>

          <Link
            to="/tracks/add"
            className="bg-white rounded-xl shadow-sm border border-gray-200 p-6 hover:shadow-lg transition-all transform hover:-translate-y-1 group"
          >
            <div className="flex items-center">
              <div className="bg-green-100 p-3 rounded-lg group-hover:bg-green-200 transition-colors">
                <Plus className="w-8 h-8 text-green-600" />
              </div>
              <div className="ml-4">
                <h3 className="text-lg font-semibold text-gray-900">Add Track</h3>
                <p className="text-gray-600">Upload new music</p>
              </div>
            </div>
          </Link>

          <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
            <div className="flex items-center">
              <div className="bg-blue-100 p-3 rounded-lg">
                <TrendingUp className="w-8 h-8 text-blue-600" />
              </div>
              <div className="ml-4">
                <h3 className="text-lg font-semibold text-gray-900">Recommended Musics</h3>

<div className="space-y-4">
  {recommendedTracks.length === 0 ? (
    <p className="text-gray-600">No recommendations available yet.</p>
  ) : (
    recommendedTracks.map((track) => (
      <div
        key={track.id}
        className="p-3 rounded-lg border border-gray-200 flex items-center justify-between hover:bg-gray-50"
      >
        <div>
          <h4 className="font-medium text-gray-800">{track.title}</h4>
          <p className="text-sm text-gray-600">{track.artistName}</p>
        </div>
        <Link
          to={`/tracks/${track.id}`}
          className="text-purple-600 hover:text-purple-800 text-sm font-medium"
        >
          View
        </Link>
      </div>
    ))
  )}
</div>
              </div>
            </div>
          </div>
        </div>

        {/* Recent Activity */}
        <div className="mt-8">
          <div className="bg-white rounded-xl shadow-sm border border-gray-200 p-6">
            <h3 className="text-xl font-semibold text-gray-900 mb-4">Recent Activity</h3>

            {loading ? (
              <p className="text-gray-500">Loading...</p>
            ) : history.length === 0 ? (
              <div className="text-center py-8">
                <Music className="w-16 h-16 text-gray-300 mx-auto mb-4" />
                <p className="text-gray-600">No recent activity</p>
                <p className="text-gray-500 text-sm mt-2">
                  Start by playing your first track!
                </p>
              </div>
            ) : (
              <ul className="divide-y divide-gray-100">
                {history.map((track) => (
                  <li key={track.id} className="py-4 flex justify-between items-center gap-4">
  <div className="flex-1">
    <p className="font-medium text-gray-900">{track.title}</p>
    <p className="text-sm text-gray-500">
      {track.artistName} • {track.albumName}
    </p>
    <p className="text-xs text-gray-400">
      Genre: {track.genre} | Released:{" "}
      {new Date(track.releaseDate).toLocaleDateString()}
    </p>
  </div>
  {/* audio player kengaytirilgan */}
  <audio controls src={track.audioUrl} className="w-64 md:w-80 lg:w-96" />
</li>

                ))}
              </ul>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
