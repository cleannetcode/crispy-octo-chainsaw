import React from 'react';
import { Layout } from 'antd';
import { Token } from '../pages/AuthPage/AuthPage';
import Nickname from './Nickname';
import { StorageAuthData } from '../StorageAuthData';
import { LoginButton } from './LoginButton';
import { LogoutButton } from './LogoutButton';
import { useStorage } from '../hooks/useStorage';
const { Header } = Layout;

function HeaderComponent() {
  const storage = useStorage();

  const token: string | null = storage.getValueFromStorage(
    StorageAuthData.AccessToken
  );
  const nickname: string | null =
    storage.getValueFromStorage(StorageAuthData.Nickname) ?? '';

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

  return token === '' ? <>{renderLogin()}</> : <>{renderLogout(nickname)}</>;
}

export default HeaderComponent;
