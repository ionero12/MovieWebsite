import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { getMovies } from '../../services/api';

function MovieList() {

    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const data = await getMovies();
                setMovies(data);
            } catch (error) {
                console.error('Error fetching movies:', error);
            }
        };

        fetchMovies();
    }, []);

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                {movies.map((movie) => (
                    <div key={movie.movieId} className="bg-russian-violet text-vista-blue shadow-md rounded-lg overflow-hidden">
                        <img className="w-full h-54 object-cover" src={movie.posterPath || 'https://via.placeholder.com/400x600'} alt={movie.title} />
                        <div className="p-4">
                            <h3 className="font-bold text-xl text-rosewood mb-2">{movie.title}</h3>
                            <p className="text-glaucous text-base mb-2">{movie.description.substring(0, 100)}...</p>
                            <Link to={`/movie/${movie.movieId}`} className="inline-block bg-hookers-green text-white px-4 py-2 rounded hover:bg-rosewood transition duration-300">
                                View Details
                            </Link>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default MovieList;
