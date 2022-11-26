import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { CourseForm } from './CourseForm/CourseForm';

export interface CreateCourseData {
  // title: string;
  // description: string;
  // repositoryName: string;
  // image: File | undefined;
  formData: FormData;
}

const token = '';

export function CoursePage() {
  const createCourse = async (data: FormData) => {
    const response = await fetch('https://localhost:64936/api/cms/courses', {
      method: 'post',
      headers: new Headers({
        // 'Content-type': 'multipart/form-data',
        Authorization: `Bearer ${token}`,
        // Accept: 'application/json, application/xml, text/plain, text/html, *.*',
      }),
      body: data,
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
