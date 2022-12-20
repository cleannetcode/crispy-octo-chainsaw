import { CreateCourseData } from '../CreateCoursePage';

export interface CourseFormProps {
  createCourse?: (data: FormData) => void;
  imagePreviewPath?: string;
}
