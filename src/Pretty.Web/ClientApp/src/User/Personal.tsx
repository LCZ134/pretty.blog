import * as React from 'react';
import * as Cookies from 'js-cookie';
import Tab from '@src/Components/Tab';
import BrowsingHistory from './BrowsingHistory';
import ThumbUpHistory from './ThumbUpHistory';
import { toggleLoginCard } from '@src/store/actions';
import { connect } from 'react-redux';
import { withRouter } from 'react-router';
import UserInfoCard from './UserInfoCard';
import once from 'ramda/es/once';
import { loadScripts } from '@src/shared/utils';

const initPersonal = once(() => {
  loadScripts([
    './assets/live2d/live2d.js',
    './assets/live2d/waifu-tips.js'
  ], () => {
    try {
      const win: any = window;
      win.live2d_settings.modelId = 1;
      win.live2d_settings.modelTexturesId = 87;
      initModel('./assets/live2d/waifu-tips.json');
    } catch (err) { console.log('[Error] JQuery is not defined.') }
  });
});

function Personal({ router, dispatch }: any) {
  React.useEffect(()=>{
    initPersonal();
    loadScripts([
      './assets/live2d/waifu-tips.js'
    ], () => {
      try {
        const win: any = window;
        win.live2d_settings.modelId = 1;
        win.live2d_settings.modelTexturesId = 87;
        initModel('./assets/live2d/waifu-tips.json');
      } catch (err) { console.log('[Error] JQuery is not defined.') }
    });
  });
  const user = Cookies.getJSON('user');
  if (!user) {
    router.push('/home');
    dispatch(toggleLoginCard());
    return (<div>请先登录</div>);
  }
  return (
    <section id="personal">
      <UserInfoCard />
      <div className="divider">Other</div>
      <div className="d-flex">
        <Tab options={[{
          id: '10',
          slot: <BrowsingHistory />,
          title: '浏览记录',
        }, {
          id: '11',
          slot: <ThumbUpHistory />,
          title: '点赞记录',
        }]} />
        <div className="card user-meta-card d-lg-flex d-none">
          <div className="card-body">
            <p>更多功能，敬请期待。。</p>
            <div className="waifu">
              {/* <div className="waifu-tips"/> */}
              <canvas id="live2d" className="live2d"/>
              <div className="waifu-tool">
                <span className="fui-home"/>
                <span className="fui-chat"/>
                <span className="fui-eye"/>
                <span className="fui-user"/>
                <span className="fui-photo"/>
                <span className="fui-info-circle"/>
                <span className="fui-cross"/>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}

const mapStateToProps = (state: any) => state;

export default connect(mapStateToProps)(withRouter(Personal));