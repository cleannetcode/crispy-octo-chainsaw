import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { StorageAuthData } from '../../StorageAuthData';
import { ExerciseForm } from './ExerciseForm/ExerciseForm';

export interface CreateExerciseData {
  title: string;
  description: string;
  branchName: string;
  courseId: number;
}

const token = localStorage.getItem(StorageAuthData.AccessToken);

export function ExercisePage() {
  const createExercise = async (data: CreateExerciseData) => {
    const response = await fetch('https://localhost:64936/api/cms/exercises', {
      method: 'post',
      headers: new Headers({
        'Content-type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
      body: JSON.stringify(data),
    });
    if (!response.ok) {
    }
  };

  return (
    <>
      <PageWrapper>
        <div className='create-exercise-page'>
          <ExerciseForm createExercise={createExercise} />
        </div>
      </PageWrapper>
    </>
  );
}
