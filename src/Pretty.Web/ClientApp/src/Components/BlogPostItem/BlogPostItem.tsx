import * as React from 'react';
import { Link } from 'react-router';
import './BlogPostItem.css'
import IBlogPost from '@typings/IBlogPost';


interface IBlogPostItemProps {
  blogPost: IBlogPost
}

class BlogPostCard extends React.Component<IBlogPostItemProps> {
  public constructor(props: IBlogPostItemProps) {
    super(props);
  }
  public render() {
    const { blogPost } = this.props;
    return (
      <div className="blog-post card text-left">
        <div className="card-body d-flex justify-content-between">
          <div>
            <h4 className="card-title">
              <Link className="card-link text-primary" to={`/blogPost?id=${blogPost.id}`} children={blogPost.title} />
              {
                blogPost.tags &&
                blogPost.tags.map(tag => <i className="badge badge-pill badge-default" key={tag.id}>{tag.title}</i>)
              }
            </h4>
            <p className="card-text text-overflow-ellipsis" style={{ height: `3rem` }}>{blogPost.describe}</p>
            <a href="#0" className="card-link text-secondary">
              <i className="material-icons">comment</i>
              {blogPost.commentCount}
            </a>
            <a href="#0" className="card-link text-secondary">
              <i className="material-icons">thumb_up</i>
              {blogPost.like}
            </a>
          </div>
          {
            blogPost.bannerUrl &&
            <img src={blogPost.bannerUrl + '&flag=50'} className="d-lg-block d-none banner-img" />
          }
        </div>
      </div>
    )
  }
}

export default BlogPostCard;