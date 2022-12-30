import { PageRoots } from '../../PageRoots';
import { useNavigate } from 'react-router-dom';
import { StorageAuthData } from '../../StorageAuthData';
import { useStorage } from '../../hooks/useStorage';
import { useState } from 'react';

export interface AuthData {
  accessToken: string;
  refreshToken: string;
  nickname: string;
  role: string;
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

interface AuthService {
  registrAdmin: (registraionData: RegistrationData) => void;
  login: (loginData: LoginData) => void;
  registrUser: (registraionData: RegistrationData) => void;
  refreshAccessToken: (refreshTokenData: RefreshTokenData) => Promise<void>;
  isExpiredAccessToken: () => boolean;
  isExpiredRefreshTokenToken: () => boolean;
  getToken: () => Promise<string>;
  token: string;
}

export const useAuthService = (): AuthService => {
  let navigate = useNavigate();
  const storage = useStorage();
  const [token, setToken] = useState<string>(
    storage.getValueFromStorage(StorageAuthData.AccessToken)
  );

  const [refreshToken, setRefreshToken] = useState<string>(
    storage.getValueFromStorage(StorageAuthData.RefreshToken)
  );

  const setAuthDataToStorage = (data: AuthData) => {
    storage.setValueInstorage(StorageAuthData.AccessToken, data.accessToken);
    storage.setValueInstorage(StorageAuthData.RefreshToken, data.refreshToken);
    storage.setValueInstorage(StorageAuthData.Nickname, data.nickname);
    storage.setValueInstorage(StorageAuthData.Role, data.role);
  };

  const getToken = async (): Promise<string> => {
    if (isExpiredAccessToken()) {
      const refreshToken = storage.getValueFromStorage(
        StorageAuthData.RefreshToken
      );
      await refreshAccessToken({
        accessToken: token,
        refreshToken: refreshToken,
      });
      console.log('getToken');

      return storage.getValueFromStorage(StorageAuthData.AccessToken);
    }

    return storage.getValueFromStorage(StorageAuthData.AccessToken);
  };

  const isExpiredAccessToken = (): boolean => {
    if (!token) {
      return true;
    }

    const jwtPayload = token.split('.')[1];
    const jwtPayloadDecoded = window.atob(jwtPayload);
    const { exp } = JSON.parse(jwtPayloadDecoded);

    const accessTokenLifeTime = exp - Math.floor(Date.now() / 1000);

    return accessTokenLifeTime <= 0;
  };

  const isExpiredRefreshTokenToken = (): boolean => {
    if (!refreshToken) {
      return true;
    }

    const jwtPayload = refreshToken.split('.')[1];
    const jwtPayloadDecoded = window.atob(jwtPayload);
    const { exp } = JSON.parse(jwtPayloadDecoded);

    const refreshTokenLifeTime = exp - Math.floor(Date.now() / 1000);

    return refreshTokenLifeTime <= 0;
  };

  const registrUser = async (registraionData: RegistrationData) => {
    const response = await fetch(
      `https://localhost:64936/api/UsersAccount/user/registration`,
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
    }
  };

  const registrAdmin = async (registraionData: RegistrationData) => {
    const response = await fetch(
      `https://localhost:64936/api/UsersAccount/courseadmin/registration`,
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
    }
  };

  const login = async (loginData: LoginData) => {
    const response = await fetch(
      'https://localhost:64936/api/usersaccount/login',
      {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json',
        }),
        body: JSON.stringify(loginData),
      }
    );
    if (response.ok) {
      const tokens: AuthData = await response.json();
      setAuthDataToStorage(tokens);

      tokens.role === 'User'
        ? navigate(PageRoots.Main)
        : navigate(`/${PageRoots.CourseAdminCatalog}`);
    }
  };

  const refreshAccessToken = async (
    refreshTokenData: RefreshTokenData
  ): Promise<void> => {
    const response = await fetch(
      'https://localhost:64936/api/usersaccount/refreshaccesstoken',
      {
        method: 'post',
        headers: new Headers({ 'Content-type': 'application/json' }),
        body: JSON.stringify(refreshTokenData),
      }
    );
    if (response.ok) {
      const tokens: AuthData = await response.json();
      setToken(tokens.accessToken);

      setAuthDataToStorage(tokens);
    }
  };

  let services: AuthService = {
    registrAdmin,
    login,
    registrUser,
    refreshAccessToken,
    isExpiredAccessToken,
    isExpiredRefreshTokenToken,
    getToken,
    token,
  };

  return services;
};
