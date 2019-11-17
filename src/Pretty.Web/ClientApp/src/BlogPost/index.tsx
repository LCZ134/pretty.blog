import * as React from 'react';
import BlogPostPage from './BlogPostPage/BlogPostPage';
import { path } from 'ramda';

function BlogPost(vm: any) {

  const blogPostId = path<string>(['location', 'query', 'id'], vm);
  React.useEffect(() => {
    setTimeout(() => {
      fetch(`/api/browsingHistory?blogPostId=${blogPostId}`, {
        method: 'POST'
      })
    }, 3000);
  },['blogPostId']);
  
  return (
    <div>
      <BlogPostPage blogPostId={blogPostId} />
    </div>
  )
}

export default BlogPost;