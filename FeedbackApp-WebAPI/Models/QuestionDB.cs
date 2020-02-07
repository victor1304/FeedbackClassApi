using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class QuestionDB
    {
        private int id;
        private string pergunta;
        private int badCount;
        private int regularCount;
        private int goodCount;
        private int excellentCount;
        private string pin;
        private string nomeAluno;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public string Pergunta { get => pergunta; set => pergunta = value; }
        public string PIN { get => pin; set => pin = value; }
        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }

        public int BadCount { get => badCount; set => badCount = value; }
        public int RegularCount { get => regularCount; set => regularCount = value; }
        public int GoodCount { get => goodCount; set => goodCount = value; }
        public int ExcellentCount { get => excellentCount; set => excellentCount = value; }

        public QuestionDB(int id, string pergunta, string pin, string nomeAluno)
        {
            Id = id;
            Pergunta = pergunta;
            PIN = pin;
            NomeAluno = nomeAluno;
        }

        public QuestionDB(Question question)
        {
            Id = question.Id;
            Pergunta = question.Pergunta;
            PIN = question.PIN;
            NomeAluno = nomeAluno;

            BadCount = question.BadCount;
            RegularCount = question.RegularCount;
            GoodCount = question.GoodCount;
            ExcellentCount = question.ExcellentCount;
        }

        public QuestionDB() : this(0, "", "", "")
        {
        }
    }
}