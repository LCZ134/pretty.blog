import * as React from 'react';
import './index.css';
import IPaged from '@typings/IPaged';
import Modal from '../Modal'
import ColorPicker from '../ColorPicker';
import { stopReactPropagation } from '@src/shared/utils';

let danmu: any;

function DanMu({ onClose }: {
  onClose: () => void
}) {
  const global: any = window;

  const [dmColor, setDMColor] = React.useState('rgb(33, 150, 243)');
  const [showColorPicker, setShowColorPicker] = React.useState(false);

  async function fetchDanMu(callback: (paged: IPaged<any>) => void) {
    const res = await fetch(`/api/danmu?pageIndex=0&pageSize=1000`);
    const paged = await res.json();
    if (typeof callback === 'function') {
      callback(paged);
    }
  }

  async function insertDanMu({ color, fontSize, content, type }: {
    color: string,
    fontSize: number,
    content: string,
    type: string
  }) {
    const res = await fetch(`/api/danmu?color=${color}&fontSize=${fontSize}&content=${content}&type=${type}`, {
      method: 'POST'
    });

    const result = await res.json();

    if (result.statusCode === 0) {
      Modal.alert('添加成功');
    } else {
      Modal.alert(result.result, 'warning');
    }
  }

  React.useEffect(() => {
    danmu = new global.R.default({
      el: document.querySelector('#danmu'),
      maxCount: 200,
      speed: 3
    });
    danmu.start();
    fetchDanMu(paged => {
      paged.data.filter(i => i.content || i.type).forEach(i => danmu.push(i));
    });

    return () => {
      danmu.stop();
    }
  }, []);

  function keyDown(e: any) {
    if (e.key === 'Enter') {
      const dm = {
        color: dmColor,
        content: e.target.value,
        fontSize: 30,
        type: 'scroll'
      };
      insertDanMu(dm);
      danmu.push(dm);
    }
  }

  function colorSelected(color: string) {
    setDMColor(color);
  }

  function inputFocus() {
    setShowColorPicker(true);
  }

  function hideColorPicker() {
    setShowColorPicker(false);
  }

  return (
    <section className="danmu-section" onClick={hideColorPicker}>
      <i className="material-icons close" onClick={onClose}>close</i>
      <div id="danmu" />
      <div className="insert-danmu-box d-flex justify-content-center" onClick={stopReactPropagation}>
        {
          showColorPicker &&
          <ColorPicker onSelected={colorSelected} />
        }
        <i className="material-icons invert_colors" style={{ color: `${dmColor}` }}>invert_colors</i>
        <input type="text"
          className="form-control txt-danmu"
          placeholder="Enter your message."
          onFocus={inputFocus}
          onKeyPress={keyDown} />
      </div>
    </section>
  )
}

export default DanMu;