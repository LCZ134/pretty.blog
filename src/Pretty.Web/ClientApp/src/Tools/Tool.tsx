import * as React from 'react';
import './Tool.css';
import DanMu from '@src/Components/Danmu';
import ColorPicker from '@src/Components/ColorPicker';
import Modal from '@src/Components/Modal';
import { TOGGLE_CHATROOM } from '@src/store/actionTypes';
import { connect } from 'react-redux';
import path from 'ramda/es/path';

function Tool({ dispatch, unread }: any) {
  const [showThemePicker, setThemePicker] = React.useState(false);
  const tools = [{
    fontSize: 18,
    icon: 'invert_colors',
    onClick: () => {
      setThemePicker(!showThemePicker);
    }
  }, {
    fontSize: 18,
    icon: 'chat',
    onClick: () => {
      let closeHandler: () => void;
      function close() {
        if (typeof closeHandler === 'function') {
          closeHandler();
        }
      }
      closeHandler = Modal.message(<DanMu onClose={close} />);
    }
  }, {
    fontSize: 18,
    icon: 'mail',
    onClick: () => {
      dispatch({
        type: TOGGLE_CHATROOM
      });
    },
    render: (tool: any, key: any) => {
      return (
        <div className="item position-relative" onClick={tool.onClick} key={key}>
          {
            unread > 0 &&
            <div className="unread-count bg-primary">{unread}</div>
          }
          <i className="material-icons" style={{ fontSize: `${tool.fontSize}px`, fontWeight: 'bold' }}>{tool.icon}</i>
        </div>
      )
    }
  }, {
    fontSize: 25,
    icon: 'keyboard_arrow_up',
    onClick: () => {
      window.scrollTo({
        left: 0,
        top: 0
      });
    }
  }, {
    fontSize: 25,
    icon: 'keyboard_arrow_down',
    onClick: () => {
      const doc: any = document;

      window.scrollTo({
        left: 0,
        top: doc.scrollingElement.scrollHeight
      });
    }
  }];

  function onThemeSelected(color: string) {
    const global: any = window;
    global.theme.setCurrentTheme(color);
  }

  return (
    <div id="tool" className="list card d-lg-block d-none">
      {
        tools.map((tool, key) => (
          tool.render ?
            tool.render(tool, key)
            :
            <div className="item" key={key} onClick={tool.onClick}>
              <i className="material-icons" style={{ fontSize: `${tool.fontSize}px`, fontWeight: 'bold' }}>{tool.icon}</i>
            </div>
        ))
      }
      <div className="exts">
        {
          showThemePicker &&
          <ColorPicker onSelected={onThemeSelected} />
        }
      </div>
    </div>
  )
}



export default connect(state => {
  return {
    unread: path(['posts', 'message', 'unread'], state) || 0
  }
})(Tool);