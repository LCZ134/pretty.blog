import * as React from 'react';
import TimeBlock from '../TimeBlock';
import { fetchPaged } from '@src/store/actions';
import path from 'ramda/es/path';
import { connect } from 'react-redux';
import Skeleton from '@src/Components/Skeleton';
import IBlogPost from '@typings/IBlogPost';
import * as dayJs from 'dayjs';
import './index.css';


function ArchiveContainer({
  dispatch,
  isFetching,
  paged
}: any) {

  const data: IBlogPost[] = paged && paged.data;

  React.useEffect(() => {
    dispatch(fetchPaged('blog', {
      pageIndex: 0,
      pageSize: 1000000
    }, 'archive'));
  }, ['isFetching']);

  if (isFetching) {
    return <Skeleton />
  } else {

    const groupData = {};

    data.forEach(blog => {
      const date = dayJs(blog.createOn).format('YYYY年MM月');

      if (groupData.hasOwnProperty(date)) {
        groupData[date].push(blog);
      } else {
        groupData[date] = [blog];
      }
    });

    return (
      <div className="archive-container text-left">
        {
          Object.keys(groupData).map((key, index) => index % 2 === 0 ?
            <div className="d-flex" key={key}>
              <div><TimeBlock key={key} direction="left" time={key} list={groupData[key]} /></div>
              <div />
            </div>
            :
            <div className="d-flex" key={key}>
              <div />
              <div><TimeBlock key={key} direction="right" time={key} list={groupData[key]} /></div>
            </div>
          )
        }
      </div>
    )
  }
}

const mapStateToProps = (state: any) => {
  return {
    ...(path(['posts', 'archive'], state) || {
      isFetching: true,
      paged: []
    })
  };
}

export default connect(mapStateToProps)(ArchiveContainer);