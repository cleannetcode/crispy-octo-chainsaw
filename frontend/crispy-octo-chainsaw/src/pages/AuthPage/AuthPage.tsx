import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AuthTabs } from './AuthTabs/AuthTabs';
import PageWrapper from '../../components/PageWrapper';
import { PageRoots } from '../../PageRoots';
import { useAuthService } from '../../Services/AuthService/useAuthService';

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
  // let navigate = useNavigate();
  const authServices = useAuthService();
  // const registrUser = async (registraionData: RegistrationData) => {
  //   const response = await fetch(
  //     `https://localhost:64936/api/UsersAccount/user/registration`,
  //     {
  //       method: 'post',
  //       headers: new Headers({
  //         'Content-type': 'application/json',
  //       }),
  //       body: JSON.stringify(registraionData),
  //     }
  //   );
  //   if (response.ok) {
  //     await login({
  //       email: registraionData.email,
  //       password: registraionData.password,
  //     });

  //     const token = sessionStorage.getItem('authtokensuser');
  //     if (token !== null) {
  //       navigate(PageNames.Mane);
  //     }
  //   }
  // };

  // const login = async (loginData: LoginData) => {
  //   const response = await fetch(
  //     'https://localhost:64936/api/usersaccount/login',
  //     {
  //       method: 'post',
  //       headers: new Headers({
  //         'Content-type': 'application/json',
  //       }),
  //       body: JSON.stringify(loginData),
  //     }
  //   );
  //   if (response.ok) {
  //     const tokens: Token = await response.json();
  //     const jsonToken = JSON.stringify(tokens);
  //     sessionStorage.setItem('authtokensuser', jsonToken);
  //     navigate(PageNames.Mane);
  //   }
  // };

  // const refreshAccessToken = async (refreshTokenData: RefreshTokenData) => {
  //   const response = await fetch(
  //     'https://localhost:64936/api/usersaccount/refreshaccesstoken',
  //     {
  //       method: 'post',
  //       headers: new Headers({ 'Content-type': 'application/json' }),
  //       body: JSON.stringify(refreshTokenData),
  //     }
  //   );
  //   if (response.ok) {
  //     const tokens: Token = await response.json();
  //     const jsonToken = JSON.stringify(tokens);
  //     sessionStorage.setItem('authtokensuser', jsonToken);
  //     navigate(PageNames.Mane);
  //   }
  // };

  return (
    <>
      <PageWrapper>
        <div className='registration-page'>
          <AuthTabs
            registraion={authServices.registrUser}
            login={authServices.login}
          />
        </div>
      </PageWrapper>
    </>
  );
}
