import {
  FETCH_PAGED_DATA,
  RECIVE_PAGED_DATA,
  RECIVE_BLOG_POST,
  FETCH_ERROR_BLOG_POST,
  THUMB_UP,
  FETCH_ACTION_ERROR,
  THUMB_UP_CANCEL,
  SET_BLOG_KEYWORD_FILTER,
  TOGGLE_LOGIN_CARD,
  UPDATE_USER,
  INSERT_COMMENT,
  FETCH_MESSAGE,
  INSERT_MESSAGE,
  ADD_BLOG_TAG_FEILTER,
  REMOVE_BLOG_TAG_FEILTER,
  FETCH_ONLINE_USERS
} from './actionTypes';
import { formatUrlParams, getFormData, isNullOrEmpty } from '@src/shared/utils';
import Modal from '@src/Components/Modal';
import store from './index';
import path from 'ramda/es/path';
import * as Cookies from 'js-cookie';
import IComment from '@typings/IComment';
import chatStore from '@src/Pulgins/chat';

/**
 * 获取实体对象
 * @param entity 请求的实体名称
 * @param params 参数对象
 * @param key 在 store 中存储的 key 值
 * @param paramsHash 参数 url hash 值，如果该参数非空，那么将直接采用该值，而非 params
 */
export const fetchPaged = (
  entity: string,
  params: object,
  key: string | null = null,
  paramsHash: string = '') => {
  return async (dispatch: any) => {
    dispatch({
      entity,
      type: FETCH_PAGED_DATA,
    });
    const url = `api/${entity}${isNullOrEmpty(paramsHash) ?
      formatUrlParams(params)
      : paramsHash}`;
    console.log(url);
    const res = await fetch(url);
    dispatch({
      data: {
        lastUpdated: new Date(),
        paged: await res.json(),
      },
      key: (key || entity),
      type: RECIVE_PAGED_DATA,
    });
  }
};

export const updateUser = (user: any) => {
  return (dispatch: any) => {
    Cookies.set('user', user);
    dispatch({
      type: UPDATE_USER,
      user
    });
  };
}

export const fetchBlogList = (options: object) => {
  const filter: any = path(['app', 'blogfilter'], store.getState()) || {};

  return (dispatch: any) => {
    const params = {
      ...filter,
      ...options
    };
    const paramsHash = '?' + Object.keys(params)
      .filter(key => params[key])
      .map(key => {
        if (key === 'tags') {
          return `${key}=${params[key]
            .map((tag: any) => tag.id)
            .join(',')}&`
        }
        return `${key}=${params[key]}&`;
      })
      .join('')
    dispatch(fetchPaged('blog', {}, null, paramsHash));
  };
}

export const fetchBlogPost = (id: string | undefined) => {
  return async (dispatch: any) => {
    if (!id) {
      errorHandle(dispatch, FETCH_ERROR_BLOG_POST, 'id 不存在');
      return;
    }
    let res: any;
    try {
      res = await fetch(`api/blog/${id}`);
    } catch (e) {
      errorHandle(dispatch, FETCH_ERROR_BLOG_POST, '数据请求错误');
    }

    dispatch({
      data: await res.json(),
      type: RECIVE_BLOG_POST,
    });
  }
}

export const thumbUp = (id: string) => {
  return async (dispatch: any) => {
    if (!id) {
      errorHandle(dispatch, FETCH_ERROR_BLOG_POST, 'id 不存在');
      return;
    }
    let res: any;
    try {
      res = await fetch(`api/thumbUp/${id}`, {
        method: 'POST'
      });
      actResultHandle(
        dispatch,
        await res.json(),
        () => {
          dispatch({
            id,
            type: THUMB_UP,
          });
        }, null);
    } catch (e) {
      errorHandle(dispatch, FETCH_ERROR_BLOG_POST, '数据请求错误');
    }
  }
}

export const fetchMessage = (from: Date | string, to: Date | string) => {
  return async (dispatch: any) => {
    await chatStore.loadChatByDate(from, to);
    await dispatch({
      type: FETCH_MESSAGE
    });
  }
}

export const insertMessage = (message: any) => {
  chatStore.insert(message);
  return async (dispatch: any) => {
    console.log('insert');
    dispatch({
      message,
      type: INSERT_MESSAGE,
    });
  }
}

export const getOnlineUsers = () => {
  return async (dispatch: any) => {
    const res = await fetch(`/api/user/online?pageSize=1000`);
    const data = await res.json();
    await dispatch({
      data: data.extends.data,
      type: FETCH_ONLINE_USERS
    });
  }
}

export const thumbUpCancel = (id: string) => {
  return async (dispatch: any) => {
    if (!id) {
      errorHandle(dispatch, FETCH_ACTION_ERROR, 'id 不存在');
      return;
    }
    try {
      const res = await fetch(`api/thumbUp/${id}`, {
        method: 'POST'
      });

      actResultHandle(
        dispatch,
        await res.json(),
        () => {
          dispatch({
            id,
            type: THUMB_UP_CANCEL,
          });
        }, null);

    } catch (e) {
      errorHandle(dispatch, FETCH_ACTION_ERROR, '数据请求错误\r\n' + e);
    }
  }
}

export const pushRootComment = (comment: IComment) => {
  return async (dispatch: any) => {
    dispatch({
      data: comment,
      type: INSERT_COMMENT,
    })
  }
}

export const insertComment = (comment: any) => {
  return async (dispatch: any) => {
    if (!comment || !comment.blogPostId) {
      errorHandle(dispatch, FETCH_ACTION_ERROR, 'id 不存在');
      return;
    }
    try {
      const res = await fetch(`api/comment`, {
        body: getFormData(comment),
        method: 'POST',
      });

      const act = await res.json();
      actResultHandle(dispatch, act);
      return act.extends;

    } catch (e) {
      errorHandle(dispatch, FETCH_ACTION_ERROR, '数据请求错误\r\n' + e);
    }
  }
}

export const toggleLoginCard = () => {
  return (dispatch: any) => {
    dispatch({
      type: TOGGLE_LOGIN_CARD
    });
  }
}

export const setSearchKeyWord = (keyword: string) => {
  return async (dispatch: any) => {
    dispatch({
      keyword,
      type: SET_BLOG_KEYWORD_FILTER
    })
  };
};

export const addTagFilter = (tag: any) => {
  return async (dispatch: any) => {
    await dispatch({
      tag,
      type: ADD_BLOG_TAG_FEILTER
    });
    await dispatch(fetchBlogList({
      pageIndex: 0,
      pageSize: 10,
    }));
  }
}

export const removeTagFilter = (tag: any) => {
  return async (dispatch: any) => {
    await dispatch({
      tag,
      type: REMOVE_BLOG_TAG_FEILTER
    })
    await dispatch(fetchBlogList({
      pageIndex: 0,
      pageSize: 10,
    }));
  }
}

function errorHandle(
  dispatch: any,
  type: string,
  msg: string) {
  Modal.alert(msg);
}

function actResultHandle(
  dispatch: any,
  act: any,
  success: any = null,
  failed: any = null
) {
  const { statusCode, result } = act;
  switch (statusCode) {
    case 0:
      if (typeof success === 'function') {
        success();
      }
      Modal.alert(result);
      break;
    case 100:
      dispatch(toggleLoginCard());
      Modal.alert(result, 'warning');
      break;
    default:
      if (typeof failed === 'function') {
        failed();
      }
      break;
  }

}