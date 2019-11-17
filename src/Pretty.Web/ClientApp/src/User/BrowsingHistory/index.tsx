import * as React from 'react';
import './index.css';
import Skeleton from '@src/Components/Skeleton';
import IBlogPost from '@typings/IBlogPost';
import BlogPostCard from '@src/Components/BlogPostItem/BlogPostItem';
import Modal from '@src/Components/Modal';

function BrowsingHistory() {

  const [pagedData, setPagedData] = React.useState<IBlogPost[]>([]);
  const [isFetching, setFetching] = React.useState<boolean>(true);

  async function fetchBrowsingHistory() {
    let res: any;
    try {
      res = await fetch('/api/browsingHistory');
    } catch (e) {
      Modal.alert(e.toString(), 'error');
    }
    const { statusCode, extends: data } = await res.json();
    setFetching(false);
    if (statusCode === 0 && data) {
      setPagedData([...pagedData, ...data.data]);
    }

  }

  React.useEffect(() => {
    fetchBrowsingHistory();
  }, ['isFetching']);

  if (isFetching) {
    return <Skeleton />
  } else {
    return (
      <div className="browsing-history">
        {
          pagedData.map(i => <BlogPostCard key={i.id} blogPost={i} />)
        }
      </div>
    )
  }

}

export default BrowsingHistory;
