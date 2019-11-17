import * as React from 'react';
import './index.css';

function Skeleton() {
  return (
    <div className="skeleton-list">
      <div className="skeleton-row" />
      <div className="skeleton-row" />
      <div className="skeleton-row" />
    </div>
  )
}

export default Skeleton;