import WangEditor from 'wangeditor';
import * as React from 'react';
import editorConfig from './editor.config';
import './PrettyEditor.css';
import { object } from 'prop-types';

class PrettyEditor extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="pretty-editor" ref="editorEl" style={{ textAlign: 'left' }}></div>
    )
  }

  componentDidMount() {

    const elem = this.refs.editorEl;
    const editor = new WangEditor(elem);
    editor.customConfig = editorConfig;
    // 使用 onchange 函数监听内容的变化，并实时更新到 state 中    
    Object.assign(editor.customConfig, this.props);
    editor.create();
  }
}

export default PrettyEditor;