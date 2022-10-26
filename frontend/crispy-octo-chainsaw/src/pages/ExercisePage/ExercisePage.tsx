import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { ExerciseForm } from './ExerciseForm/ExerciseForm';

export interface CreateExerciseData {
  title: string;
  description: string;
  branchName: string;
}

const token = '';

export function ExercisePage() {
  const createExercise = async (data: CreateExerciseData) => {
    const response = await fetch('https://localhost:64935/api/cms/courses', {
      method: 'post',
      headers: new Headers({
        'Content-type': 'application/json',
        Authorization: `Bearer ${token}`,
        body: JSON.stringify(data),
      }),
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
