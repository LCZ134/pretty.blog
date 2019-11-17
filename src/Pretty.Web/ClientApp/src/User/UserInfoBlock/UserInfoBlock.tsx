import * as React from 'react';
import './UserInfoBlock.css';
import * as Cookies from 'js-cookie';
import { stopNativePropagation } from '@src/shared/utils';
import { toggleLoginCard } from '@src/store/actions';
import { connect } from 'react-redux';
import Avatar from '@src/Components/Avatar';

function UserInfoBlock({
  dispatch,
  user
}: any) {

  const [showUserMenu, setShowUserMenu] = React.useState(false);

  function AvatarClickHandler() {
    dispatch(toggleLoginCard());
  }

  function hidenUserMenuHandler() {
    setShowUserMenu(false);
    document.removeEventListener('click', hidenUserMenuHandler);
  }

  function showUserMenuHandler() {
    setShowUserMenu(true);
    document.addEventListener('click', hidenUserMenuHandler)
  }

  function signOut() {
    Cookies.remove('user');
    Cookies.remove('token');
    location.reload();
  }

  if (user) {
    return (
      <div className="user-info-block" onClick={showUserMenuHandler}>
        <Avatar url={user.avatarUrl} cls={'btn-round btn btn-just-icon'} />
        {
          showUserMenu &&
          <ul className="list-group card user-menu" onClick={stopNativePropagation}>
            <li onClick={signOut} className="list-group-item text-center" style={{ cursor: `point` }}>退出账号</li>
          </ul>
        }
      </div>
    )
  } else {
    return (
      <div className="user-info-block">
        <div className="avatar-img btn-round btn btn-just-icon"
          data-toggle="tooltip"
          data-placement="right"
          title="登录"
          onClick={AvatarClickHandler}>
          <i className="material-icons">person</i>
        </div>
      </div>
    )
  }
}

const mapStateToProps = ({ app }: any) => {
  return app || {};
};

export default connect(mapStateToProps)(UserInfoBlock);