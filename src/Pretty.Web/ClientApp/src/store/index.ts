import { combineReducers } from 'redux';
import { app, posts } from './reducers';
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { createLogger } from 'redux-logger';

const middleware = [thunk];
if (process.env.NODE_ENV !== 'production') {
  const logger: any = createLogger;
  middleware.push(logger());
}

const reducers = combineReducers<{}>({
  app,
  posts,
});

const store = createStore(
  reducers,
  applyMiddleware(...middleware));

export default store;