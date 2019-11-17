import * as React from 'react';
import './BlogPostList.css';
import BlogPostItem from '@Components/BlogPostItem/BlogPostItem';
import IBlogPost from '@typings/IBlogPost';
import { path } from 'ramda';
import { connect } from 'react-redux';
import { fetchBlogList } from '@src/store/actions';
import { isScrollOnBottom } from '@src/shared/utils';
import { debounce, once } from 'underscore';
import Skeleton from '@src/Components/Skeleton';

// paged size
const pageSize = 6;
let pageIndex = 0;
let pagedData: any = [];
let hasNextPage = true;
let oldData: any = [];
let oldFilter: any = null;

const initialComponent = once((dispatch: any) => {
  dispatch(fetchBlogList({
    pageIndex: pageIndex++,
    pageSize
  }));
});


function BlogPostList({
  dispatch,
  blogfilter,
  blog
}: any) {
  const { isFetching, paged } = blog;
  const onScrollToBottom = debounce(() => {
    dispatch(fetchBlogList({
      pageIndex: pageIndex++,
      pageSize
    }));

    if (!hasNextPage) {
      removeEvent();
    }
  }, 2000, true);

  function scrollChanged() {
    if (isScrollOnBottom()) {
      onScrollToBottom();
    }
  }

  function attchEvent() {
    document.addEventListener('scroll', scrollChanged);
  }

  function removeEvent() {
    document.removeEventListener('scroll', scrollChanged)
  }

  React.useEffect(() => {

    initialComponent(dispatch);

    if (hasNextPage) {
      attchEvent();
    }

    return () => {
      removeEvent();
    }
  }, ['isFetching']);

  if (blogfilter) {
    if (oldFilter !== blogfilter) {
      pagedData = [];
      oldFilter = blogfilter;
    }
  }
  if (paged && paged.data) {
    if (oldData !== paged.data) {
      hasNextPage = paged.hasNextPage;
      pagedData.push(...paged.data);
      oldData = paged.data;
    }
  }

  if (isFetching && pagedData.length <= 0) {
    return <Skeleton />
  } else {
    return (
      <div>
        <RenderBlogPostList data={pagedData} />
        {
          !hasNextPage &&
          <p>已经没有更多文章了。。。</p>
        }
      </div>
    );
  }
};

const RenderBlogPostList = ({ data }: {
  data: IBlogPost[]
}) => {
  return (
    <div className="blog-post-list">
      {
        data &&
        data.map((blogPost, key) => <BlogPostItem key={key} blogPost={blogPost} />)
      }
    </div>
  )
};

const mapStateToProps = (state: any) => {
  return {
    blog: path(['posts', 'blog'], state) || {
      isClear: true,
      isFetching: true,
      lastUpdated: Date.now(),
      paged: [],
    },
    blogfilter: path(['app', 'blogfilter'], state),
  }
}

export default connect(mapStateToProps)(BlogPostList);