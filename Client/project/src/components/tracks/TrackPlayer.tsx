import React, { useRef, useState, useEffect } from "react";

interface TrackPlayerProps {
  url: string;
  onPlay?: () => void; // Play bosilganda callback
}

const TrackPlayer: React.FC<TrackPlayerProps> = ({ url, onPlay }) => {
  const audioRef = useRef<HTMLAudioElement>(null);
  const [isPlaying, setIsPlaying] = useState(false);
  const [currentTime, setCurrentTime] = useState(0);
  const [duration, setDuration] = useState(0);

  useEffect(() => {
    const audio = audioRef.current;
    if (!audio) return;

    const handleLoadedMetadata = () => setDuration(audio.duration);
    const handleTimeUpdate = () => setCurrentTime(audio.currentTime);
    const handleEnded = () => setIsPlaying(false);

    audio.addEventListener("loadedmetadata", handleLoadedMetadata);
    audio.addEventListener("timeupdate", handleTimeUpdate);
    audio.addEventListener("ended", handleEnded);

    return () => {
      audio.removeEventListener("loadedmetadata", handleLoadedMetadata);
      audio.removeEventListener("timeupdate", handleTimeUpdate);
      audio.removeEventListener("ended", handleEnded);
    };
  }, [url]);

  const togglePlay = async () => {
    const audio = audioRef.current;
    if (!audio) return;

    if (isPlaying) {
      audio.pause();
      setIsPlaying(false);
    } else {
      try {
        await audio.play();
        setIsPlaying(true);
        onPlay?.(); // 🔑 Play bosilganda callback ishlaydi
      } catch (err) {
        console.error("Audio o‘ynashda xatolik:", err);
      }
    }
  };

  return (
    <div className="space-y-2">
      <button
        onClick={togglePlay}
        className="px-4 py-2 bg-green-600 text-white rounded-lg"
      >
        {isPlaying ? "Pause" : "Play"}
      </button>

      <div className="text-sm text-gray-700">
        {Math.floor(currentTime)}s / {Math.floor(duration)}s
      </div>

      {/* 🔑 audio elementni qo‘shdik */}
      <audio ref={audioRef} src={url} preload="metadata" />
    </div>
  );
};

export default TrackPlayer;
