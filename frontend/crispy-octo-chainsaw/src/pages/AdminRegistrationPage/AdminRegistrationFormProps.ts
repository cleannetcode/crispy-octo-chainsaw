import { RegistrationData } from '../AuthPage/AuthPage';

export interface AdminRegistrationFormProps {
  registraion: (data: RegistrationData) => void;
}
