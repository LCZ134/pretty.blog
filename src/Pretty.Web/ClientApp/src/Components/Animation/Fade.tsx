import * as React from 'react';
import { TransitionGroup, CSSTransition } from 'react-transition-group';

function Fade({ children }: any) {
  return (
    <TransitionGroup>
      <CSSTransition
        appear={true}
        classNames="appAppear"
        timeout={500}>
        {children}
      </CSSTransition>
    </TransitionGroup>
  )
}

export default Fade;