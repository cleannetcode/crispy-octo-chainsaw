import { useState } from 'react';
import { StorageAuthData } from '../../StorageAuthData';
import { useAuthService } from '../AuthService/useAuthService';

interface CourseService {
  createCourse: (data: FormData) => void;
  editCourse: () => void;
  deleteCourse: () => void;
  getCourses: () => Promise<Course[]>;
  getCourseById: (id: number) => Course;
}

export interface Course {
  id: number;
  title: string;
  description: string;
  repositoryName: string;
  bannerName: string;
}

interface CreateCourseData {}
const token: string = sessionStorage.getItem(StorageAuthData.AccessToken) ?? '';
const host = 'https://localhost:64936';
const endpointroot = 'api/cms/courses';

export const useCourseService = (): CourseService => {
  const services = useAuthService();
  const createCourse = async (data: FormData) => {
    const response = await fetch('https://localhost:64936/api/cms/courses', {
      method: 'post',
      headers: new Headers({
        Authorization: `Bearer ${token}`,
      }),
      body: data,
    });
    if (!response.ok) {
      const refreshToken: string =
        sessionStorage.getItem(StorageAuthData.RefreshToken) ?? '';
      services.refreshAccessToken({
        accessToken: token,
        refreshToken: refreshToken,
      });
    }
  };

  const editCourse = () => {};

  const deleteCourse = () => {};

  const getCourses = async () => {
    const data = await fetch(`https://localhost:64936/api/cms/courses`, {
      method: 'get',
      headers: new Headers({
        'Content-type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    });
    const courses: Course[] = await data.json();
    console.log(courses);

    return courses;
  };

  const getCourseById = (id: number): Course => {
    const course: Course = {
      id: 0,
      description: '',
      repositoryName: '',
      title: '',
      bannerName: '',
    };
    return course;
  };

  const courseServices: CourseService = {
    createCourse,
    editCourse,
    deleteCourse,
    getCourses,
    getCourseById,
  };

  return courseServices;
};
