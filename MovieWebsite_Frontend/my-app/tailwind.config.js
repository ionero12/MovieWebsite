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
      },
    },
  },
  plugins: [],
};