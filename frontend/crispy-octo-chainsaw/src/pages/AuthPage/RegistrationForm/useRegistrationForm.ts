import { useState } from 'react';

interface useRegistationFormData {
  email: string;
  password: string;
  nickname: string;
  handleRegistraionEmail: (email: string) => void;
  handleRegistraionPassword: (password: string) => void;
  handleRegistraionNickname: (nickname: string) => void;
}

export const useRegistationForm = () => {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [nickname, setNickname] = useState<string>('');

  const handleRegistraionEmail = (email: string) => {
    setEmail(email);
  };

  const handleRegistraionPassword = (password: string) => {
    setPassword(password);
  };

  const handleRegistraionNickname = (nickname: string) => {
    setNickname(nickname);
  };

  let data: useRegistationFormData = {
    email,
    password,
    nickname,
    handleRegistraionEmail,
    handleRegistraionPassword,
    handleRegistraionNickname,
  };

  return data;
};
