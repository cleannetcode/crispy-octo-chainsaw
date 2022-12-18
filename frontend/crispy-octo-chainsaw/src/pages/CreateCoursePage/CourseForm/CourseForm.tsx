import { Button, Form, Input, Tooltip } from 'antd';
import React, { useEffect, useRef, useState } from 'react';
import './CourseFormStyles.css';
import { CourseFormProps } from './CourseFormProps';
import { useCourseForm } from './useCourseForm';
import { QuestionCircleOutlined } from '@ant-design/icons';
import { useNavigate } from 'react-router-dom';

const { TextArea } = Input;

export function CourseForm(props: CourseFormProps) {
  const [image, setImage] = useState<File | null>(null);
  const [preview, setPreview] = useState<string>('');
  const navigate = useNavigate();
  const fileInputRef =
    useRef<HTMLInputElement>() as React.MutableRefObject<HTMLInputElement>;
  const {
    title,
    description,
    repositoryName,
    handleTitle,
    handleDescription,
    handleRepositoryName,
  } = useCourseForm();

  useEffect(() => {
    if (image) {
      handlePreview(image as File);
    }
  }, [image]);

  const handlePreview = (image: File) => {
    const reader = new FileReader();
    reader.onloadend = () => {
      setPreview(reader.result as string);
    };
    reader.readAsDataURL(image);
  };

  const handleImage = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file && file.type.substring(0, 5) === 'image') {
      setImage(file);
    }
  };

  const onFinish = () => {
    const formData = new FormData();
    formData.append('title', title);
    formData.append('description', description);
    formData.append('repositoryName', repositoryName);
    formData.append('image', image as File);
    props.createCourse(formData);
    navigate(-1);
  };

  return (
    <div className='create-course-form'>
      <h3>Banner</h3>
      {preview ? (
        <img
          className='banner-img'
          src={preview}
          style={{ objectFit: 'scale-down' }}
          onClick={() => setPreview('')}
        />
      ) : (
        <button
          className='button-image'
          onClick={(event) => {
            event.preventDefault();
            fileInputRef.current?.click();
          }}
        >
          Add image
        </button>
      )}

      <input
        type='file'
        style={{ display: 'none' }}
        ref={fileInputRef}
        accept='image/*'
        onChange={handleImage}
      />
      <Form
        name='create_course'
        initialValues={{ remember: true }}
        onFinish={onFinish}
      >
        <h3>Title</h3>
        <Form.Item
          name='title'
          rules={[{ required: true, message: 'Please input course title!' }]}
        >
          <Input
            type='title'
            value={title}
            onChange={(e) => handleTitle(e.target.value)}
            maxLength={500}
          />
        </Form.Item>
        <h3>Description</h3>
        <Form.Item
          name='description'
          rules={[
            { required: true, message: 'Please input course description!' },
          ]}
        >
          <TextArea
            value={description}
            showCount
            maxLength={1500}
            onChange={(e) => handleDescription(e.target.value)}
          />
        </Form.Item>
        <div className='flex-contrainer-repository-name'>
          <h3>Repository name</h3>
          <Tooltip
            placement='right'
            title={'example: https://github.com/YouAccountName/RepositoryName'}
          >
            <QuestionCircleOutlined />
          </Tooltip>
        </div>
        <Form.Item
          name='repositoryName'
          rules={[
            {
              required: true,
              message: 'Please input course respository name!',
            },
          ]}
        >
          <Input
            type='repositoryName'
            value={repositoryName}
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
