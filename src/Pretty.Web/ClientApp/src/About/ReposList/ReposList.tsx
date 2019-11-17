import * as React from 'react';
import PrettyFetch from '@src/Components/PrettyFetch/PrettyFetch';
import ReposItem from './ReposItem';
import './ReposList.css';

function ReposList() {

  return (
    <div className="repos-list">
      <PrettyFetch url={`/github`}>
        {
          (data: any) =>
            data.map((i: any, key: number) => {
              return <ReposItem repos={i} key={key} />
            })
        }
      </PrettyFetch>
    </div>
  )
}

export default ReposList;