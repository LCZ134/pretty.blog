import * as React from 'react';
import './TagList.css'
import PrettyFetch from '@src/Components/PrettyFetch/PrettyFetch';
import { addTagFilter } from '@src/store/actions';
import { connect } from 'react-redux';

interface IBlogPostTag {
  id: string,
  title: string,
  citationCount: number
}

function TagList({ dispatch }: any) {

  function tagOnClick(tag: any) {
    return () => dispatch(
      addTagFilter(tag)
    );
  }

  return (
    <div className="blog-post-tags card text-left">
      <div className="card-body">
        <h4 className="card-title">文章标签</h4>
        <div>
          <PrettyFetch url={`/tag`}>
            {
              (data: IBlogPostTag[]) =>
                data.map(i => (
                  <button onClick={tagOnClick(i)} key={i.id} type="button" className="btn btn-sm btn-primary btn-link float-left">
                    {i.title} <span className="badge badge-default">{i.citationCount}</span>
                  </button>
                ))
            }
          </PrettyFetch>
        </div>
      </div>
    </div>
  )
}

export default connect(state => state)(TagList);