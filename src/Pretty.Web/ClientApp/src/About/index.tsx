import './index.css';
import * as React from 'react';
import Map from './Map.js';
import ReposList from './ReposList/ReposList';

function About() {
  return (
    <section className="about">
      <h3 className="divider">网站信息</h3>
      <div className="d-flex align-items-center" style={{ flexDirection: 'column' }}>
        <div className="d-flex">
          <p>技术栈：</p>
          <p>React/Redux (前端) .net core 2.0/ef (后端)</p>
        </div>
        <div className="d-flex">
          <p>浏览器支持：</p>
          <p>所有<span className="text-info">现代浏览器</span></p>
        </div>
        <div className="d-flex">
          <p>博主邮箱：</p>
          <p><a href="mailto:sk2010407810@gmail.com">sk2010407810@gmail.com</a></p>
        </div>
      </div>
      <h3 className="divider">开源项目</h3>
      <div className="github-repos">
        <ReposList />
      </div>
      <h3 className="divider">定位</h3>
      <div className="map">
        <Map />
      </div>
    </section>
  )
}

export default About;