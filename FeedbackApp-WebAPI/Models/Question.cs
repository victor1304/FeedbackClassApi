using System.Collections.Generic;
using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class Question
    {
        private int id;
        private string pergunta;
        private List<string> feedbacks;
        private int badCount;
        private int regularCount;
        private int goodCount;
        private int excellentCount;
        private string pin;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public string Pergunta { get => pergunta; set => pergunta = value; }
        public List<string> Feedbacks { get => feedbacks; set => feedbacks = value; }
        public string PIN { get => pin; set => pin = value; }

        public int BadCount { get => badCount; set => badCount = value; }
        public int RegularCount { get => regularCount; set => regularCount = value; }
        public int GoodCount { get => goodCount; set => goodCount = value; }
        public int ExcellentCount { get => excellentCount; set => excellentCount = value; }

        public Question(int id, string pergunta, List<string> feedbacks, string pin)
        {
            Id = id;
            Pergunta = pergunta;
            Feedbacks = feedbacks;
            PIN = pin;
        }

        public Question(QuestionDB question)
        {
            Id = question.Id;
            Pergunta = question.Pergunta;
            PIN = question.PIN;

            BadCount = question.BadCount;
            RegularCount = question.RegularCount;
            GoodCount = question.GoodCount;
            ExcellentCount = question.ExcellentCount;
        }

        public Question() : this(0, "", null, "")
        {
        }
    }
}