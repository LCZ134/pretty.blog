import * as React from 'react';
import { Link, withRouter } from 'react-router';
import './Header.css';
import PrettyFetch from '@Components/PrettyFetch/PrettyFetch';
import UserInfoBlock from '@src/User/UserInfoBlock/UserInfoBlock';
import { fetchBlogList, setSearchKeyWord } from '@src/store/actions';
import { connect } from 'react-redux';
import { debounce } from 'underscore';

interface INavigation {
  name: string,
  url: string,
  order: number
}

function Header({
  dispatch,
  router
}: any) {
  const [current, setCurrent] = React.useState('');
  router.listen((i: any) => {
    setCurrent(i.pathname);
  });

  function renderNavItems(navs: INavigation[]) {
    return (
      navs.sort((x, y) => x.order < y.order ? -1 : 1).map((i, key) => (
        <React.Fragment key={key}>
          <li className="nav-item">
            <Link
              className={`nav-link ${current === i.url ? 'active text-primary' : ''}`}
              to={i.url}
              children={i.name} />
          </li>
        </React.Fragment>
      ))
    )
  }

  const keyPressHandler = debounce(
    (event: any) => {
      if (event.key === 'Enter') {
        const { value }: any = event.target;
        dispatch(setSearchKeyWord(value));
        dispatch(fetchBlogList({
          pageIndex: 0,
          pageSize: 10,
        }));
        router.push('/home');
      }
    }, 200);

  const searchTextKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    const { target, key } = event;
    keyPressHandler({ target, key });
  };

  return (
    <header>
      <nav className="navbar navbar-expand-lg justify-content-between">
        <div className="container">
          <Link className="navbar-brand" to="/home" >PRETTY'BLOG</Link>
          <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav">
            <span className="navbar-toggler-icon" />
            <span className="navbar-toggler-icon" />
            <span className="navbar-toggler-icon" />
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav mr-auto nav">
              <li className="d-lg-none d-flex drawer-header">
                <UserInfoBlock />
                <input type="text"
                  className="form-control"
                  placeholder="Search"
                  onKeyUp={searchTextKeyPress} />
              </li>
              <PrettyFetch url={`/navigation`}>
                {renderNavItems}
              </PrettyFetch>
            </ul>
          </div>
          <div className="d-lg-flex d-none align-items-center">
            <input type="text"
              className="form-control"
              placeholder="Search"
              onKeyUp={searchTextKeyPress} />
            <UserInfoBlock />
          </div>
        </div>
      </nav>
    </header>
  )
}

export default connect((state: any) => state)(withRouter(Header));