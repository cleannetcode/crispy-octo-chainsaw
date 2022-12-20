import { PageHeader, Typography } from 'antd';
import React, { useEffect, useState } from 'react';
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom';
import PageWrapper from '../../components/PageWrapper';
import Paragraph from 'antd/lib/typography/Paragraph';

import { PageRoots } from '../../PageRoots';
import {
  Course,
  useCourseService,
} from '../../Services/CourseService/useCourseService';

export function UserCoursePage() {
  const [course, setCourse] = useState<Course>({
    id: 0,
    title: '',
    description: '',
    bannerName: '',
    repositoryName: '',
    exercises: [],
  });
  var { id } = useParams();
  const navigate = useNavigate();

  var sevices = useCourseService();
  useEffect(() => {
    const handleService = async () => {
      const course: Course = await sevices.getCourseById(Number(id));
      setCourse(course);
    };
    handleService().catch(console.error);
  }, []);

  const { state } = useLocation();

  // const course: Course = state;
  const goBack = () => navigate(PageRoots.Main);

  return (
    <>
      <PageWrapper>
        <div className='edit-course-page'>
          <PageHeader
            className='site-page-header'
            onBack={() => goBack()}
            title={course?.title}
          />
          <div className='course-description'>
            <Typography>
              <Paragraph style={{ fontSize: 20, textAlign: 'justify' }}>
                {course?.description}
              </Paragraph>
            </Typography>
          </div>
        </div>
      </PageWrapper>
    </>
  );
}
