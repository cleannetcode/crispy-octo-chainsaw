import { Button, Form, Input, Select } from 'antd';
import { LockOutlined, UserOutlined } from '@ant-design/icons';
import React from 'react';
import { AdminRegistrationFormProps } from './AdminRegistrationFormProps';
import { useRegistationForm } from '../AuthPage/RegistrationForm/useRegistrationForm';

const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/;

export function AdminRegistrationForm(props: AdminRegistrationFormProps) {
  const data = useRegistationForm();

  const [form] = Form.useForm();

  const onFinish = async () => {
    await props.registraion({
      email: data.email,
      nickname: data.nickname,
      password: data.password,
    });
  };

  return (
    <div className='registration-form'>
      <h1 className='text-center mb-15'>Registration</h1>
      <Form form={form} name='register' onFinish={onFinish} scrollToFirstError>
        <Form.Item
          name='nicname'
          rules={[
            {
              required: true,
              message: 'Please input your nickname',
            },
          ]}
        >
          <Input
            value={data.nickname}
            prefix={<UserOutlined className='site-form-item-icon' />}
            placeholder='Nickname'
            onChange={(e) => data.handleRegistraionNickname(e.target.value)}
          />
        </Form.Item>
        <Form.Item
          name='email'
          rules={[
            {
              type: 'email',
              message: 'The input is not valid E-mail!',
            },
            {
              required: true,
              message: 'Please input your E-mail!',
            },
          ]}
        >
          <Input
            value={data.email}
            prefix={<UserOutlined className='site-form-item-icon' />}
            placeholder='Email'
            onChange={(e) => data.handleRegistraionEmail(e.target.value)}
          />
        </Form.Item>
        <Form.Item
          name='password'
          rules={[
            {
              required: true,
              message: 'Please input your password!',
              pattern: regex,
            },
          ]}
          hasFeedback
        >
          <Input.Password
            value={data.password}
            prefix={<LockOutlined className='site-form-item-icon' />}
            placeholder='Password'
            onChange={(e) => {
              data.handleRegistraionPassword(e.target.value);
            }}
          />
        </Form.Item>
        <Form.Item
          name='confirm'
          dependencies={['password']}
          hasFeedback
          rules={[
            {
              required: true,
              message: 'Please confirm your password!',
            },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue('password') === value) {
                  return Promise.resolve();
                }
                return Promise.reject(
                  new Error('The two passwords that you entered do not match!')
                );
              },
            }),
          ]}
        >
          <Input.Password
            placeholder='Confirm Password'
            prefix={<LockOutlined className='site-form-item-icon' />}
          />
        </Form.Item>
        <Form.Item>
          <Button type='primary' htmlType='submit'>
            Register
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
}
