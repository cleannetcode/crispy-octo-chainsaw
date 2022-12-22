import { CreateCourseData } from '../CreateCoursePage';

export interface CourseFormProps {
  createCourse?: (data: FormData) => Promise<void>;
  imagePreviewPath?: string;
}
