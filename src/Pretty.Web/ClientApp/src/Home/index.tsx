import * as React from 'react';
import BlogPostList from './BlogPostList/BlogPostList';
import './index.css';
import Hots from './Hots/Hots';
import TagList from './TagList/TagList';
import TagFilter from './TagFilter';

class Home extends React.Component {
  public render() {
    return (
      <div id="home" className="row">
        <div className="col-md-9">
          <TagFilter/>
          <BlogPostList />
        </div>
        <div className="col-md-3 d-lg-block d-none">
          {/* <Personal /> */}
          <Hots />
          <TagList />
        </div>
      </div>
    )
  }
}

export default Home;