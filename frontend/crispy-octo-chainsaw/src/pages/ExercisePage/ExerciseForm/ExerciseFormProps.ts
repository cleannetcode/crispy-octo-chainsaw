import { CreateExerciseData } from '../ExercisePage';

export interface ExerciseProps {
  createExercise: (data: CreateExerciseData) => Promise<void>;
}
