import React from 'react';
import MovieListNext from "../components/movies/MovieListNext";

function Next() {
    return (
        <div className="home bg-russian-violet text-vista-blue min-h-screen flex flex-col items-center">
            <header className="bg-rosewood w-full py-4">
                <h1 className="text-4xl font-bold text-center">Next Watch</h1>
            </header>
            <main className="flex-1 w-full p-8">
                <MovieListNext />
            </main>
        </div>
    );
}

export default Next;
