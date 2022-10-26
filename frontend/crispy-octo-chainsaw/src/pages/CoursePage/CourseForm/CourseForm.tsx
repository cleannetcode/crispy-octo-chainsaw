import { Button, Form, Input, Tooltip } from 'antd';
import React from 'react';
import { UploadImage } from '../UploadImage';
import './CourseFormStyles.css';
import { CourseFormProps } from './CourseFormProps';
import { useCourseForm } from './useCourseForm';
import { QuestionCircleOutlined } from '@ant-design/icons';

const { TextArea } = Input;

export function CourseForm(props: CourseFormProps) {
  const {
    title,
    description,
    repositoryName,
    handleTitle,
    handleDescription,
    handleRepositoryName,
  } = useCourseForm();

  const onFinish = () => {
    props.createCourse({
      title: title,
      description: description,
      repositoryName: repositoryName,
    });
  };

  return (
    <div className='create-form-course'>
      <h3>Banner</h3>
      <UploadImage />
      <Form
        name='create_course'
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <Form.Item
          name='title'
          rules={[{ required: true, message: 'Please input course title!' }]}
        >
          <h3>Title</h3>
          <Input
            onChange={(e) => handleTitle(e.target.value)}
            maxLength={500}
          />
        </Form.Item>
        <Form.Item
          name='description'
          rules={[
            { required: true, message: 'Please input course description!' },
          ]}
        >
          <h3>Description</h3>
          <TextArea
            showCount
            maxLength={1500}
            onChange={(e) => handleDescription(e.target.value)}
          />
        </Form.Item>
        <Form.Item
          name='repositoryName'
          rules={[
            {
              required: true,
              message: 'Please input course respository name!',
            },
          ]}
        >
          <div className='flex-contrainer-repository-name'>
            <h3>Repository name</h3>
            <Tooltip
              placement='right'
              title={
                'example: https://github.com/YouAccountName/RepositoryName'
              }
            >
              <QuestionCircleOutlined />
            </Tooltip>
          </div>

          <Input
            maxLength={50}
            onChange={(e) => handleRepositoryName(e.target.value)}
          />
        </Form.Item>
        <Form.Item>
          <Button
            type='primary'
            htmlType='submit'
            className='login-form-button'
          >
            Submit
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
}
