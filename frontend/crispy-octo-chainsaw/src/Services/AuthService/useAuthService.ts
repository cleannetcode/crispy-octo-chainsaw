import { PageRoots } from '../../PageRoots';
import { useNavigate } from 'react-router-dom';
import { StorageAuthData } from '../../StorageAuthData';

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
  refreshAccessToken: (refreshTokenData: RefreshTokenData) => void;
}

export const useAuthService = (): AuthService => {
  let navigate = useNavigate();

  const setAuthDataToStorage = (data: AuthData) => {
    localStorage.setItem(StorageAuthData.AccessToken, data.accessToken);
    localStorage.setItem(StorageAuthData.RefreshToken, data.refreshToken);
    localStorage.setItem(StorageAuthData.Nickname, data.nickname);
    localStorage.setItem(StorageAuthData.Role, data.role);
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

  const refreshAccessToken = async (refreshTokenData: RefreshTokenData) => {
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
      setAuthDataToStorage(tokens);
    }
  };

  let services: AuthService = {
    registrAdmin,
    login,
    registrUser,
    refreshAccessToken,
  };
  return services;
};
