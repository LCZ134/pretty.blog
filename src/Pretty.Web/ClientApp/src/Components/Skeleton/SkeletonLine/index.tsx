import * as React from 'react';
import './index.css';

function SkeletonLine({ children, data, style }: any) {
  if (!data) {
    return (
      <div className="skeleton-line-loading" style={style} />
    )
  } else {
    return children(data);
  }
}

export default SkeletonLine;