import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';

function Header() {
    const { user, logout } = useAuth();

    return (
        <header className="bg-russian-violet p-4">
            <nav className="container mx-auto">
                <ul className="flex justify-between items-center">
                    <li className="text-vista-blue font-bold text-lg">
                        <Link to="/" className="hover:text-glaucous transition duration-300">Home</Link>
                    </li>
                    {user ? (
                        <>
                            <li className="text-vista-blue font-bold text-lg">
                                <Link to="/profile"
                                      className="hover:text-glaucous transition duration-300">Profile</Link>
                            </li>
                            <li className="text-vista-blue font-bold text-lg">
                                <Link to="/next"
                                      className="hover:text-glaucous transition duration-300">Next Watch</Link>
                            </li>
                            <li className="text-vista-blue font-bold text-lg">
                                <Link to="/seen"
                                      className="hover:text-glaucous transition duration-300">Already Seen</Link>
                            </li>
                            <li>
                                <button
                                    onClick={logout}
                                    className="bg-rosewood text-white px-4 py-2 rounded hover:bg-hookers-green transition duration-300"
                                >
                                    Logout
                                </button>
                            </li>
                        </>
                    ) : (
                        <li className="text-vista-blue font-bold text-lg">
                            <Link to="/login" className="hover:text-glaucous transition duration-300">Login</Link>
                        </li>
                    )}
                </ul>
            </nav>
        </header>
    );
}

export default Header;
