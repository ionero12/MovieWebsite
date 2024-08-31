import React, { useState, useEffect } from 'react';
import {addMovieScore, getMoviesByStatus, removeUserMovie} from '../../services/api';
import RatingModal from "./RatingModal";

function MovieList() {
    const [movies, setMovies] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedMovieId, setSelectedMovieId] = useState(null);
    const [rating, setRating] = useState(0);

    const fetchMovies = async () => {
        try {
            const data = await getMoviesByStatus('seen');
            setMovies(data);
        } catch (error) {
            console.error('Error fetching movies:', error);
        }
    };

    useEffect(() => {
        fetchMovies();
    }, []);

    const removeFromList = async (movieId, status) => {
        try {
            setIsLoading(true);
            await removeUserMovie(movieId, status);
            await fetchMovies();
        } catch (error) {
            console.error('Error removing movie from list:', error);
        } finally {
            setIsLoading(false);
        }
    };

    const openRatingModal = (movieId) => {
        setSelectedMovieId(movieId);
        setIsModalOpen(true);
    };

    const closeRatingModal = () => {
        setIsModalOpen(false);
        setSelectedMovieId(null);
        setRating(0);
    };

    const handleRating = async (score) => {
        try {
            setIsLoading(true);
            await addMovieScore(selectedMovieId, score);
            setRating(score);
            console.log(`Rating added: ${score}`);
            closeRatingModal();
        } catch (error) {
            console.error('Error adding rating:', error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                {movies.map((movie) => (
                    <div key={movie.movieId} className="bg-russian-violet text-vista-blue shadow-md rounded-lg overflow-hidden">
                        <img className="w-full h-52 object-cover" src={movie.posterPath || 'https://via.placeholder.com/400x600'} alt={movie.title} />
                        <div className="p-4">
                            <h3 className="font-bold text-xl text-hookers-green mb-2">{movie.title}</h3>
                            <button
                                className="bg-rosewood text-white px-4 py-2 rounded hover:bg-rosewood_dark transition duration-300"
                                onClick={() => removeFromList(movie.movieId, 'seen')}
                                disabled={isLoading}
                            >
                                {isLoading ? 'Removing...' : 'Remove'}
                            </button>
                            <button
                                className="bg-glaucous text-white px-4 py-2 rounded hover:bg-glaucous_dark transition duration-300 ml-2"
                                onClick={() => openRatingModal(movie.movieId)}
                                disabled={isLoading}
                            >
                                Rate
                            </button>
                        </div>
                    </div>
                ))}
            </div>
            <RatingModal
                isOpen={isModalOpen}
                onClose={closeRatingModal}
                onRate={handleRating}
                isLoading={isLoading}
                rating={rating}
            />
        </div>
    );
}
export default MovieList;
