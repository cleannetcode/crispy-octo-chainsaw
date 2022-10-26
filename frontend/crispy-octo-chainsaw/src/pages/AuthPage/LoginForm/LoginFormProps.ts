import { LoginData } from '../AuthPage';

export interface LoginFormProps {
  login: (data: LoginData) => void;
}
