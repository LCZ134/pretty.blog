import * as React from 'react';
import './index.css';

function ColorPicker({ onSelected }: {
  onSelected: (color: string) => void
}) {
  const colors = [{
    name: 'pink',
    value: '#e91e63'
  }, {
    name: 'blue',
    value: '#2196f3'
  }, {
    name: 'green',
    value: '#4caf50'
  }, {
    name: 'orange',
    value: '#ffeb3b'
  }, {
    name: 'purple',
    value: '#9c27b0'
  }];

  function onItemClick(name: string) {
    return () => onSelected(name);
  }

  return (
    <div className="color-picker card d-inline-block">
      {
        colors.map((i, key) => <div key={key} onClick={onItemClick(i.name)} className="item" style={{ backgroundColor: i.value }} />)
      }
    </div>
  )
}

export default ColorPicker;