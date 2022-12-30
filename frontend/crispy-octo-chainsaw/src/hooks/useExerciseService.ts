import { useState } from 'react';
import { useAuthService } from '../Services/AuthService/useAuthService';

export interface CreateExerciseData {
  title: string;
  description: string;
  branchName: string;
  courseId: number;
}

export interface useExerciseService {
  createExercise: (data: CreateExerciseData) => Promise<void>;
}

export const useExerciseService = (): useExerciseService => {
  const services = useAuthService();
  const [token, setToken] = useState<Promise<string>>(services.getToken());

  const createExercise = async (data: CreateExerciseData) => {
    const response = await fetch('https://localhost:64936/api/cms/exercises', {
      method: 'post',
      headers: new Headers({
        'Content-type': 'application/json',
        Authorization: `Bearer ${await token}`,
      }),
      body: JSON.stringify(data),
    });
  };

  return { createExercise };
};
