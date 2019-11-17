import * as React from 'react';
import './error.css';

function Error() {
  return (
    <div className="error text-center">
      <h1 className="text-title">404 Not found</h1>
      <p><a href="/">go home</a></p>
    </div>
  )
}

export default Error;