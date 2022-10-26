import { RegistrationData } from '../AuthPage';

export interface RegistrationFormProps {
  registraion: (data: RegistrationData) => void;
}
