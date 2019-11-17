import * as React from 'react';
import { showFileDialog } from '@src/shared/utils';
import { path } from 'ramda';
import Modal from '@src/Components/Modal';
import { connect } from 'react-redux';
import { updateUser } from '@src/store/actions';

let changed = false;

function UserInfoCard({ dispatch, avatarUrl, nickName }: any) {
  const [editing, toggleEditing] = React.useState<boolean>(false);

  function saveBtnOnClickHandler() {
    toggleEditing(!editing);
  }
  if (!editing && changed) {
    updateUserInfo();
  }

  function updateStore() {
    dispatch(updateUser({
      avatarUrl,
      nickName
    }));
  }

  function changeName(e: any) {
    const newNickName = e.target.value;
    if (nickName === newNickName) { return; }
    nickName = newNickName;
    updateStore();
  }

  function updateChangedState() {
    changed = true;
  }

  async function updateUserInfo() {
    const body = new FormData();
    body.append("nickName", nickName);
    body.append("avatarUrl", avatarUrl);
    const res = await fetch('/api/user', {
      body,
      method: 'PATCH',
    });
    const result = await res.json();
    if (result.statusCode === 0) {
      Modal.alert('保存成功');
    } else {
      Modal.alert('保存失败');
    }
    changed = false;
  }

  async function uploadAvatar(file: any, callback: (result: any) => void) {
    if (!file) { return };
    const body = new FormData();
    body.append("file", file);
    const res = await fetch('/api/file', {
      body,
      method: 'POST',
    });
    const result = await res.json();
    if (callback) {
      callback(result);
    }
  }

  function changeAvatar() {
    showFileDialog(async ({ target }: any) => {
      uploadAvatar(target.files[0], (result: any) => {
        if (!result) { return; }
        const newAvatarUrl = path(['extends', 'path'], result[0]);
        if (newAvatarUrl === avatarUrl) { return; }
        avatarUrl = newAvatarUrl;
        updateStore();
        changed = true;
      });
    }, {
      accept: "image/png,image/jpg,image/jpeg"
    });
  }

  const avatarProps = !editing ? {
    className: 'user-head',
    style: { backgroundImage: `url(${avatarUrl})` }
  } : {
      className: 'user-head editing',
      onClick: changeAvatar,
      style: { backgroundImage: `url(${avatarUrl})` }
    };
  return (
    <div className="user-info-card card">
      <div style={{ backgroundImage: `url(${avatarUrl})` }} />
      <div className="card-body d-flex justify-content-between align-items-center">
        <div className="d-flex align-items-center">
          <div {...avatarProps} />
          <div className="nickname">
            {
              editing ?
                <div><input
                  className="form-control"
                  type="text"
                  onChange={changeName}
                  onBlur={updateChangedState}
                  value={nickName} /></div> :
                nickName
            }
          </div>
        </div>
        <button onClick={saveBtnOnClickHandler}
          className="btn btn-primary"
          style={{ height: '2.5rem' }}>
          {
            editing ?
              <span><i className="material-icons">save</i>保存</span> :
              <span><i className="material-icons">add</i>编辑资料</span>
          }
        </button>
      </div>
    </div>
  )
}

const mapStateToProps = ({ app }: any) => {
  return app.user ? app.user : {};
}

export default connect(mapStateToProps)(UserInfoCard);