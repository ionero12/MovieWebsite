import React, { useState, useEffect } from 'react';
import {getMoviesByStatus, removeUserMovie} from '../../services/api';

function MovieList() {
    const [movies, setMovies] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    const fetchMovies = async () => {
        try {
            const data = await getMoviesByStatus('next');
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

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                {movies.map((movie) => (
                    <div key={movie.movieId} className="bg-russian-violet text-vista-blue shadow-md rounded-lg overflow-hidden">
                        <img className="w-full h-54 object-cover" src={movie.posterPath} alt={movie.title} />
                        <div className="p-4">
                            <h3 className="font-bold text-xl text-rosewood mb-2">{movie.title}</h3>
                            <button
                                className="bg-hookers-green text-white px-4 py-2 rounded hover:bg-rosewood transition duration-300"
                                onClick={() => removeFromList(movie.movieId, 'next')}
                                disabled={isLoading}
                            >
                                {isLoading ? 'Removing...' : 'Remove'}
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default MovieList;
