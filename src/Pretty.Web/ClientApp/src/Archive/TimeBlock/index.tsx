import * as React from 'react';
import './index.css';
import { Link } from 'react-router';

interface ITimeBlockProps {
  time: any,
  list: any[],
  key: any,
  direction: any
}

function TimeBlock({
  time,
  list,
  direction = "right"
}: ITimeBlockProps) {
  return (
    <dl className={`time-block ${direction}`}>
      <dt className="time-block-header">{time}</dt>
      {
        list &&
        list.map(i => <dd key={i.id}><Link to={`/blogpost?id=${i.id}`}>{i.title}</Link></dd>)
      }
    </dl>
  )
}

export default TimeBlock;