import React, { useEffect, useState } from 'react';
import { Layout } from 'antd';
import LoginButton from './LoginButton';
import LogoutButton from './LogoutButton';
import { Token } from '../pages/AuthPage/AuthPage';
import Nickname from './Nickname';
const { Header } = Layout;

function HeaderComponent() {
  const token: string | null = sessionStorage.getItem('authtokensuser');
  let nickname: string = '';
  if (token !== null) {
    const parseToken: Token = JSON.parse(token ?? '');
    nickname = parseToken.nickname;
  }

  const renderLogin = () => {
    return (
      <Header className='headerContent'>
        <div>
          <LoginButton />
        </div>
      </Header>
    );
  };
  const renderLogout = (nickname: string) => {
    return (
      <Header className='headerContent'>
        <Nickname nickname={nickname} />
        <LogoutButton />
      </Header>
    );
  };

  return token === null ? <>{renderLogin()}</> : <>{renderLogout(nickname)}</>;
}

export default HeaderComponent;
