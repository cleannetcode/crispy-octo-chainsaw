import React, { useState } from 'react';

export const useExerciseForm = () => {
  const [title, setTitle] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [branchName, setBranchName] = useState<string>('');

  const handleTitle = (title: string) => {
    setTitle(title);
  };

  const handleDescription = (description: string) => {
    setDescription(description);
  };

  const handleBranchName = (branchName: string) => {
    setBranchName(branchName);
  };

  return {
    title,
    description,
    branchName,
    handleTitle,
    handleDescription,
    handleBranchName,
  };
};
