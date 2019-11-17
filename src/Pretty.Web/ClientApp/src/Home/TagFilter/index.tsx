import * as React from 'react';
import { connect } from 'react-redux';
import path from 'ramda/es/path';
import './index.css';
import { removeTagFilter } from '@src/store/actions';

function TagFilter({
  dispatch,
  tagFilters
}: {
  dispatch: any
  tagFilters: any[]
}) {
  function tagOnClick(tag: any) {
    return () => dispatch(removeTagFilter(tag));
  }
  return (
    <div className="text-left tag-filter">
      {
        tagFilters &&
        tagFilters.map(tag =>
          <i key={tag.id} onClick={tagOnClick(tag)} className="badge badge-pill badge-info">{tag.title} X</i>
        )
      }
    </div>
  )
}

const mapStateToProps = (state: any) => {
  return {
    tagFilters: path(['app', 'blogfilter', 'tags'], state) || []
  }
}

export default connect(mapStateToProps)(TagFilter);