import { Checkbox, Layout } from 'antd';
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
import { Link } from 'react-router-dom';
import { PageRoots } from '../../PageRoots';
import { fixtureCourses } from '../../SeedData';

export function CourseAdminCatalogPage() {
  const [courses, setCourses] = useState<Course[]>();
  const courseSevices = useCourseService();
  const [isClick, setIsClick] = useState<boolean>(false);

  const handleSetisClick = () => {
    setIsClick(!isClick);
    console.log(isClick);
  };

  useEffect(() => {
    const handleCourseService = async () => {
      const courses = await courseSevices.getCourses();
      console.log(courses);

      setCourses(courses);
    };
    handleCourseService().catch(console.error);
  }, [isClick]);

  const getCourses = () => {
    const coursesList = fixtureCourses?.map((course: Course) => {
      return (
        <>
          <CourseCard
            key={course.id}
            id={course.id}
            title={course.title}
            description={course.description}
            repositoryName={course.repositoryName}
            bannerName={course.bannerName}
            handleIsClick={handleSetisClick}
          />
        </>
      );
    });
    return coursesList;
  };

  return (
    <div>
      <PageWrapper>
        <Content>
          <div className='course-page-content'>
            <div className='mb-20'>
              <CreateCourseButton />
            </div>
            <div className='card-content'>{getCourses()}</div>
          </div>
        </Content>
      </PageWrapper>
    </div>
  );
}
