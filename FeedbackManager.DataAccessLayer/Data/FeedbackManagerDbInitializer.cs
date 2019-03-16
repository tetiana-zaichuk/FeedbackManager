using System;
using Microsoft.EntityFrameworkCore;
using FeedbackManager.DataAccessLayer.Entities;

namespace FeedbackManager.DataAccessLayer.Data
{
    public static class FeedbackManagerDbInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder, int amount = 10)
        {
            var surveys = new Survey[]
            {
                new Survey { Id = 1, CreatorName = "Jose Moreno", CreatedAt = DateTime.UtcNow, SurveyName = "Survey1", Description = "Some text 1" },
                new Survey { Id = 2, CreatorName = "John Bleach", CreatedAt = DateTime.UtcNow, SurveyName = "Survey2", Description = "Some text 2" },
                new Survey { Id = 3, CreatorName = "Anna Rodriguez", CreatedAt = DateTime.UtcNow, SurveyName = "Survey3", Description = "Some text 3" }
            };

            var questions = new Question[]
            {
                new Question{Id = 1, SurveyId = 1, QuestionName = "Question title 1", Answers =  "Answer 1;Answer 2;Answer 3", ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Question{Id = 2, SurveyId = 1, QuestionName = "Question title 2", Answers =  "Answer 1;Answer 2", ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."},
                new Question{Id = 3, SurveyId = 2, QuestionName = "Question title 1", Answers =  "Answer 1;Answer 2;Answer 3", ShortComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Lorem ipsum dolor sit amet, consectetur..."},
            };

            modelBuilder.Entity<Survey>().HasData(surveys);
            modelBuilder.Entity<Question>().HasData(questions);
        }
    }
}
