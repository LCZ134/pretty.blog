import * as SignalR from '@aspnet/signalr';
import store from '@src/store';
import { insertMessage } from '@src/store/actions';
import { ADD_ONLINE_USER, REMOVE_ONLINE_USER } from '@src/store/actionTypes';
import path from 'ramda/es/path';

const connection = new SignalR.HubConnectionBuilder()
  .withUrl('/chathub')
  .build();

connection.on("messageReceived", (message: any) => {
  console.log('recived', message);
  switch (message.type) {
    case 'NewMessage':
      insertMessage(message)(store.dispatch);
      break;
    case 'Connect':
      store.dispatch({
        type: ADD_ONLINE_USER,
        user: message.user
      });
      break
    case 'DisConnect':
      store.dispatch({
        id: path(['user', 'id'], message),
        type: REMOVE_ONLINE_USER
      });
      break;
    default:
      break;
  }
});

connection.start().catch(console.log);

function sendMessage(message: string) {
  connection.send("newMessage", message)
    .then(() => console.log('sended'));
}

export default {
  sendMessage
};