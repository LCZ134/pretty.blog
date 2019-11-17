import {
  FETCH_PAGED_DATA,
  RECIVE_PAGED_DATA,
  RECIVE_BLOG_POST,
  THUMB_UP,
  THUMB_UP_CANCEL,
  INSERT_COMMENT,
  SET_BLOG_KEYWORD_FILTER,
  TOGGLE_LOGIN_CARD,
  UPDATE_USER,
  FETCH_MESSAGE,
  INSERT_MESSAGE,
  ADD_BLOG_TAG_FEILTER,
  REMOVE_BLOG_TAG_FEILTER,
  TOGGLE_CHATROOM,
  CLEAR_UNREAD,
  FETCH_ONLINE_USERS,
  ADD_ONLINE_USER,
  REMOVE_ONLINE_USER
} from './actionTypes';
import path from 'ramda/es/path';
import * as Cookies from 'js-cookie';
import chatStore from '@src/Pulgins/chat';

chatStore.loadChatByDate(chatStore.get().recentDate,new Date());

const defaultPostsState: any = {
  message: chatStore.get()
};

const defaultAppState: any = {
  user: Cookies.getJSON('user') || null
};

function getBlogPostById(state: any, id: string) {
  const pagedData: any[] = path(
    ['blog', 'paged', 'data'],
    state) || [];

  for (const item of pagedData) {
    if (item.id === id) {
      return item;
    }
  }
}

export const posts = (
  state = defaultPostsState,
  action: any) => {
  switch (action.type) {
    case FETCH_PAGED_DATA:
      return {
        ...state,
        [action.key]: {
          ...action.data,
          isClear: true,
          isFetching: true,
          lastUpdated: Date.now()
        },
      }
    case FETCH_MESSAGE: {
      return {
        ...state,
        message: {
          ...chatStore.get()
        }
      }
    }
    case INSERT_MESSAGE: {
      return {
        ...state,
        message: {
          ...chatStore.get()
        }
      }
    }
    case CLEAR_UNREAD:
      chatStore.clearUnread();
      return {
        ...state,
        ...chatStore.get()
      }
    case FETCH_ONLINE_USERS:
      return {
        ...state,
        onlineUsers: action.data
      }
    case ADD_ONLINE_USER: {
      const onlineUsers = [...(state.onlineUsers || []), action.user];
      return {
        ...state,
        onlineUsers
      }
    }
    case REMOVE_ONLINE_USER: {
      const onlineUsers = (state.onlineUsers || [])
        .filter((user: any) => user.id !== action.id);
      console.log(onlineUsers);
      return {
        ...state,
        onlineUsers
      }
    }
    case RECIVE_PAGED_DATA:
      return {
        ...state,
        [action.key]: {
          ...action.data,
          isClear: false,
          isFetching: false,
          lastUpdated: Date.now()
        }
      }
    case RECIVE_BLOG_POST:
      const currentBlogPost = {
        data: action.data
      }
      return {
        ...state,
        currentBlogPost
      }
    case THUMB_UP:
      const cur1: any = path(['currentBlogPost', 'data'], state);
      if (cur1) {
        cur1.like++;
        cur1.isLiked = true;
      }
      (getBlogPostById(state, action.id) || {}).like++;
      return {
        ...state
      }
    case THUMB_UP_CANCEL:
      const cur2: any = path(['currentBlogPost', 'data'], state);
      if (cur2) {
        cur2.like--;
        cur2.isLiked = false;
      }
      (getBlogPostById(state, action.id) || {}).like--;
      return {
        ...state
      }
    case INSERT_COMMENT:
      const paged: any = path(['comment', 'paged'], state) || {};
      paged.data = [action.data, ...paged.data];
      (getBlogPostById(state, action.blogPostId) || {}).commentCount++;
      return {
        ...state,
        comment: {
          ...state.comment,
          lastUpdated: Date.now()
        }
      }
    default:
      return state;
  }
}

export const app = (
  state = defaultAppState,
  action: any
) => {
  switch (action.type) {
    case SET_BLOG_KEYWORD_FILTER:
      return {
        ...state,
        blogfilter: {
          ...state.filter,
          keyword: action.keyword
        }
      }
    case TOGGLE_CHATROOM:
      const showChatRoom = !state.showChatRoom;
      return {
        ...state,
        showChatRoom
      }
    case UPDATE_USER:
      return {
        ...state,
        user: action.user
      }
    case ADD_BLOG_TAG_FEILTER: {
      let tags = (path<any[]>(['blogfilter', 'tags'], state) || []);
      console.log(tags.some(i => i.id === action.tag.id));
      if (!tags.some(i => i.id === action.tag.id)) {
        tags = [
          ...(path<any[]>(['blogfilter', 'tags'], state) || []),
          action.tag
        ];
      }
      return {
        ...state,
        blogfilter: {
          ...state.filter,
          tags
        }
      }
    }
    case REMOVE_BLOG_TAG_FEILTER: {
      const tags = [...(path<any[]>(['blogfilter', 'tags'], state) || [])];
      tags.splice(tags.indexOf(action.tag), 1);
      return {
        ...state,
        blogfilter: {
          ...state.filter,
          tags
        }
      }
    }
    case TOGGLE_LOGIN_CARD:
      return {
        ...state,
        showLoginCard: !state.showLoginCard
      }
    default:
      return state;
  }
}