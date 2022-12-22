import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import {
  Course,
  useCourseService,
} from '../../Services/CourseService/useCourseService';
import { EditCourseForm } from './EditCourseForm';

export function EditCoursePage() {
  const [course, setCourse] = useState<Course>({
    id: 0,
    title: '',
    description: '',
    exercises: [],
    bannerName: '',
    repositoryName: '',
  });

  const services = useCourseService();
  const { id } = useParams();

  useEffect(() => {
    const handleService = async () => {
      const course: Course = await services.getCourseById(Number(id));
      setCourse(course);
    };
    handleService().catch(console.error);
  }, []);

  // const { state } = useLocation();
  // const course: Course = state;
  return (
    <div
      style={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        paddingTop: '20px',
      }}
    >
      <EditCourseForm
        key={course.id}
        course={course}
        editCourse={services.editCourse}
        // imagePreviewPath='/Images/SQL-Training-300x246.png'
        imagePreviewPath={`https://localhost:64936/Images/${course.bannerName}`}
      />
    </div>
  );
}
