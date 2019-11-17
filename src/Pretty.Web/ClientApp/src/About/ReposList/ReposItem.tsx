import * as React from 'react';

function ReposItem({ repos }: any) {
  return (
    <div className="github-repos item card  text-left">
      <div className="card-body">
        <h4 className="card-title">
          <a href={repos.htmlUrl} target="_blank">{repos.name}</a>
        </h4>
        <p className="card-text text-overflow-ellipsis" style={{ height: `3rem` }}>{repos.description}</p>
        <span>{repos.language}</span>
        <a href="#0" className="card-link text-secondary">
          <i className="material-icons">star</i>
          <span>{repos.stargazersCount}</span>
        </a>
      </div>
    </div>
  )
}

export default ReposItem;