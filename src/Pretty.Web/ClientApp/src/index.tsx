import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { browserHistory } from 'react-router';
import './index.css';
import { unregister } from './registerServiceWorker';
import Router from './route/index';
import { Provider } from 'react-redux';
import store from './store';
import './signalR';


const App =
  <Provider store={store}>
    <Router history={browserHistory} />
  </Provider>;

ReactDOM.render(
  App,
  document.getElementById('root') as HTMLElement
);


unregister();
