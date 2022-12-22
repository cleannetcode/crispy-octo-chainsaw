import React from 'react';
import { EditOutlined, CloseCircleOutlined } from '@ant-design/icons';
import { Card } from 'antd';
import Meta from 'antd/lib/card/Meta';
import { Link } from 'react-router-dom';
import { PageRoots } from '../PageRoots';
import {
  Course,
  useCourseService,
} from '../Services/CourseService/useCourseService';

interface GroupCardProps {
  id: number;
  title: string;
  description: string;
  repositoryName: string;
  bannerName: string;
  handleIsClick: () => void;
}

const imagePath: string = 'https://localhost:64936/Images';
// const imagePath: string = 'Images';

export function CourseCard(props: GroupCardProps) {
  const course: Course = {
    id: props.id,
    title: props.title,
    description: props.description,
    repositoryName: props.repositoryName,
    bannerName: props.bannerName,
    exercises: [],
  };

  const services = useCourseService();

  const handleDeleteCourse = async () => {
    await services.deleteCourse(props.id);
    props.handleIsClick();
  };

  return (
    <Card
      key={props.id}
      style={{
        width: 350,
        borderRadius: '15px',
        overflow: 'hidden',
      }}
      hoverable={true}
      cover={
        <div style={{ overflow: 'hidden', height: '200px' }}>
          <Link to={`${PageRoots.Course}/${props.id}`}>
            <img
              alt='example'
              style={{ objectFit: 'cover', height: '100%', width: '100%' }}
              src={`${imagePath}/${props.bannerName}`}
            />
          </Link>
        </div>
      }
      actions={[
        <Link to={`${props.id}/${PageRoots.EditeCourse}`} state={course}>
          <EditOutlined key='edit' />,
        </Link>,
        <CloseCircleOutlined
          key='delete'
          onClick={async () => await handleDeleteCourse()}
        />,
      ]}
      bodyStyle={{ height: '120px' }}
    >
      <Meta
        title={<div style={{ whiteSpace: 'break-spaces' }}>{props.title}</div>}
        description={<div style={{}}>Description</div>}
      />
    </Card>
  );
}
