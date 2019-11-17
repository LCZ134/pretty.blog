import * as React from 'react';
import { Fetch } from 'react-request';
import { formatUrlParams } from '@src/shared/utils';
import './PrettyFetch.css';

interface IPrettyFetchProps {
  url: string,
  args?: object,
  children?: any,
  fetchProps?: object
}

const PrettyFetch = (props: IPrettyFetchProps) => {
  const { url, args, children, fetchProps } = props;

  return (
    <Fetch
      {...fetchProps}
      url={`/api${url}` + formatUrlParams(args)}
    >
      {({ fetching, failed, data }) => {
        if (fetching) {
          return <p className="skeleton-line-loading">&nbsp;</p>;
        }

        if (failed) {
          // 当前组件请求失败
          return <p>数据请求失败了 -_-||</p>;
        }

        if (data && children) {
          // 请求成功
          return children(data);
        }

        return null;
      }}
    </Fetch>
  );
};


export default PrettyFetch