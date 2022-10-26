import { CreateExerciseData } from '../ExercisePage';

export interface ExerciseProps {
  createExercise: (data: CreateExerciseData) => void;
}
