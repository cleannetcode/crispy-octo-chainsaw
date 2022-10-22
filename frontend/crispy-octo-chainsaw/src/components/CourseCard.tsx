import {
  EditOutlined,
  EllipsisOutlined,
  SettingOutlined,
} from "@ant-design/icons";
import { Avatar, Card } from "antd";
import React from "react";

const { Meta } = Card;

interface CourseCardProps {
  image: string;
  title: string;
  description: string;
}

export const CourseCard: React.FC<CourseCardProps> = ({
  image,
  title,
  description,
}) => (
  <>
    <Card style={{ width: 300 }} cover={<img alt="example" src={image} />}>
      <Meta
        avatar={<Avatar src="https://joeschmoe.io/api/v1/random" />}
        title={title}
        description={description}
      />
    </Card>
  </>
);
