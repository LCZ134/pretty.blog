import * as React from 'react';
import { path } from 'ramda';
import './CommentItem.css';
import CommentPublisher from '../CommentPublisher/CommentPublisher';
import { stopNativePropagation } from '@src/shared/utils';
import Avatar from '@src/Components/Avatar';
import Modal from '@src/Components/Modal';
import IComment from '@typings/IComment';
import * as dayjs from 'dayjs';

interface ICommentItemProps {
  comment: IComment,
  parent: IComment | null
}

function CommentItem({ comment, parent }: ICommentItemProps): JSX.Element {
  const { user, content } = comment;
  const [showCmtPublisher, setShowCmtPulisher] = React.useState(false);
  const [commentChild, loadCommentChild] = React.useState([]);

  const toggleCmtPublisher: React.MouseEventHandler<Element> = e => {
    setShowCmtPulisher(!showCmtPublisher);
    attchDocEvent();
  }

  const onSend = (e: IComment) => {
    console.log(e);
    if (!e) { return }
    const newChild: any = [e, ...commentChild];
    loadCommentChild(newChild);
  }

  const attchDocEvent = () => document.addEventListener('click', onDocumentClick);

  const onDocumentClick = () => {
    setShowCmtPulisher(false);
    document.removeEventListener('click', onDocumentClick);
  }

  React.useEffect(() => {
    if (comment.childCount > 0) {
      fetch(`/api/comment?blogpostId=${comment.blogPostId}&parentId=${comment.id}`).then(res => {
        res.json().then(({ data }: { data: [] }) => {
          if (data) {
            loadCommentChild(data);
          } else {
            Modal.alert('评论数据获取失败');
          }
        })
      });
    }
  }, ['showCmtPublisher']);
  return (
    <li className="item">
      <div id={path(['nickname'], user)} className="comment-item text-left" onClick={stopNativePropagation}>
        <div className="comment-header d-flex align-items-center justify-content-between">
          <div className="d-flex align-items-center">
            <Avatar url={user && user.avatarUrl} cls="user-head" />
            <p className="nickname">
              {path(['nickName'], user)}
              {
                parent &&
                <a href={`#${path(['user', 'nickName'], parent)}`}> @ {path(['user', 'nickName'], parent)}</a>
              }
            </p>
          </div>
          <div className="right">
            <span>{comment.createOn && dayjs(comment.createOn).format('MM-DD HH:mm')}</span>
            <button className="btn btn-reply btn-primary btn-link btn-sm" onClick={toggleCmtPublisher}>回复</button>
          </div>
        </div>
        <div className="comment-content">
          <div className="content text-left" dangerouslySetInnerHTML={{ __html: content }} />
          {
            showCmtPublisher &&
            <CommentPublisher onSend={onSend} blogPostId={comment && comment.blogPostId} parentId={comment && comment.id} />
          }
        </div>
      </div>
      {
        commentChild.length > 0 &&
        <ul className="list">
          {
            commentChild.map((child, key) =>
              <CommentItem key={key} comment={child} parent={comment} />)
          }
        </ul>
      }
    </li>

  )
}

export default CommentItem;