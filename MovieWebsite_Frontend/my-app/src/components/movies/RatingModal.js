// RatingModal.js
import React from 'react';

const RatingModal = ({ isOpen, onClose, onRate, isLoading, rating }) => {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
            <div className="bg-white p-6 rounded shadow-md">
                <h3 className="text-lg font-semibold text-gray-900">Rate this movie:</h3>
                <div className="flex space-x-1 mt-2">
                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((star) => (
                        <button
                            key={star}
                            onClick={() => onRate(star)}
                            className={`px-2 py-1 rounded ${
                                star <= rating ? 'bg-vista-blue text-white' : 'bg-gray-200 text-gray-600'
                            }`}
                            disabled={isLoading}
                        >
                            {star}
                        </button>
                    ))}
                </div>
                <button onClick={onClose} className="mt-4 bg-red-500 text-white px-4 py-2 rounded">
                    Close
                </button>
            </div>
        </div>
    );
};

export default RatingModal;
