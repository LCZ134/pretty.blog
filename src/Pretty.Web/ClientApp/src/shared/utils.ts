import { isNull, isUndefined } from 'util';

/**
 * stop natvie proppagation with react event
 * @param e react event
 */
export const stopNativePropagation: React.EventHandler<React.SyntheticEvent<any, Event>> = e =>
  e.nativeEvent.stopImmediatePropagation();

export const stopReactPropagation: React.EventHandler<React.SyntheticEvent<any, Event>> = e =>
  e.stopPropagation();

export const isNullOrEmpty = (str: string | undefined | null): boolean =>
  isNull(str) || isUndefined(str) || str === '';

/**
 * format url params
 * @param args url params
 */
export const formatUrlParams = (args: object | null | undefined): string => {
  let str = '';
  if (args) {
    str += '?';
    str += Object
      .keys(args)
      .filter(key => args[key])
      .map(key => `${key}=${args[key]}&`)
      .join('');
  } else {
    return str;
  }
  str.substr(str.length - 1, 1);
  return str;
}

export const getFormData = (data: object) => {
  const formData = new FormData();
  Object
    .keys(data)
    .filter(key => data[key])
    .forEach(key => formData.append(key, data[key]))
  return formData;
}

export const copyToCutBoard = (str: string, callback: (str: string) => void) => {
  const inputDOM = document.createElement('input');
  inputDOM.value = str;

  document.body.appendChild(inputDOM);

  inputDOM.select();// 选择对象

  document.execCommand("Copy");

  inputDOM.style.display = 'none';

  if (typeof callback === 'function') {
    callback(str);
  }
}

export const isScrollOnBottom = () => Math.abs(getScrollTop() + getWindowHeight() - getScrollHeight()) < 5;

export const showFileDialog = (onSelected: any, props: any = {}) => {
  const fileDOM = document.createElement('input')
  fileDOM.setAttribute('type', 'file');
  fileDOM.setAttribute('class', 'd-none');
  Object.keys(props).forEach(key => {
    fileDOM.setAttribute(key, props[key]);
  })
  fileDOM.addEventListener('change', (e) => {
    if (typeof onSelected === 'function') {
      onSelected(e);
    }
    document.body.removeChild(fileDOM);
  })
  document.body.appendChild(fileDOM);
  fileDOM.click();
};

export const loadScript = (src: string, cb: () => void) => {
  const dom = document.createElement('script');
  dom.src = src;
  dom.onload = () => {
    if (cb) {
      cb();
    }
  }
  document.head.appendChild(dom);
}

export const loadScripts = (sources: string[], cb: () => void) => {
  let loaded = 0;
  sources.forEach(src => {
    loadScript(src, () => {
      if (++loaded === sources.length) {
        if (cb) {
          cb();
        }
      }
    });
  });
}

function getScrollTop() {
  let scrollTop = 0;
  let bodyScrollTop = 0;
  let documentScrollTop = 0;
  if (document.body) {
    bodyScrollTop = document.body.scrollTop;
  }
  if (document.documentElement) {
    documentScrollTop = document.documentElement.scrollTop;
  }
  scrollTop = (bodyScrollTop - documentScrollTop > 0) ? bodyScrollTop : documentScrollTop;
  return scrollTop;
}

function getScrollHeight() {
  let scrollHeight = 0;
  let bodyScrollHeight = 0;
  let documentScrollHeight = 0;
  if (document.body) {
    bodyScrollHeight = document.body.scrollHeight;
  }
  if (document.documentElement) {
    documentScrollHeight = document.documentElement.scrollHeight;
  }
  scrollHeight = (bodyScrollHeight - documentScrollHeight > 0) ? bodyScrollHeight : documentScrollHeight;
  return scrollHeight;
}

function getWindowHeight() {
  let windowHeight = 0;
  if (document.compatMode === "CSS1Compat") {
    windowHeight = document.documentElement.clientHeight;
  } else {
    windowHeight = document.body.clientHeight;
  }
  return windowHeight;
}