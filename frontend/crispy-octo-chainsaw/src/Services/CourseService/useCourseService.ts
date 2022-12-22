import { json } from 'stream/consumers';
import { StorageAuthData } from '../../StorageAuthData';
import { useAuthService } from '../AuthService/useAuthService';

interface CourseService {
  createCourse: (data: FormData) => Promise<void>;
  editCourse: (id: number, data: FormData) => Promise<void>;
  deleteCourse: (id: number) => Promise<void>;
  getCourseAdminCourses: () => Promise<Course[]>;
  getCourseById: (id: number) => Promise<Course>;
  getAllCourses: () => Promise<Course[]>;
}

export interface Course {
  id: number;
  title: string;
  description: string;
  repositoryName: string;
  bannerName: string;
  exercises: Exercise[];
}

interface Exercise {
  id: number;
  title: string;
  description: string;
}

const token: string = localStorage.getItem(StorageAuthData.AccessToken) ?? '';
const host = 'https://localhost:64936';
const endpointrootcms = '/api/cms/courses';
const endpointroot = '/api/courses';

export const useCourseService = (): CourseService => {
  const services = useAuthService();

  const getAllCourses = async (): Promise<Course[]> => {
    const response = await fetch(host + endpointroot, {
      method: 'get',
      headers: new Headers({ 'Content-type': 'application/json' }),
    });
    const courses: Course[] = await response.json();
    return courses;
  };

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
      await createCourse(data);
    }
  };

  const editCourse = async (id: number, data: FormData) => {
    const response = await fetch(
      `https://localhost:64936/api/cms/courses/${id}`,
      {
        method: 'put',
        headers: new Headers({
          Authorization: `Bearer ${token}`,
        }),
        body: data,
      }
    );
  };

  const deleteCourse = async (id: number) => {
    const response = await fetch(
      `https://localhost:64936/api/cms/courses/${id}`,
      {
        method: 'delete',
        headers: new Headers({
          'Content-type': 'application/json',
          Authorization: `Bearer ${token}`,
        }),
      }
    );
  };

  const getCourseAdminCourses = async () => {
    const data = await fetch(`https://localhost:64936/api/cms/courses`, {
      method: 'get',
      headers: new Headers({
        'Content-type': 'application/json',
        Authorization: `Bearer ${token}`,
      }),
    });
    const courses: Course[] = await data.json();

    return courses;
  };

  const getCourseById = async (id: number) => {
    const response = await fetch(
      `https://localhost:64936/api/cms/courses/${id}`,
      {
        method: 'get',
        headers: new Headers({
          'Content-type': 'application/json',
          Authorization: `Bearer ${token}`,
        }),
      }
    );
    const course: Course = await response.json();
    return course;
  };

  const courseServices: CourseService = {
    createCourse,
    editCourse,
    deleteCourse,
    getCourseAdminCourses: getCourseAdminCourses,
    getCourseById,
    getAllCourses,
  };

  return courseServices;
};
