import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import PageWrapper from '../../components/PageWrapper';
import { UserCourseCard } from '../../components/UserCourseCard';
import { useStorage } from '../../hooks/useStorage';
import { PageRoots } from '../../PageRoots';
import { fixtureCourses } from '../../SeedData';
import {
  Course,
  useCourseService,
} from '../../Services/CourseService/useCourseService';
import { StorageAuthData } from '../../StorageAuthData';
import './MainPage.css';

export function MainPage() {
  const [courses, setCourses] = useState<Course[]>();
  const services = useCourseService();
  const storage = useStorage();

  useEffect(() => {
    const serviceHandler = async () => {
      const courses: Course[] = await services.getAllCourses();
      setCourses(courses);
    };
    serviceHandler().catch(console.error);
  }, []);

  const showCourses = () => {
    const data = courses?.map((course: Course) => {
      return (
        <Link to={`${PageRoots.Course}/${course.id}`} key={course.id}>
          <UserCourseCard
            key={course.id}
            id={course.id}
            title={course.title}
            description={course.description}
            bannerName={course.bannerName}
            repositoryName={course.repositoryName}
          />
        </Link>
      );
    });
    return data;
  };

  return (
    <PageWrapper>
      <div className='main-page-container'>{showCourses()}</div>
    </PageWrapper>
  );
}
