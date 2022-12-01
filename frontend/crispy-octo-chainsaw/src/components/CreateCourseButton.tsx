import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';
import { PageNames } from '../PageName';

export function CreateCourseButton() {
  const [size, setSize] = useState<SizeType>('middle');

  return (
    <Button size={size} href={PageNames.CreteCourse}>
      Create course
    </Button>
  );
}
