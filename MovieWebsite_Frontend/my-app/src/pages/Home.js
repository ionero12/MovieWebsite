import React from 'react';
import MovieList from '../components/movies/MovieList';

function Home() {

    return (
        <div className="home bg-russian-violet text-vista-blue min-h-screen flex flex-col items-center">
            <header className="bg-rosewood w-full py-4">
                <h1 className="text-4xl font-bold text-center">Welcome to Ione's Movie Website</h1>
            </header>
            <main className="flex-1 w-full p-8">
                <MovieList />
            </main>
        </div>
    );
}

export default Home;
