import { Question } from './question.model';

export interface Survey {
  id: number;
  creatorName: string;
  surveyName: string;
  description: string;
  createdAt: Date;
  questions: Question[];
}
