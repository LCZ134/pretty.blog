import * as React from 'react';
import './App.css';
import Header from '../Header/Header';
import Footer from '../Footer/Footer';
import Tool from '../../Tools/Tool';
import LoginCard from '@src/User/LoginCard/LoginCard';
import { toggleLoginCard } from '@src/store/actions';
import { connect } from 'react-redux';
import ChatRoom from '@src/Components/ChatRoom';
import Fade from '@src/Components/Animation/Fade';

function App({ dispatch, children, showLoginCard, showChatRoom }: any) {
  function toggleLoginCardHandler() {
    dispatch(toggleLoginCard());
  }
  return (
    <div className="App">
      <Header />
      <main className="container m-auto">
        {children}
        {
          showChatRoom &&
          <Fade>
            <ChatRoom />
          </Fade>
        }
      </main>
      <Footer />
      <Tool />
      {
        showLoginCard &&
        <Fade>
          <LoginCard toggleLoginCardHandler={toggleLoginCardHandler} />
        </Fade>
      }
    </div>
  );
}

const mapStateToProps = ({ app }: any) => {
  return app || {};
};

export default connect(mapStateToProps)(App);
