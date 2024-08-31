import React from 'react';
import { useAuth } from '../context/AuthContext';
import MovieListRated from "../components/movies/MovieListRated";

function UserProfile() {
    const { user } = useAuth();
    console.log(user);
    console.log(user.username);
    if (!user) return (
        <div className="flex justify-center items-center h-screen text-glaucous">
            Please log in to view your profile.
        </div>
    );

    return (
        <div className="container mx-auto px-4 py-8">
            <div className="bg-russian-violet text-vista-blue shadow-md rounded-lg p-6">
                <h1 className="text-2xl font-bold text-rosewood mb-4">User Profile</h1>

                 Add more user details here
                <main className="flex-1 w-full p-8">
                    <MovieListRated/>
                </main>
            </div>
        </div>
    );
}

export default UserProfile;
