import * as React from 'react';
import './LoginCard.css';
import Tab from '@src/Components/Tab';
import Login from './Login';
import { stopReactPropagation } from '@src/shared/utils';
import Register from './Register';

interface ILoginCardProp {
  toggleLoginCardHandler: () => void
}

function LoginCard({ toggleLoginCardHandler }: ILoginCardProp) {

  const hiddenLoginCardEvent = () => toggleLoginCardHandler();

  return (
    <div id="login-card-wrapper" onClick={hiddenLoginCardEvent}>
      <div className="login-card card" onClick={stopReactPropagation}>
        <Tab options={[{
          id: 'login',
          slot: <Login />,
          title: '登录'
        }, {
          id: 'register',
          slot: <Register/>,
           title: '注册'
        }]} />
      </div>
    </div>
  )
}

export default LoginCard;