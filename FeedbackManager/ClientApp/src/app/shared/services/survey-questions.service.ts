import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../models/question.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SurveyQuestionsService {
  private readonly ctrlUrl = 'SurveyQuestions';

  constructor(private http: HttpClient) {
  }

  getAllById(id: number): Observable<Question[]> {
    return this.http.get<Question[]>(`/${this.ctrlUrl}/${id}`);
  }

  create(id: number, question: Question) {
    return this.http.post(`/${this.ctrlUrl}/${id}`, question);
  }

  delete(id: number) {
    return this.http.delete(`/${this.ctrlUrl}/${id}`);
  }
}
