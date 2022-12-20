import React from 'react';
import { Avatar, Card, Tooltip } from 'antd';
import Meta from 'antd/lib/card/Meta';
import { AntDesignOutlined, UserOutlined } from '@ant-design/icons';

interface GroupCardProps {
  id: number;
  title: string;
  description: string;
  repositoryName: string;
  bannerName: string;
}

// const imagePath: string = 'https://localhost:64936/Images';
const imagePath: string = 'Images';

export function UserCourseCard(props: GroupCardProps) {
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
          <img
            alt='example'
            style={{ objectFit: 'cover', height: '100%', width: '100%' }}
            src={`${imagePath}/${props.bannerName}`}
          />
        </div>
      }
      actions={[]}
      bodyStyle={{ height: '120px' }}
    >
      <Meta
        // avatar={<Avatar src='https://joeschmoe.io/api/v1/random' />}
        title={<div style={{ whiteSpace: 'break-spaces' }}>{props.title}</div>}
        description={<div style={{}}>Description</div>}
      />
    </Card>
  );
}
