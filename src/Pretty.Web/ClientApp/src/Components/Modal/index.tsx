import * as React from 'react';
import { render } from 'react-dom';
import './index.css';

function Modal({ children }: any): JSX.Element {
  return (
    <div>
      {children}
    </div>
  )
}

Modal.alert = (msg: string,
  type: string = 'success',
  direction: string = 'top',
  timeout: number = 3000) => {
  const alertVM =
    <div className={`alert-wrapper ${direction}`}>
      <div className={`alert alert-${type}`}>{msg}</div>
    </div>;

  const node = document.createElement('div');
  document.body.appendChild(node);
  render(alertVM, node);

  setTimeout(() => document.body.removeChild(node), timeout);
}

Modal.message = (content: any) => {
  const alertVM =
    <div className={`message-wrapper`}>
      {content}
    </div>;
  const node = document.createElement('div');
  document.body.appendChild(node);
  render(alertVM, node);

  return function close() {
    // 如果挂载的是组件，为了触发其 unmounted 钩子 
    render(<div />, node);

    document.body.removeChild(node);
  }
}



export default Modal;