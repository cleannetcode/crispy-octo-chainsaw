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
  const authServices = useAuthService();

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
