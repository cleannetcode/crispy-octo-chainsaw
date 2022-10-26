import React from 'react';
import { Tabs } from 'antd';
import { RegistrationForm } from '../RegistrationForm/RegistrationForm';
import { LoginForm } from '../LoginForm/LoginForm';
import { AuthTabsProps } from './AuthTabsProps';

export function AuthTabs(props: AuthTabsProps) {
  return (
    <div>
      <Tabs defaultActiveKey='1'>
        <Tabs.TabPane tab='Login' key='1'>
          <LoginForm login={props.login} />
        </Tabs.TabPane>
        <Tabs.TabPane tab='Register' key='2'>
          <RegistrationForm registraion={props.registraion} />
        </Tabs.TabPane>
      </Tabs>
    </div>
  );
}
