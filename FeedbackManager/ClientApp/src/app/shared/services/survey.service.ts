import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Survey } from '../models/survey.model';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SurveyService {
  private readonly ctrlUrl = 'survey';

  constructor(private http: HttpClient) {
  }

  getAll(): Observable<Survey[]> {
    return this.http.get<Survey[]>(`/${this.ctrlUrl}`);
  }

  get(id: number): Observable<Survey> {
    return this.http.get<Survey>(`/${this.ctrlUrl}/${id}`);
  }

  create(survey: Survey) {
    return this.http.post(`/${this.ctrlUrl}`, survey);
  }

  update(id: number, survey: Survey) {
    return this.http.put(`/${this.ctrlUrl}/${id}`, survey);
  }

  delete(id: number) {
    return this.http.delete(`/${this.ctrlUrl}/${id}`);
  }
}
