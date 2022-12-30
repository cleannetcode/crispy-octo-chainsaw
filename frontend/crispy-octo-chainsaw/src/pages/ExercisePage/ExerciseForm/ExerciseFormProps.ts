import { CreateExerciseData } from '../../../hooks/useExerciseService';

export interface ExerciseProps {
  createExercise: (data: CreateExerciseData) => Promise<void>;
}
