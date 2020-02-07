using SQLite;

namespace FeedbackApp_WebAPI.Models
{
    public class NomesAlunosDB
    {
        private int id;
        private string pin;
        private string nomeAluno;

        [PrimaryKey]
        public int Id { get => id; set => id = value; }
        public string PIN { get => pin; set => pin = value; }
        public string NomeAluno { get => nomeAluno; set => nomeAluno = value; }

        public NomesAlunosDB(string pin, string nomeAluno)
        {
            PIN = pin;
            NomeAluno = nomeAluno;
        }

        public NomesAlunosDB() : this("", "")
        {
        }
    }
}
