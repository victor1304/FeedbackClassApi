using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class FeedbackDB
    {
        private int id;
        private int questionId;
        private string feedback;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public int QuestionId { get => questionId; set => questionId = value; }
        public string Feedback { get => feedback; set => feedback = value; }

        public FeedbackDB(int questionId, string feedback)
        {
            QuestionId = questionId;
            Feedback = feedback;
        }

        public FeedbackDB() : this(0, "") { }
    }
}