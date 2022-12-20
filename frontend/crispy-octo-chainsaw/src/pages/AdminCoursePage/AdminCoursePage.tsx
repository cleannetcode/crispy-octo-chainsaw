import React, { useEffect, useState } from 'react';
import { Collapse, PageHeader, Typography } from 'antd';
import './AdminCoursePage.css';
import PageWrapper from '../../components/PageWrapper';
import Paragraph from 'antd/lib/typography/Paragraph';
import {
  Course,
  useCourseService,
} from '../../Services/CourseService/useCourseService';
import { CreateExerciseButton } from './CreateExerciseButton';
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom';
import { PageRoots } from '../../PageRoots';
import { fixtureCourses } from '../../SeedData';

export function AdminCoursePage() {
  const [course, setCourse] = useState<Course>();
  const courseService = useCourseService();
  const { id } = useParams();
  const navigate = useNavigate();
  const { state } = useLocation();
  const fixtureCourse: Course = state;
  console.log(fixtureCourse.exercises);
  const { Panel } = Collapse;

  useEffect(() => {
    const handleCourseService = async () => {
      const course = await courseService.getCourseById(Number(id));
      setCourse(course);
    };
    handleCourseService().catch(console.error);
  }, []);

  const goBack = () => navigate(`/${PageRoots.CourseAdminCatalog}`);

  const getExercises = () => {
    const exercises = fixtureCourses[0].exercises.map((exercise) => {
      return (
        <>
          <Collapse>
            <Panel header={exercise.title} key={exercise.id}>
              <p>{exercise.description}</p>
            </Panel>
          </Collapse>
        </>
      );
    });
    return exercises;
  };

  return (
    <PageWrapper>
      <div className='edit-course-page'>
        <PageHeader
          className='site-page-header'
          onBack={() => goBack()}
          title={fixtureCourse.title}
        />
        <div className='course-description'>
          <Typography>
            <Paragraph style={{ fontSize: 20, textAlign: 'justify' }}>
              {fixtureCourse?.description}
            </Paragraph>
          </Typography>
          <div className='pb-25'>{getExercises()}</div>
          <Link to={PageRoots.CreteExercise} state={Number(id)}>
            <CreateExerciseButton />
          </Link>
        </div>
      </div>
    </PageWrapper>
  );
}
