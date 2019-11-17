import * as React from 'react';
import './Hots.css'
import PrettyFetch from '@src/Components/PrettyFetch/PrettyFetch';
import IBlogPost from '@typings/IBlogPost';
import { Link } from 'react-router';

function Hots() {
  function getBadgeByRank(rank: number): string {
    switch (rank) {
      case 1:
        return 'badge-danger'
      case 2:
        return 'badge-warning'
      case 3:
        return 'badge-success'
      default:
        return 'badge-info'
    }
  }

  return (
    <div className="blog-post-hots card text-left">
      <div className="card-body">
        <h4 className="card-title">热门文章</h4>
        <ul className="list-group">
          <PrettyFetch url={`/blog/hots`} fetchProps={{ count: 8 }}>
            {
              (data: IBlogPost[]) =>
                data.map((item, key) =>
                  <React.Fragment key={key}>
                    <li className="list-group-item">
                            <span className={`badge ${getBadgeByRank(key + 1)}`}>{key + 1}</span>
                            <Link to={`/blogpost?id=${item.id}`}>{item.title}</Link>
                    </li>
                  </React.Fragment>
                )
            }
          </PrettyFetch>
        </ul>
      </div>
    </div>
  )
}

export default Hots;