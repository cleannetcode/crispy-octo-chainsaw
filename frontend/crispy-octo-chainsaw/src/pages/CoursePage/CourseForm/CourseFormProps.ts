import { CreateCourseData } from '../CoursePage';

export interface CourseFormProps {
  createCourse: (data: CreateCourseData) => void;
}
