import React, { useState } from 'react';

export const useCourseForm = () => {
  const [title, setTitle] = useState<string>('');
  const [description, setDescription] = useState<string>('');
  const [repositoryName, setRepositoryName] = useState<string>('');

  const handleTitle = (title: string) => {
    setTitle(title);
  };

  const handleDescription = (description: string) => {
    setDescription(description);
  };

  const handleRepositoryName = (repositoryName: string) => {
    setRepositoryName(repositoryName);
  };

  return {
    title,
    description,
    repositoryName,
    handleTitle,
    handleDescription,
    handleRepositoryName,
  };
};
