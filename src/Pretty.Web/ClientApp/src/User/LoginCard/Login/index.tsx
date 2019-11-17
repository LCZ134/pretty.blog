import * as React from 'react';
import path from 'ramda/es/path';
import Modal from '@src/Components/Modal';
import { isNullOrEmpty } from '@src/shared/utils';
import * as cookie from 'js-cookie';

function Login() {

  async function fetchLogin() {
    const emailDOM = document.querySelector('.name');
    const pwdDOM = document.querySelector('.password');
    if (emailDOM === null || pwdDOM === null) { return; }
    const getValue = (dom: object) => path<string>(['value'], dom);
    const email = getValue(emailDOM);
    const pwd = getValue(pwdDOM);

    if (isNullOrEmpty(email) || isNullOrEmpty(pwd)) {
      Modal.alert('请将信息填写完整', 'warning');
      return;
    }


    const data =
      await fetch(`api/user/login?email=${email}&password=${pwd}`, {
        method: 'POST'
      });

    const result = await data.json();
    let tips = '';
    switch (result.statusCode) {
      case 0:
        tips = '登录成功';
        loginSuccess(result);
        break;
      case 1:
        tips = '密码错误';
        break;
      case 2:
        tips = '账号不存在';
        break;
      default:
        tips = 'Unknow Error';
    }
    Modal.alert(tips, result.statusCode === 0 ? 'success' : 'warning');

  }

  function loginSuccess(result: any) {
    cookie.set('token', result.token);
    cookie.set('user', result.user);
    location.reload();
  }

  return (
    <div className="login">
      <input type="text" style={{ display: 'block', width: '100%', marginBottom: '2rem' }} className="form-control name" placeholder="邮箱" />
      <input type="password" style={{ display: 'block', width: '100%', marginBottom: '2rem' }} className="form-control password" placeholder="密码" />
      <button type="button" className="btn btn-primary" onClick={fetchLogin}>登录</button>
    </div>
  )
}

export default Login;