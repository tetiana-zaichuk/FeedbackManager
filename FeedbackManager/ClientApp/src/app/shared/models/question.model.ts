import { Survey } from './survey.model';

export interface Question {
  id: number;
  questionName: string;
  shortComment: string;
  surveyId: number;
  survey: Survey;
  answers: string[];
}
