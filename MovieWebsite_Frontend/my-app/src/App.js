import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Header from './components/common/Header';
import Footer from './components/common/Footer';
import Home from './pages/Home';
import MoviePage from './pages/MoviePage';
import UserProfile from './pages/UserProfile';
import Login from './pages/Login';
import { AuthProvider } from './context/AuthContext';
import './App.css';
import Next from "./pages/Next";
import Seen from "./pages/Seen";

function App() {
    return (
        <AuthProvider>
            <Router>
                <div className="App">
                    <Header />
                    <main>
                        <Routes>
                            <Route path="/" element={<Home />} />
                            <Route path="/movie/:movieId" element={<MoviePage />} />
                            <Route path="/profile" element={<UserProfile />} />
                            <Route path="/next" element={<Next />} />
                            <Route path="/seen" element={<Seen />} />
                            <Route path="/login" element={<Login />} />
                            <Route path="*" element={<Navigate to="/" replace />} />
                        </Routes>
                    </main>
                    <Footer />
                </div>
            </Router>
        </AuthProvider>
    );
}

export default App;