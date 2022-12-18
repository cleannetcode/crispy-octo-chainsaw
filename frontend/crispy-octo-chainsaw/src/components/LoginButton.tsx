import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { ImportOutlined } from '@ant-design/icons';
import { PageRoots } from '../PageRoots';

function LoginButton() {
  const [size, setSize] = useState<SizeType>('middle');

  return (
    <Button
      icon={<ImportOutlined rotate={180} />}
      size={size}
      href={PageRoots.Login}
    >
      Login
    </Button>
  );
}

export default LoginButton;
