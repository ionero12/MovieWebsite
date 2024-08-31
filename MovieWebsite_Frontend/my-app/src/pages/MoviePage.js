import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import {getMovieById, setUserMovie, addMovieScore, getUserMovies} from '../services/api';

function MoviePage() {

    const { movieId } = useParams();
    const [movie, setMovie] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);
    const [rating, setRating] = useState(0); // State for handling the selected rating

    useEffect(() => {
        const fetchMovie = async () => {
            try {
                const data = await getMovieById(movieId);
                setMovie(data);
            } catch (error) {
                console.error('Error fetching movie:', error);
            }
        };

        const fetchUserMovies = async () => {
            try {
                const data = await getUserMovies(movieId);
                setRating(data[0].score);
            } catch (error) {
                console.error('Error fetching movie:', error);
            }
        };

        fetchMovie();
        fetchUserMovies();
    }, [movieId]);



    const addToList = async (status) => {
        try {
            setIsLoading(true);
            await setUserMovie(movie.movieId, status);
            console.log(status);
        } catch (error) {
            console.error('Error adding movie to list:', error);
            setError('Failed to add movie to list');
        } finally {
            setIsLoading(false);
        }
    };

    const handleRating = async (score) => {
        if (score !== rating) {
            try {
                setIsLoading(true);
                await addMovieScore(movie.movieId, score);
                setRating(score);
            } catch (error) {
                setError('Failed to add rating');
            } finally {
                setIsLoading(false);
            }
        } else {
            console.log('Selected rating is the same as the current rating. No update needed.');
        }
    };

    if (!movie) return <div className="flex justify-center items-center h-screen">Loading...</div>;

    return (
        <div className="bg-russian-violet min-h-screen flex items-center justify-center">
            <div className="container mx-auto px-4 py-8">
                <div className="bg-white shadow-lg rounded-lg overflow-hidden">
                    <div className="md:flex">
                        <div className="md:flex-shrink-0">
                            <img className="h-54 w-full object-cover md:w-48" src={movie.posterPath} alt={movie.title} />
                        </div>
                        <div className="p-8">
                            <h1 className="text-2xl font-bold text-gray-900 mb-2">{movie.title}</h1>
                            <p className="text-gray-600 mb-4">{movie.description}</p>
                            <div className="text-sm text-gray-500">
                                <p>Duration: {movie.duration} min</p>
                                <p>Release Date: {movie.releaseDate}</p>
                            </div>

                            {/* Star Rating Section */}
                            <div className="mt-4">
                                <h3 className="text-lg font-semibold text-gray-900">Rate this movie:</h3>
                                <div className="flex space-x-1 mt-2">
                                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((star) => (
                                        <button
                                            key={star}
                                            onClick={() => handleRating(star)}
                                            className={`px-2 py-1 rounded ${
                                                star <= rating ? 'bg-vista-blue text-white' : 'bg-gray-200 text-gray-600'
                                            }`}
                                            disabled={isLoading}
                                        >
                                            {star}
                                        </button>
                                    ))}
                                </div>
                                {rating > 0 && <p className="mt-2 text-green-600">Your current rating: {rating}</p>}
                            </div>

                            <div className="mt-4 flex space-x-4">
                                <button
                                    className="bg-hookers-green text-white px-4 py-2 rounded hover:bg-rosewood transition duration-300"
                                    onClick={() => addToList('next')}
                                    disabled={isLoading}
                                >
                                    {isLoading ? 'Adding...' : 'Add to Next Watch'}
                                </button>
                                <button
                                    className="bg-hookers-green text-white px-4 py-2 rounded hover:bg-rosewood transition duration-300"
                                    onClick={() => addToList('seen')}
                                    disabled={isLoading}
                                >
                                    {isLoading ? 'Adding...' : 'Already Seen'}
                                </button>
                            </div>

                            {error && <div className="text-red-500 mt-4">{error}</div>}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default MoviePage;
