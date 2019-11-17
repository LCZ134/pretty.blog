import * as React from 'react';
import './BlogPostPage.css';
import CommentList from '../CommentList/CommentList';
import CommentPublisher from '../CommentPublisher/CommentPublisher';
import { path } from 'ramda';
import { connect } from 'react-redux';
import { fetchBlogPost, thumbUp, thumbUpCancel, pushRootComment } from '@src/store/actions';
import Skeleton from '@src/Components/Skeleton';
import SkeletonLine from '@src/Components/Skeleton/SkeletonLine';
import Avatar from '@src/Components/Avatar';
import IComment from '@typings/IComment';
import './registerHighlight';
import { copyToCutBoard } from '@src/shared/utils';
import Modal from '@src/Components/Modal';

function BlogPostPage({
  isLiked,
  title,
  content,
  tags,
  user,
  createOn,
  updateOn,
  like,
  dispatch,
  blogPostId,
  mdContent
}: any) {

  function thumbUpHandle() {
    if (isLiked) {
      dispatch(thumbUpCancel(blogPostId));
    }
    else {
      dispatch(thumbUp(blogPostId));
    }
  }

  const onSend = (comment: IComment) => {
    if (!comment) { return }
    dispatch(pushRootComment(comment));
  }

  React.useEffect(() => {
    dispatch(fetchBlogPost(blogPostId));
  }, ['isLiked', 'blogPostId']);
  function shared() {
    const global: any = window;
    copyToCutBoard(global.location.href, () => {
      Modal.alert('文章链接已复制到剪切板，快去分享吧');
    });
  }
  return (
    <section className="blog-post-page m-auto card">
      <SkeletonLine data={title} style={{ height: `1.8rem`, marginLeft: `1rem` }}>
        {
          (data: any) =>
            <h2 className="text-title">{data}</h2>
        }
      </SkeletonLine>
      {
        content ?
          <div className="content text-left" dangerouslySetInnerHTML={{ __html: content }} /> :
          <Skeleton />
      }

      <div className="divider" id="post-info-area">信息</div>
      <div className="d-flex blog-post-meta">
        <div className="d-flex align-items-center">
          <SkeletonLine data={createOn} style={{ height: `4rem`, width: '4rem' }}>
            {
              (data: any) =>
                <div>
                  <Avatar url={user && user.avatarUrl} cls="user-head" />
                  <p className="nickname">{user && user.nickName}</p>
                </div>
            }
          </SkeletonLine>
          <div style={{ marginLeft: '1rem', textAlign: 'left', fontSize: '.8rem', color: 'gray' }}>

            <div>创建于：
              <SkeletonLine data={createOn}>
                {
                  (data: any) =>
                    <span>{data}</span>
                }
              </SkeletonLine>
            </div>
            <div>最近更新：
              <SkeletonLine data={updateOn || createOn}>
                {
                  (data: any) =>
                    <span>{data}</span>
                }
              </SkeletonLine>
            </div>
            <div>文章长度：
              <SkeletonLine data={mdContent && mdContent.length}>
                {
                  (data: any) =>
                    <span>{data}</span>
                }
              </SkeletonLine>
            </div>
          </div>
        </div>
        <div className="tags">
          {tags && tags.map((tag: any) =>
            <span className="tag badge badge-info" key={tag.id}>{tag.title}</span>
          )}
        </div>
        <div className="blog-post-toolbar">
          <div className="thumbUp_box">
            <i className={`material-icons blog-post-toolbar-item thumb-up ${isLiked ? 'active' : 'bg-white'}`} onClick={thumbUpHandle}>thumb_up_alt</i>
            <span className="thumbUp_count">{like}</span>
          </div>
          <a href="#post-info-area" className="bg-white material-icons thumb-up bg-white blog-post-toolbar-item">comment</a>
          <i className="bg-white material-icons thumb-up bg-white blog-post-toolbar-item" onClick={shared}>open_in_new</i>
        </div>
      </div>

      <h2 className="divider">
        <span id="comment-area">评论区</span>
      </h2>
      <CommentPublisher onSend={onSend} blogPostId={blogPostId} parentId={null} />
      <CommentList blogPostId={blogPostId} />
    </section>
  )
}

const mapStateToProps = (state: object) => {
  return {
    ...path(['posts', 'currentBlogPost', 'data'], state)
  } || {};
}

export default connect(mapStateToProps)(BlogPostPage);