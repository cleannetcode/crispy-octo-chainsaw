import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { useCourseService } from '../../Services/CourseService/useCourseService';
import { StorageAuthData } from '../../StorageAuthData';
import { CourseForm } from './CourseForm/CourseForm';

export interface CreateCourseData {
  formData: FormData;
}

const token = sessionStorage.getItem(StorageAuthData.AccessToken);

export function CreateCoursePage() {
  const courseServices = useCourseService();
  // const createCourse = async (data: FormData) => {
  //   const response = await fetch('https://localhost:64936/api/cms/courses', {
  //     method: 'post',
  //     headers: new Headers({
  //       Authorization: `Bearer ${token}`,
  //     }),
  //     body: data,
  //   });
  //   if (!response.ok) {
  //   }
  // };

  return (
    <>
      <PageWrapper>
        <div className='create-course-page'>
          <CourseForm createCourse={courseServices.createCourse} />
        </div>
      </PageWrapper>
    </>
  );
}
