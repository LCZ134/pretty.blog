import * as React from 'react';
import { IndexRoute, Route, Router } from 'react-router'
import App from '../Layout/App';
import BlogPostPage from '../BlogPost';
import Error from '../Error/Error';
import Home from '@src/Home';
import About from '@src/About';
import Archive from '@src/Archive';
import Personal from '@src/User';

export interface IProps {
  history: any;
}

const RouterConfig = ({ history }: IProps) => {
  return (
    <Router history={history}>
      <Route path="/" component={App}>
        <IndexRoute component={Home} />
        <Route path="home" component={Home} />
        <Route path="blogPost" component={BlogPostPage} />
        <Route path="archive" component={Archive} />
        <Route path="personal" component={Personal} />
        <Route path="about" component={About} />
      </Route>
      <Route path="*" component={Error} />
    </Router>
  )
};

export default RouterConfig;