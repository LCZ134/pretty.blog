import * as React from 'react';
import IComment from '@typings/IComment';
import CommentItem from '../CommentItem/CommentItem';
import './CommentList.css';
import IPaged from '@typings/IPaged';
import path from 'ramda/es/path';
import { connect } from 'react-redux';
import { fetchPaged } from '@src/store/actions';
import Skeleton from '@src/Components/Skeleton';
import Pagination from '@src/Components/Pagination';

function CommentList({
  blogPostId,
  paged,
  isFetching,
  dispatch
}: {
  blogPostId: any,
  paged?: IPaged<IComment>,
  isFetching?: boolean,
  lastUpdated?: any,
  dispatch?: any
}): JSX.Element {
  const [curIndex, setCurIndex] = React.useState(0);
  function RenderCommentList({ commentList }: {
    commentList: IComment[]
  }) {
    return (
      <div className="comment-list">
        <ul className="list">
          {
            commentList.map((comment, key) =>
              <CommentItem key={key} comment={comment} parent={null} />
            )
          }
        </ul>
        {
          paged && paged.totalPages > 1 &&
          <Pagination curIndex={curIndex} paged={paged} onChange={onPaginationChange} />
        }
      </div>
    );
  }

  function onPaginationChange(from: number, to: number) {
    console.log(`from: ${from} to: ${to}`)
    setCurIndex(to);
    dispatch(fetchPaged('comment', {
      blogPostId,
      pageIndex: to
    }));
  }

  React.useEffect(() => {
    dispatch(fetchPaged('comment', {
      blogPostId,
      pageIndex: curIndex
    }));
  }, ['isFetching', 'lastUpdated']);

  if (isFetching) {
    return <Skeleton />
  }
  else if (paged && paged.data.length > 0) {
    return <RenderCommentList commentList={paged.data} />;
  } else {
    return <p>暂时还没有人评论。。。</p>
  }
}

const mapStateToProps = (state: any) => {
  return path(['posts', 'comment'], state) || {
    isFetching: true,
    paged: {}
  };
}

export default connect(mapStateToProps)(CommentList);