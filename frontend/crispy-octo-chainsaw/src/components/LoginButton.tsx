import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { ImportOutlined } from '@ant-design/icons';
import { PageRoots } from '../PageRoots';
import { Link } from 'react-router-dom';

export function LoginButton() {
  const [size, setSize] = useState<SizeType>('middle');

  return (
    <Link to={`/${PageRoots.Login}`}>
      <Button icon={<ImportOutlined rotate={180} />} size={size}>
        Login
      </Button>
    </Link>
  );
}
