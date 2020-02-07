using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using FeedbackApp_WebAPI.Models;
using SQLite;

namespace FeedbackApp_WebAPI.DBAccess
{
    public class SQLiteFunctions
    {
        private static readonly string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sqliteDb.db3");
        private static SQLiteConnection Connection { get; set; }

        static SQLiteFunctions()
        {
            CreateTables();
        }

        private static void CreateTables()
        {
            if (!File.Exists(dbPath)) { var stream = File.Create(dbPath); stream.Dispose(); }

            Connection = new SQLiteConnection(dbPath);
            Connection.CreateTable<EvaluationDB>(CreateFlags.AutoIncPK);
            Connection.CreateTable<QuestionDB>(CreateFlags.AutoIncPK);
            Connection.CreateTable<FeedbackDB>(CreateFlags.AutoIncPK);
            Connection.CreateTable<NomesAlunosDB>(CreateFlags.AutoIncPK);
        }

        public static Evaluation SelectEvaluation(string pin)
        {
            var resultDb = Connection.Table<EvaluationDB>().ToList().Where(p => p.PIN == pin).FirstOrDefault();
            if (resultDb != null)
            {
                var result = new Evaluation(resultDb);
                result.Perguntas = GetQuestions(Connection.Table<QuestionDB>().ToList().Where(p => p.PIN == pin).ToList());
                return result;
            }
            else
            {
                throw new Exception("Nenhuma avaliação com o PIN especificado!");
            }
        }

        public static List<Evaluation> SelectHistoryEvaluations(string user_email)
        {
            var resultDb = Connection.Table<EvaluationDB>().Where(p => p.Prof_Email == user_email).ToList();
            if (resultDb != null)
            {
                var result = new List<Evaluation>();
                resultDb.ForEach(p => result.Add(new Evaluation(p)));
                result.ForEach(p => p.Perguntas = GetQuestions(Connection.Table<QuestionDB>().ToList().Where(q => q.PIN == p.PIN).ToList()));
                result.ForEach(p => p.Perguntas
                .ForEach(q => (q.Feedbacks = new List<string>())
                .AddRange(Connection.Table<FeedbackDB>().ToList()
                .Where(r => r.QuestionId == q.Id).Select(s => s.Feedback))));
                result.ForEach(p => (p.NomesAlunos = new List<string>())
                .AddRange(Connection.Table<NomesAlunosDB>().Where(q => q.PIN == p.PIN).Select(r => r.NomeAluno)));

                return result;
            }
            else
            {
                throw new Exception("Nenhuma avaliação!");
            }
        }

        public static int InsertEvaluation(Evaluation evaluation)
        {
            var evaluationDb = new EvaluationDB(evaluation);
            var questions = evaluation.Perguntas;
            questions.ForEach(p => p.PIN = evaluation.PIN);
            var questionsDB = GetQuestionsDB(questions);
            Connection.InsertAll(questionsDB);
            return Connection.Insert(evaluationDb);
        }

        public static int UpdateEvaluation(Evaluation evaluation)
        {
            var feedbacks = new List<FeedbackDB>();

            evaluation.Perguntas.ForEach(p => feedbacks.Add(new FeedbackDB(p.Id, p.Feedbacks.FirstOrDefault())));
            Connection.InsertAll(feedbacks);
            Connection.Insert(new NomesAlunosDB(evaluation.PIN, evaluation.NomesAlunos.FirstOrDefault()));

            evaluation.Perguntas.ForEach(p =>
            p.BadCount = Connection.Table<FeedbackDB>().Where(q => q.QuestionId == p.Id && q.Feedback == "Ruim").Count());
            evaluation.Perguntas.ForEach(p =>
            p.RegularCount = Connection.Table<FeedbackDB>().Where(q => q.QuestionId == p.Id && q.Feedback == "Regular").Count());
            evaluation.Perguntas.ForEach(p =>
            p.GoodCount = Connection.Table<FeedbackDB>().Where(q => q.QuestionId == p.Id && q.Feedback == "Bom").Count());
            evaluation.Perguntas.ForEach(p =>
            p.ExcellentCount = Connection.Table<FeedbackDB>().Where(q => q.QuestionId == p.Id && q.Feedback == "Excelente").Count());

            var questions = GetQuestionsDB(evaluation.Perguntas);
            return Connection.UpdateAll(questions);
        }

        public static bool DeleteAllHistory()
        {
            Connection.DeleteAll<EvaluationDB>();
            Connection.DeleteAll<QuestionDB>();
            Connection.DeleteAll<FeedbackDB>();
            Connection.DeleteAll<NomesAlunosDB>();

            return true;
        }

        public static List<QuestionDB> GetQuestionsDB(List<Question> questions)
        {
            List<QuestionDB> result = new List<QuestionDB>();
            questions.ForEach(p => result.Add(new QuestionDB(p)));
            return result;
        }

        public static List<Question> GetQuestions(List<QuestionDB> questions)
        {
            List<Question> result = new List<Question>();
            questions.ForEach(p => result.Add(new Question(p)));
            return result;
        }

        public static Dictionary<string, string> RecoverPassword(string email)
        {
            var result = new Dictionary<string, string>();
            var pin = GerarPIN();
            result.Add(email, pin);
            SendRecoveryEmail(email, pin);
            return result;
        }

        public static string GerarPIN()
        {
            Random rd = new Random();
            int pin = rd.Next(0, 999999);
            var result = pin.ToString().PadLeft(6, '0');
            return result;
        }

        public static void SendRecoveryEmail(string email, string pin)
        {
            MailMessage mail = new MailMessage { From = new MailAddress("salaabertauna@gmail.com", "App Sala Aberta") };
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Recuperação de senha - Sala Aberta";
            mail.Body = $"Olá, você solicitou a recuperação de senha para seu email. Insira este PIN na tela do aplicativo para prosseguir. PIN: {pin}";

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("salaabertauna@gmail.com", "al784512");
                client.EnableSsl = true;
                client.Send(mail);
            }
        }
    }
}