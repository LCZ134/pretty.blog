import * as React from 'react';
// import PrettyEditor from '../../Components/PrettyEditor/PrettyEditor';
import './CommentPublisher.css';
import PrettyEditor from '@Components/PrettyEditor/PrettyEditor.js';
import { connect } from 'react-redux';
import IComment from '@typings/IComment';
import { insertComment } from '@src/store/actions';

interface ICommentPublisherProps {
  blogPostId: string | undefined | null,
  parentId: string | undefined | null,
  content?: string,
  dispatch?: any,
  onSend: (e: IComment) => void
}

function CommentPublisher({
  blogPostId,
  parentId,
  dispatch,
  onSend }: ICommentPublisherProps)
  : JSX.Element {
  const [showCommentBtn, setShowCommentBtn] = React.useState(!!parentId);
  const [htmlContent, setHtmlContent] = React.useState(null);

  async function postComment() {
    const comment = Object.assign({
      content: htmlContent
    }, {
      blogPostId,
      parentId
    });
    onSend(await insertComment(comment)(dispatch));
  }

  function onEditorChange(html: any) {
    setHtmlContent(html);
  }

  function onEditorFocus() {
    setShowCommentBtn(true);
  }

  function onEditorBlur() {
    setShowCommentBtn(false);
  }

  return (
    <div className="comment-publisher" style={{ overflow: `hidden` }}>
      <PrettyEditor
        onchange={onEditorChange}
        onfocus={onEditorFocus}
        onblur={onEditorBlur} />
      <button onClick={postComment} className="btn btn-primary float-right btn-sm btn-send" style={{
        visibility:
          showCommentBtn ? 'visible' : 'hidden'
      }}>发送</button>
    </div>
  )
}

const mapStateToProps = (state: any) => {
  return {};
};

export default connect((mapStateToProps))(CommentPublisher);