import * as React from 'react';
import './index.css';
import Avatar from '@src/Components/Avatar';
import { path } from 'ramda';
import { connect } from 'react-redux';
import { fetchMessage } from '@src/store/actions';
import * as Cookies from 'js-cookie';
import * as dayjs from 'dayjs';
import { CLEAR_UNREAD } from '@src/store/actionTypes';

function ChatContainer({
  dispatch,
  prevDate,
  recentDate,
  unread,
  data
}: any) {
  const conatinerRef = React.createRef<any>();
  const [showLoadingMore, setLoadingMore] = React.useState(true);
  dispatch({
    type: CLEAR_UNREAD
  });
  console.log('reload');
  React.useEffect(() => {
    dispatch(fetchMessage(
      prevDate,
      recentDate
    ));

    conatinerRef.current.scrollTop = conatinerRef.current.scrollHeight;
  }, ['message']);

  function isMessageOfCurrent(i: any) {
    return path(['id'], Cookies.getJSON('user')) === path(['user', 'id'], i);
  }

  function messageHandler(message: any, key: any) {
    switch (message.type) {
      case 'NewMessage':
        return (
          <div className={`item d-flex ${isMessageOfCurrent(message) ? 'flex-row-reverse' : ''}`} key={key}>
            <div id={path(['user', 'nickname'], message)} className="comment-item text-left">
              <div className={`comment-header d-flex align-items-center justify-content-between ${isMessageOfCurrent(message) ? 'flex-row-reverse' : ''}`}>
                <div className="d-flex align-items-center">
                  <Avatar url={message.user && message.user.avatarUrl} cls="user-head" />
                  <p className="nickname">
                    {path(['user', 'nickName'], message)}
                    <span style={{ marginLeft: '1rem' }}>{message.createOn && dayjs(message.createOn).format('MM-DD HH:mm')}</span>
                  </p>
                </div>
              </div>
              <div className="comment-content">
                <div className="content text-left" dangerouslySetInnerHTML={{ __html: message.content }} />
              </div>
            </div>
          </div>
        )
      case 'Connect':
        return (
          <div className="new-connect text-primary" key={key}>用户：{message.user && message.user.nickName} 上线了</div>
        )
      case 'DisConnect':
        return (
          <div className="new-connect text-secondary" key={key}>用户：{message.user && message.user.nickName} 下线了</div>
        )
      default:
        return (<React.Fragment key={key} />)
    }
  }

  function closeBtnOnClick(e: any) {
    setLoadingMore(false);
    e.stopPropagation();
  }

  function loadingMore() {
    setLoadingMore(false);
    const from =
      dayjs(prevDate).add(-7, 'day').toDate();
    dispatch(fetchMessage(from, prevDate));
  }

  return (
    <div className="chat-block" ref={conatinerRef} >
      {
        showLoadingMore &&
        <div className="loading-more" onClick={loadingMore}>加载更多。。。
      <i className="material-icons close" onClick={closeBtnOnClick}>close</i></div>
      }
      {
        data.map((i: any, key: any) => messageHandler(i, key))
      }
    </div>
  )
}

const mapStateToProps = (state: any) => {
  return path(['posts', 'message'], state);
}

export default connect(mapStateToProps)(ChatContainer);