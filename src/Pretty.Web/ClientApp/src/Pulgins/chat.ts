import localStore from "./localStore";
import Modal from '@src/Components/Modal';
import * as dayjs from 'dayjs';

class ChatStore {
  private message: any;
  constructor() {
    const message = localStore.getJSON('message');
    if (message) {
      this.message = localStore.getJSON('message');
    } else {
      const defaultPrevDate = new Date();
      defaultPrevDate.setHours(new Date().getHours() - 24 * 7);
      const dateNow = new Date();
      this.message = {
        data: [],
        prevDate: dateNow,
        recentDate: dateNow,
        unread: 0,
      };
      this.loadChatByDate(defaultPrevDate, dateNow);
    }
    this.saveChange();
  }
  public getDateStr = (date: any) => dayjs(date).format('YYYY-MM-DD HH:mm:ss');
  public insert(message: any) {
    console.log('insert a');
    if (typeof message === 'object') {
      this.message.data.push(message);
      console.log([...this.message.data])
      this.message.unread++;
      this.message.recentDate = message.createOn;
      this.saveChange();
    }
  }
  public saveChange() {
    localStore.setJSON('message', this.message);
  }
  public get() {
    return this.message;
  }
  public clearUnread() {
    this.message.unread = 0;
    this.saveChange();
  }
  public async loadChatByDate(
    from: Date | string,
    to: Date | string,
    cb?: (message: any) => void | null
  ) {
    const fromObj = dayjs(from);
    const recentObj = dayjs(this.message.recentDate);
    if (fromObj.isBefore(this.message.prevDate)) {
      this.message.prevDate = from;
    } else if (recentObj.isBefore(new Date())) {
      from = this.message.recentDate;
      to = new Date();
    } else {
      return;
    }
    const res = await fetch(`/api/message?type=message&from=${this.getDateStr(from)}&to=${this.getDateStr(to)}`);
    const result = await res.json();
    if (!result.data) {
      Modal.alert('请求消息失败了，请稍后重试。。', 'wraning');
      return;
    }
    console.log([...this.message.data], [...result.data]);
    const oldLength = this.message.data.length;
    result.data = result.data
      .filter((i: any) =>
        !this.message.data.some((x: any) => x.id === i.id));
    let data = [
      ...this.message.data,
      ...result.data
    ];
    data = data.sort((prev: any, next: any) => {
      if (prev.createOn > next.createOn) {
        return 1;
      } else if (prev.createOn < next.createOn) {
        return -1;
      } else {
        return 0;
      }
    });
    this.message.data = data;
    this.message.recentDate = new Date();
    this.message.unread = data.length - oldLength;
    this.saveChange();
    if (cb) {
      cb(this.message);
    }
  }
}

const chatStore = new ChatStore();

export default chatStore;