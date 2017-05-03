using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCorrection.Library;
using TestCorrection.Model;
using TestCorrection.Model.Model;

namespace TestCorrection.Mappers
{
    public class DbInitializer : DropCreateDatabaseAlways<Entities>
    {
        protected override void Seed(Entities db)
        {
            System.Diagnostics.Debug.Print("Seeding db");

            Role role = db.Role.Add(new Role
            {
                Name = "administrator",
            });

            Crypto crypto = new Crypto();
            String md5Password = crypto.GetMd5Hash("admin");

            var ad = db.User.Add(new User
            {
                Name = "administrator",
                CPF = "07910096704",
                Email = "admin@admin.com.br",
                Password = md5Password,
                PasswordQuestion = "",
                RoleId = role.Id,
                Role = role,
                LastActivityDate = DateTime.Now,
                CreationDate = DateTime.Now
            });

            Teacher t = db.Teacher.Add(new Teacher
            {
                IdUser = ad.Id,
                IsTeacher = true
            });

            Grade g1 = db.Grade.Add(new Grade
            {
                Grade1 = 1
            });

            Grade g5 = db.Grade.Add(new Grade
            {
                Grade1 = 5
            });

            Grade g10 = db.Grade.Add(new Grade
            {
                Grade1 = 10
            });

            Question q1 = db.Question.Add(new Question
            {
                Type = "discursiva",
                Number = 1,
                Descripton = ""
            });

            Question q2 = db.Question.Add(new Question
            {
                Type = "discursiva",
                Number = 2,
                Descripton = ""
            });

            Question q3 = db.Question.Add(new Question
            {
                Type = "discursiva",
                Number = 3,
                Descripton = ""
            });

            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q1.Id,
                GradeId = g1.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q1.Id,
                GradeId = g5.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q1.Id,
                GradeId = g10.Id
            });

            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q2.Id,
                GradeId = g1.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q2.Id,
                GradeId = g5.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q2.Id,
                GradeId = g10.Id
            });

            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q3.Id,
                GradeId = g1.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q3.Id,
                GradeId = g5.Id
            });
            db.QuestionGrade.Add(new QuestionGrade
            {
                QuestionId = q3.Id,
                GradeId = g10.Id
            });

            db.SaveChanges();
        }
    }
}
