import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { CourseForm } from './CourseForm/CourseForm';

export interface CreateCourseData {
  title: string;
  description: string;
  repositoryName: string;
}

const token = '';

export function CoursePage() {
  const createCourse = async (data: CreateCourseData) => {
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
        <div className='create-course-page'>
          <CourseForm createCourse={createCourse} />
        </div>
      </PageWrapper>
    </>
  );
}
