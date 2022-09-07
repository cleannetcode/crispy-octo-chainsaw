import { Layout } from 'antd';
import React, { useState } from 'react';
import HeaderComponent from '../../components/HeaderComponent';
import { useNavigate } from 'react-router-dom';
import AuthTabs from './AuthTabs';
import PageWrapper from '../../components/PageWrapper';
export interface Token {
  accessToken: string;
  refreshToken: string;
  nickname: string;
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
function AuthPage() {
  let navigate = useNavigate();

  const registrAdmin = async (registraionData: RegistrationData) => {
    const data = await fetch(
      `https://localhost:64935/api/UsersAccount/user/registration`,
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(registraionData),
      }
    );

    await login({
      email: registraionData.email,
      password: registraionData.password,
    });

    const token = sessionStorage.getItem('authtokensuser');
    if (token !== null) {
      navigate('/');
    }
  };

  const registrUser = async (registraionData: RegistrationData) => {
    const data = await fetch(
      `https://localhost:64935/api/UsersAccount/user/registration`,
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(registraionData),
      }
    );
    console.log(registraionData);

    await login({
      email: registraionData.email,
      password: registraionData.password,
    });

    const token = sessionStorage.getItem('authtokensuser');
    if (token !== null) {
      navigate('/');
    }
  };

  const login = async (loginData: LoginData) => {
    const data = await fetch('https://localhost:64935/api/usersaccount/login', {
      method: 'post',
      headers: new Headers({
        'Content-type': 'application/json',
      }),
      body: JSON.stringify(loginData),
    });

    const tokens: Token = await data.json();
    const jsonToken = JSON.stringify(tokens);
    sessionStorage.setItem('authtokensuser', jsonToken);
    navigate('/');
  };

  const refreshAccessToken = async () => {};

  return (
    <div>
      <PageWrapper>
        <div className='registration-page'>
          <AuthTabs registraion={registrUser} login={login} />
        </div>
      </PageWrapper>
    </div>
  );
}

export default AuthPage;
