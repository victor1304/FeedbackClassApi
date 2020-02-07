using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class EvaluationDB
    {
        private int id;
        private string pin;
        private string turma;
        private string ies;
        private string curso;
        private string prof_email;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public string PIN { get => pin; set => pin = value; }
        public string Turma { get => turma; set => turma = value; }
        public string Ies { get => ies; set => ies = value; }
        public string Curso { get => curso; set => curso = value; }
        public string Prof_Email { get => prof_email; set => prof_email = value; }

        public EvaluationDB(Evaluation evaluation)
        {
            Id = evaluation.Id;
            PIN = evaluation.PIN;
            Turma = evaluation.Turma;
            Ies = evaluation.Ies;
            Curso = evaluation.Curso;
            Prof_Email = evaluation.Prof_Email;
        }

        public EvaluationDB(int id, string pin, string turma, string ies, string curso, string prof_email)
        {
            Id = id;
            PIN = pin;
            Turma = turma;
            Ies = ies;
            Curso = curso;
            Prof_Email = prof_email;
        }

        public EvaluationDB() : this(0, "", "", "", "", "")
        {
        }
    }
}