import { Button, Form, Input, Tooltip } from 'antd';
import React from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { PageRoots } from '../../../PageRoots';
import { ExerciseProps } from './ExerciseFormProps';
import './ExerciseFormStyles.css';
import { useExerciseForm } from './useExerciseForm';

const { TextArea } = Input;

export function ExerciseForm(props: ExerciseProps) {
  const {
    title,
    description,
    branchName,
    handleTitle,
    handleDescription,
    handleBranchName,
  } = useExerciseForm();

  const navigate = useNavigate();
  const id: number = useLocation().state;

  const onFinish = () => {
    props.createExercise({
      title: title,
      description: description,
      branchName: branchName,
      courseId: id,
    });
    navigate(`/${PageRoots.CourseAdminCatalog}/${PageRoots.Course}/${id}`);
  };

  return (
    <div className='create-form-exercise'>
      <Form
        name='create_exercise'
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <h3>Title</h3>
        <Form.Item
          name='title'
          rules={[{ required: true, message: 'Please input exercise title!' }]}
        >
          <Input
            onChange={(e) => handleTitle(e.target.value)}
            maxLength={500}
          />
        </Form.Item>
        <h3>Description</h3>
        <Form.Item
          name='description'
          rules={[
            { required: true, message: 'Please input exercise description!' },
          ]}
        >
          <TextArea
            showCount
            maxLength={1500}
            onChange={(e) => handleDescription(e.target.value)}
          />
        </Form.Item>
        <h3>Branch name</h3>
        <Form.Item
          name='branchName'
          rules={[
            {
              required: true,
              message: 'Please input course branch name!',
            },
          ]}
        >
          <Input
            maxLength={50}
            onChange={(e) => handleBranchName(e.target.value)}
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
