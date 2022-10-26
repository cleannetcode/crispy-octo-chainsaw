import { LoginData, RegistrationData } from '../AuthPage';

export interface AuthTabsProps {
  registraion: (data: RegistrationData) => void;
  login: (data: LoginData) => void;
}
