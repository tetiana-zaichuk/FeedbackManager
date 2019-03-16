import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SurveyComponent } from './survey/survey.component';
import { SurveyService } from './shared/services/survey.service';
import { QuestionService } from './shared/services/question.service';
import { SurveyQuestionsService } from './shared/services/survey-questions.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SurveyComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'surveys-details/:id', component: SurveyComponent },
    ])
  ],
  providers: [SurveyService, QuestionService, SurveyQuestionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
