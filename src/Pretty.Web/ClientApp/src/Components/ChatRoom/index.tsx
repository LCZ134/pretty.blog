import * as React from 'react';
import PrettyEditor from '../PrettyEditor/PrettyEditor';
import './index.css';
import ChatContainer from './Components/ChatContainer';
import Rembers from './Components/Rembers';
import wb from '@src/signalR';
import { connect } from 'react-redux';
import { TOGGLE_CHATROOM } from '@src/store/actionTypes';
import { stopReactPropagation } from '@src/shared/utils';

let content: string;
function ChatRoom({ dispatch }: any) {

  function onTextChange(htmlStr: string) {
    content = htmlStr;
  }

  function sendNewMessage() {
    wb.sendMessage(content);
  }

  function hideChatRoom() {
    dispatch({
      type: TOGGLE_CHATROOM
    });
  }

  return (
    <div className="chat-room-wrapper" onClick={hideChatRoom}>
      <section id="chat-room" className="d-flex" onClick={stopReactPropagation}>
        <div className="chat">
          <ChatContainer />
          <PrettyEditor onchange={onTextChange} />
          <button className="btn btn-primary btn-send" onClick={sendNewMessage}>发送</button>
        </div>
        <div className="member">
          <div>在线成员</div>
          <Rembers />
        </div>
      </section>
    </div>
  )
}

export default connect(state => state)(ChatRoom);