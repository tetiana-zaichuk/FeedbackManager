import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from '../shared/services/survey.service';
import { Survey } from '../shared/models/survey.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  survey: Survey;
  surveys: Survey[];
  createdAt: Date;
  creatorName: string;
  surveyName: string;
  description: string;
  tableMode: boolean = true;
  isSubmiting: Boolean = false;

  constructor(private surveyService: SurveyService, private router: Router) { }

  ngOnInit() {
    this.surveyService.getAll().subscribe((value: Survey[]) => this.surveys = value);
  }

  goToDetails(surveyId: number) {
    this.router.navigate(['/surveys-details', surveyId]);
  }

  cancel() {
    this.tableMode = true;
    this.creatorName = null;
    this.surveyName = null;
    this.description = null;
  }

  add() {
    this.tableMode = false;
  }

  validate() {

    return false;
  }

  onSubmit() {
    this.isSubmiting = true;
    if ((!this.creatorName || this.creatorName === ' ') && (!this.surveyName || this.surveyName === ' ')) {
      this.isSubmiting = false;
      return;
    }
    const newSurvey: Survey = {
      id: 0,
      createdAt: new Date(),
      creatorName: this.creatorName,
      surveyName: this.surveyName,
      description: this.description,
      questions: null
    };

    this.surveyService.create(newSurvey).
      subscribe(
        value => {
          this.isSubmiting = false;
          this.tableMode = true;
        },
        error => {
          this.isSubmiting = false;
        });
  
  }
}
