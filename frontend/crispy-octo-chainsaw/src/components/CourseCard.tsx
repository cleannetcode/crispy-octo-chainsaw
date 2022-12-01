import React from 'react';
import {
  EditOutlined,
  EllipsisOutlined,
  SettingOutlined,
} from '@ant-design/icons';
import { Avatar, Card } from 'antd';
import Meta from 'antd/lib/card/Meta';

interface GroupCardProps {
  id: number;
  title: string;
  description: string;
  repositoryName: string;
  bannerName: string;
}

const imagePath: string = 'https://localhost:64936/Images';

export function CourseCard(props: GroupCardProps) {
  console.log(props.bannerName);

  return (
    <Card
      key={props.id}
      style={{ width: 300, borderRadius: '20px', overflow: 'hidden' }}
      hoverable={true}
      cover={<img alt='example' src={`${imagePath}/${props.bannerName}`} />}
      actions={[
        <SettingOutlined key='setting' />,
        <EditOutlined key='edit' />,
        <EllipsisOutlined key='ellipsis' />,
      ]}
    >
      <Meta
        avatar={<Avatar src='https://joeschmoe.io/api/v1/random' />}
        title={props.title}
        description={props.description}
      />
    </Card>
  );
}
