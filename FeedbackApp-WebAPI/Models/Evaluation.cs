using System.Collections.Generic;
using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class Evaluation
    {
        private int id;
        private string pin;
        private string turma;
        private string ies;
        private string curso;
        private List<Question> pergunta;
        private string prof_email;
        private List<string> nomesAlunos;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public string PIN { get => pin; set => pin = value; }
        public string Turma { get => turma; set => turma = value; }
        public string Ies { get => ies; set => ies = value; }
        public string Curso { get => curso; set => curso = value; }
        public List<Question> Perguntas { get => pergunta; set => pergunta = value; }
        public string Prof_Email { get => prof_email; set => prof_email = value; }
        public List<string> NomesAlunos { get => nomesAlunos; set => nomesAlunos = value; }

        public Evaluation(EvaluationDB evaluation)
        {
            Id = evaluation.Id;
            PIN = evaluation.PIN;
            Turma = evaluation.Turma;
            Ies = evaluation.Ies;
            Curso = evaluation.Curso;
            Prof_Email = evaluation.Prof_Email;
        }

        public Evaluation(int id, string pin, string turma, string ies, string curso, List<Question> pergunta, string prof_email, List<string> nomesAlunos)
        {
            Id = id;
            PIN = pin;
            Turma = turma;
            Ies = ies;
            Curso = curso;
            Perguntas = pergunta;
            Prof_Email = prof_email;
            NomesAlunos = nomesAlunos;
        }

        public Evaluation() : this(0, "", "", "", "", null, "", null)
        {
        }
    }
}