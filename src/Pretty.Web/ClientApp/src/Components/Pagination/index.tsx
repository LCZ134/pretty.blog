import * as React from 'react';
import IPaged from '@typings/IPaged';

function Pagination({
  curIndex,
  paged,
  onChange,
}: {
  curIndex: number,
  paged?: IPaged<any>,
  onChange: (from: number, to: number) => void,
}) {
  if (!paged) { return <div>paged undefind</div> }

  const pageItems = [];
  function pageIndexTo(index: number) {
    return () => {
      onChange(curIndex, index);
    }
  }
  const pageIndexToPrev = () => {
    return paged.hasPreviousPage ? pageIndexTo(curIndex - 1)() : null;
  }
  const pageIndexToNext = () => {
    return paged.hasNextPage ? pageIndexTo(curIndex + 1)() : null;
  }
  for (let i = 0; i < paged.totalPages; i++) {
    pageItems.push(
      <li className={`page-item ${curIndex === i ? 'active' : ''}`} key={i}>
        <a className="page-link" onClick={pageIndexTo(i)}>{i + 1}</a>
      </li>
    )
  }
  return (
    <nav className="navigation" style={{margin:`1rem 0`}}>
      <ul className="pagination justify-content-center">
        <li className="page-item">
          <a className="page-link" onClick={pageIndexToPrev}>上一页</a>
        </li>
        {
          pageItems
        }
        <li className="page-item">
          <a className="page-link" onClick={pageIndexToNext}>下一页</a>
        </li>
      </ul>
    </nav>
  )
}

export default Pagination;