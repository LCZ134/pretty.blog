import * as React from 'react';
import { path } from 'ramda';
import { isNullOrEmpty } from '@src/shared/utils';
import Modal from '@src/Components/Modal';
import * as Cookies from 'js-cookie';

function Register() {

  const [vCodeNextTime, setVCodeNextTime] = React.useState(60);

  function registerSuccess(result: any) {
    const { token, user }: any = result.extends;
    Cookies.set('token', token);
    Cookies.set('user', user);
    location.reload();
  }
  async function fetchRegister() {
    const nameDOM = document.querySelector('#register-name');
    const pwdDOM = document.querySelector('#register-pwd');
    const confimPwdDOM = document.querySelector('#confim-pwd');
    const emailDOM = document.querySelector('#email');
    const verifiyCodeDOM = document.querySelector('#verifiyCode');
    if (nameDOM === null ||
      pwdDOM === null ||
      confimPwdDOM === null ||
      emailDOM === null ||
      verifiyCodeDOM === null) { return; }

    const getValue = (dom: object) => path<string>(['value'], dom);
    const nickname = getValue(nameDOM);
    const pwd = getValue(pwdDOM);
    const confimPwd = getValue(confimPwdDOM);
    const email = getValue(emailDOM);
    const verifiyCode = getValue(verifiyCodeDOM);

    if (isNullOrEmpty(email)) {
      Modal.alert('请填写正确的邮箱');
      return;
    }
    if (isNullOrEmpty(nickname) || isNullOrEmpty(pwd)) {
      Modal.alert('请将信息填写完整', 'warning');
      return;
    }

    if (confimPwd !== pwd) {
      Modal.alert('两次密码不一致', 'warning');
      return;
    }

    const data =
      await fetch(`api/user/register?email=${email}&nickname=${nickname}&password=${pwd}&verifiycode=${verifiyCode}`, {
        method: 'POST'
      });

    const result = await data.json();
    let tips = '';
    switch (result.statusCode) {
      case 0:
        tips = '注册成功';
        registerSuccess(result);
        break;
      default:
        tips = result.result;
    }
    Modal.alert(tips, result.statusCode === 0 ? 'success' : 'warning');
  }

  async function sendVerifiyCode() {
    const emailDOM = document.querySelector('#email');
    if (emailDOM === null) {
      return;
    }
    const email = path(['value'], emailDOM);
    if (!email) {
      Modal.alert('请输入正确的邮箱');
      return;
    }
    const res = await fetch(`/api/user/genverifiycode?email=${email}`, {
      method: 'POST'
    });
    const result = await res.json();
    if (result) {
      Modal.alert('验证码发送成功，请在邮箱中确认');
      changeEnableStateOfSendBtn();
    } else {
      Modal.alert('验证码发送失败，请确认邮箱无误后重试');
    }
  }

  function changeEnableStateOfSendBtn() {
    const timer = setInterval(() => {
      setVCodeNextTime(vCodeNextTime - 1);
      if (vCodeNextTime <= 1) {
        clearInterval(timer);
        setVCodeNextTime(60);
      }
    }, 1000);
  }

  return (
    <div className="login">
      <input type="text"
        style={{ display: 'block', width: '100%', marginBottom: '2rem' }}
        className="form-control"
        placeholder="Nickname"
        id="register-name" />
      <input
        type="password"
        style={{ display: 'block', width: '100%', marginBottom: '2rem' }}
        className="form-control"
        placeholder="password"
        id="register-pwd" />
      <input
        type="password"
        style={{ display: 'block', width: '100%', marginBottom: '2rem' }}
        className="form-control"
        placeholder="confim password"
        id="confim-pwd" />
      <div className="d-flex">
        <input
          disabled={vCodeNextTime < 60}
          type="email"
          style={{ display: 'block', width: '100%', marginBottom: '2rem' }}
          className="form-control"
          placeholder="邮箱"
          id="email" />
        <button disabled={vCodeNextTime < 60} onClick={sendVerifiyCode} style={{ height: `2.4rem` }} className="btn btn-success">
          {vCodeNextTime === 60 ? '发送验证码' : `重试（${vCodeNextTime}s）`}
        </button>
      </div>
      <input type="text" className="form-control" placeholder="验证码" id="verifiyCode" />
      <button
        type="button"
        className="btn btn-primary"
        onClick={fetchRegister}>注册</button>
    </div>
  )
}

export default Register;

