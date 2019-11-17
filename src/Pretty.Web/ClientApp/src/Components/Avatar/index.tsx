import * as React from 'react';
import './index.css';

export default ({ url, cls }: any) => <div className={`avatar-img ${cls}`} style={{ backgroundImage: `url(${url+ '&flag=50'})` }} />