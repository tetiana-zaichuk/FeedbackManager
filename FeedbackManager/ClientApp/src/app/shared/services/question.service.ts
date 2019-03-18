import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Question } from '../models/question.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class QuestionService {
  private readonly ctrlUrl = 'question';

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<Question[]> {
    return this.http.get<Question[]>(`/${this.ctrlUrl}`);
  }

  get(id: number): Observable<Question> {
    return this.http.get<Question>(`/${this.ctrlUrl}/${id}`);
  }

  create(question: Question) {
    return this.http.post(`/${this.ctrlUrl}`, question);
  }

  update(id: number, question: Question) {
    return this.http.put(`/${this.ctrlUrl}/${id}`, question);
  }

  delete(id: number) {
    return this.http.delete(`/${this.ctrlUrl}/${id}`);
  }
}
