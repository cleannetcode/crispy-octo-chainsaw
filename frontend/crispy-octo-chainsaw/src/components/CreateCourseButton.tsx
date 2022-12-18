import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { PageRoots } from '../PageRoots';
import { Link } from 'react-router-dom';

export function CreateCourseButton() {
  const [size, setSize] = useState<SizeType>('large');

  return (
    <Link to={`/${PageRoots.CreteCourse}`}>
      <Button size={size} style={{ background: 'yellow' }}>
        Create course
      </Button>
    </Link>
  );
}
