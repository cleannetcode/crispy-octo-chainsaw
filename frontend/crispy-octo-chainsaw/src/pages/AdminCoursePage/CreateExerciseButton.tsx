import React, { useState } from 'react';
import { Button } from 'antd';
import type { SizeType } from 'antd/es/config-provider/SizeContext';

export function CreateExerciseButton() {
  const [size, setSize] = useState<SizeType>('large');

  return (
    <Button size={size} style={{ background: 'yellow' }}>
      Create exercise
    </Button>
  );
}
