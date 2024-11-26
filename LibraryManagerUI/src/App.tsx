import React from 'react';
import './App.css';
import { Books } from './features/books/Books';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Books />
      </header>
    </div>
  );
}

export default App;
