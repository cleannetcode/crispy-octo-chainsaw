import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthTabs } from './AuthTabs/AuthTabs';
import PageWrapper from '../../components/PageWrapper';

export interface Token {
  accessToken: string;
  refreshToken: string;
  nickname: string;
}

export interface RefreshTokenData {
  accessToken: string;
  refreshToken: string;
}

export interface RegistrationData {
  nickname: string;
  email: string;
  password: string;
}

export interface LoginData {
  email: string;
  password: string;
}

export function AuthPage() {
  let navigate = useNavigate();

  const registrAdmin = async (registraionData: RegistrationData) => {
    const response = await fetch(
      `https://localhost:64935/api/UsersAccount/user/registration`,
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(registraionData),
      }
    );
    if (response.ok) {
      await login({
        email: registraionData.email,
        password: registraionData.password,
      });

      const token = sessionStorage.getItem('authtokensuser');
      if (token !== null) {
        navigate('/');
      }
    }
  };

  const registrUser = async (registraionData: RegistrationData) => {
    const response = await fetch(
      `https://localhost:64935/api/UsersAccount/user/registration`,
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(registraionData),
      }
    );
    if (response.ok) {
      await login({
        email: registraionData.email,
        password: registraionData.password,
      });

      const token = sessionStorage.getItem('authtokensuser');
      if (token !== null) {
        navigate('/');
      }
    }
  };

  const login = async (loginData: LoginData) => {
    const response = await fetch(
      'https://localhost:64935/api/usersaccount/login',
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(loginData),
      }
    );
    if (response.ok) {
      const tokens: Token = await response.json();
      const jsonToken = JSON.stringify(tokens);
      sessionStorage.setItem('authtokensuser', jsonToken);
      navigate('/');
    }
  };

  const refreshAccessToken = async (refreshTokenData: RefreshTokenData) => {
    const response = await fetch(
      'https://localhost:64935/api/usersaccount/refreshaccesstoken',
      {
        method: 'post',
        headers: new Headers({ 'Content-type': 'application/json' }),
        body: JSON.stringify(refreshTokenData),
      }
    );
    if (response.ok) {
      const tokens: Token = await response.json();
      const jsonToken = JSON.stringify(tokens);
      sessionStorage.setItem('authtokensuser', jsonToken);
      navigate('/');
    }
  };

  return (
    <>
      <PageWrapper>
        <div className='registration-page'>
          <AuthTabs registraion={registrUser} login={login} />
        </div>
      </PageWrapper>
    </>
  );
}
