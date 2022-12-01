import { Layout } from 'antd';
import { Content } from 'antd/lib/layout/layout';
import React, { useEffect, useState } from 'react';
import { CreateCourseButton } from '../../components/CreateCourseButton';
import { CourseCard } from '../../components/CourseCard';
import PageWrapper from '../../components/PageWrapper';
import {
  Course,
  useCourseService,
} from '../../Services/CourseService/useCourseService';
import './CourseAdminCatalog.css';

export function CourseAdminCatalogPage() {
  const [courses, setCourses] = useState<Course[]>();
  const courseSevices = useCourseService();

  useEffect(() => {
    const handleCourseService = async () => {
      const courses = await courseSevices.getCourses();
      console.log(courses);

      setCourses(courses);
    };
    handleCourseService().catch(console.error);
  }, []);

  const getCourses = () => {
    const coursesList = courses?.map((course: Course) => {
      return (
        <CourseCard
          key={course.id}
          id={course.id}
          title={course.title}
          description={course.description}
          repositoryName={course.repositoryName}
          bannerName={course.bannerName}
        />
      );
    });
    return coursesList;
  };

  return (
    <div>
      <PageWrapper>
        <Content>
          <div className='course-admin-page-content'>
            <CreateCourseButton />
            {getCourses()}
          </div>
        </Content>
      </PageWrapper>
    </div>
  );
}
