import * as React from 'react';

interface ITabOption {
  id: string | null,
  title: any,
  slot: React.ReactElement
}

interface ITabProp {
  options: ITabOption[]
}

function Tab(this: any, { options }: ITabProp) {
  if (options.length <= 0) { return <div />; }
  const [current, setCurrent] = React.useState(options[0]);
  const moveToCurrent = (i: ITabOption) => {
    setCurrent(i);
  }
  return (
    <div className="pretty-tab">
      <ul className="nav nav-pills nav-pills-primary" role="tablist">
        {
          options.map((i: ITabOption, key) =>
            <li className={`nav-item`} key={key} style={{ cursor: 'pointer' }}>
              <a onClick={moveToCurrent.bind(this, i)} className={`nav-link ${i.id === current.id ? 'active' : ''}`} aria-expanded="true">
                {i.title}
              </a>
            </li>
          )
        }
      </ul>
      <div className="tab-content tab-space">
        {
          options.map((i: ITabOption, key) =>
            <div className={`tab-pane ${i.id === current.id ? 'active' : ''}`} id={`${i.id}`} aria-expanded="true" key={key}>
              {i.slot}
            </div>
          )
        }

      </div>
    </div>
  )
}

export default Tab;