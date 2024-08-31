// tailwind.config.js

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}", // Ensure this matches your file structure
  ],
  theme: {
    extend: {
      colors: {
        'russian-violet': '#0d0630ff',
        'rosewood': '#640d14ff',
        'vista-blue': '#8ea4d2ff',
        'glaucous': '#6279b8ff',
        'hookers-green': '#496f5dff',
        'rosewood_dark': '#460106',
        'vista-blue_dark': '#506ca9',
        'glaucous_dark': '#2c417c',
        'hookers-green_dark': '#1d3d2e',
      },
    },
  },
  plugins: [],
};