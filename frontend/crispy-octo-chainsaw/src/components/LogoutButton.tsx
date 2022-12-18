import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { ImportOutlined } from '@ant-design/icons';
import { PageRoots } from '../PageRoots';
import { StorageAuthData } from '../StorageAuthData';

function LogoutButton() {
  const [size, setSize] = useState<SizeType>('middle');

  const logout = () => {
    sessionStorage.removeItem(StorageAuthData.AccessToken);
    sessionStorage.removeItem(StorageAuthData.RefreshToken);
    sessionStorage.removeItem(StorageAuthData.Nickname);
  };

  return (
    <Button
      icon={<ImportOutlined />}
      size={size}
      onClick={() => {
        logout();
      }}
      href={PageRoots.Main}
    >
      Logout
    </Button>
  );
}

export default LogoutButton;
