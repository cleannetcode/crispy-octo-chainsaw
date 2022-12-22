import React from 'react';
import PageWrapper from '../../components/PageWrapper';
import { useCourseService } from '../../Services/CourseService/useCourseService';
import { StorageAuthData } from '../../StorageAuthData';
import { CourseForm } from './CourseForm/CourseForm';
import './CourseForm/CourseFormStyles.css';

export interface CreateCourseData {
  formData: FormData;
}

const token = sessionStorage.getItem(StorageAuthData.AccessToken);

export function CreateCoursePage() {
  const courseServices = useCourseService();

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
