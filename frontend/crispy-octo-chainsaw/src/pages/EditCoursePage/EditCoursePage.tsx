import React from 'react';
import { PageHeader, Typography } from 'antd';
import './EditCoursePageStyles.css';
import PageWrapper from '../../components/PageWrapper';
import Paragraph from 'antd/lib/typography/Paragraph';

export function EditCoursePage() {
  return (
    <PageWrapper>
      <div className='edit-course-page'>
        <PageHeader
          className='site-page-header'
          onBack={() => null}
          title='Title'
          subTitle='This is a subtitle'
        />
        <div className='course-description'>
          <Typography>
            <Paragraph>
              In the process of internal desktop applications development, many
              different design specs and implementations would be involved,
              which might cause designers and developers difficulties and
              duplication and reduce the efficiency of development.
            </Paragraph>
          </Typography>
        </div>
      </div>
    </PageWrapper>
  );
}
