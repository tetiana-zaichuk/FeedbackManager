import { Component, OnInit, OnDestroy } from '@angular/core';
import { QuestionService } from '../shared/services/question.service';
import { SurveyQuestionsService } from '../shared/services/survey-questions.service';
import { Question } from '../shared/models/question.model';
import { Survey } from '../shared/models/survey.model';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../shared/services/survey.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit, OnDestroy {

  surveyId: number;
  survey: Survey;
  questions: Question[];
  tableMode: boolean = true;
  isSubmiting: Boolean = false;
  private subscription: Subscription;

  constructor(private questionService: QuestionService,
    private surveyQuestionsService: SurveyQuestionsService,
    private surveyService: SurveyService,
    private router: ActivatedRoute) {
  }

  ngOnInit() {
    this.subscription = this.router.params.subscribe(params => this.surveyId = params['id']);
    this.surveyService.get(this.surveyId).subscribe((value: Survey) => this.survey = value);
    this.surveyQuestionsService.getAllById(this.surveyId).subscribe((value: Question[]) => this.questions = value);

  }
  
  cancel() {
    this.tableMode = true;
  }

  add() {
    this.tableMode = false;
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}


