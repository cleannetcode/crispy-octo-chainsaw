import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { ImportOutlined } from '@ant-design/icons';
import { PageRoots } from '../PageRoots';
import { StorageAuthData } from '../StorageAuthData';
import { Link } from 'react-router-dom';
import { useStorage } from '../hooks/useStorage';

export function LogoutButton() {
  const [size, setSize] = useState<SizeType>('middle');
  const storage = useStorage();

  const logout = () => {
    storage.clearStorage(StorageAuthData.AccessToken);
    storage.clearStorage(StorageAuthData.RefreshToken);
    storage.clearStorage(StorageAuthData.Nickname);
    storage.clearStorage(StorageAuthData.Role);
  };

  return (
    <Link to={PageRoots.Main}>
      <Button
        icon={<ImportOutlined />}
        size={size}
        onClick={() => {
          logout();
        }}
      >
        Logout
      </Button>
    </Link>
  );
}
