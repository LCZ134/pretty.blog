import * as React from 'react';
import { path } from 'ramda';
import Avatar from '@src/Components/Avatar';
import { connect } from 'react-redux';
import { getOnlineUsers } from '@src/store/actions';

function Rembers({ dispatch, onlineUsers }: { dispatch: any, onlineUsers: any[] }) {
  React.useEffect(() => {
    dispatch(getOnlineUsers());
  }, ['onlineUsers']);
  return (
    <section className="rembers">
      {
        onlineUsers.map((i, key) =>
          <div key={key} id={path(['nickName'], i)} className="comment-item text-left">
            <div className="comment-header d-flex align-items-center justify-content-between">
              <div className="d-flex align-items-center">
                <Avatar url={i && i.avatarUrl} cls="user-head online" />
                <p className="nickname">
                  {path(['nickName'], i)}
                </p>
              </div>
            </div>
          </div>)
      }
    </section>
  )
}

const mapStateToProps = (state: any) => {
  return {
    onlineUsers: path(['posts', 'onlineUsers'], state) || []
  }
}

export default connect(mapStateToProps)(Rembers);