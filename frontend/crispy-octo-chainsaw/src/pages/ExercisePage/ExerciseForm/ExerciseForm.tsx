import { Button, Form, Input, Tooltip } from 'antd';
import React from 'react';
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

  const onFinish = () => {
    props.createExercise({
      title: title,
      description: description,
      branchName: branchName,
    });
  };

  return (
    <div className='create-form-exercise'>
      <Form
        name='create_exercise'
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <Form.Item
          name='title'
          rules={[{ required: true, message: 'Please input exercise title!' }]}
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
            { required: true, message: 'Please input exercise description!' },
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
          name='branchName'
          rules={[
            {
              required: true,
              message: 'Please input course branch name!',
            },
          ]}
        >
          <h3>Branch name</h3>
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
