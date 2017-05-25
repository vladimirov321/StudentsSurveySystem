using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentsSurveySystem.Models;

namespace StudentsSurveySystem.Controllers
{
    public class StatsClass
    {
        public int A, B, C, D, E;
    }

    [Authorize]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        [HttpGet]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            var questions = db.Questions.Include(q => q.Survey).Where(s => s.SurveyID == id);

            ViewBag.SurveyName = survey.Name;
            ViewBag.SurveyID = survey.ID;

            string currentUserEmail = User.Identity.Name;
            Student currentStudent = db.Students.FirstOrDefault(x => x.Email == currentUserEmail);

            //Checks for Admin role
            bool currentUserRoleIsAdmin = User.IsInRole("Admin");
            if(currentUserRoleIsAdmin)
            {
                ViewBag.CanEnroll = true;
            }
            else
            {
                if (db.Enrollments.Any(x => x.SurveyID == id && x.StudentID == currentStudent.ID))
                {
                    ViewBag.CanEnroll = false;
                }
                else
                {
                    ViewBag.CanEnroll = true;
                }
            }
            
            return View(questions.ToList());
        }

        [HttpPost]
        public ActionResult Index(int surveyID)
        {
            string data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var pairs = data.Split('&');

            string currentUserEmail = User.Identity.Name;
            Student currentStudent = db.Students.FirstOrDefault(x => x.Email == currentUserEmail);

            foreach (var pair in pairs)
            {
                var secondSplit = pair.Split('=');
                if (secondSplit[0] == "surveyID")
                {
                    continue;
                }

                var answer = new Answer
                {
                    QuestionID = int.Parse(secondSplit[0]),
                    ChosenAnswer = (ChosenAnswer)Enum.Parse(typeof(ChosenAnswer), secondSplit[1]),
                    StudentAge = currentStudent.Age,
                    StudentGender = currentStudent.Gender,
                    StudentSpecialty = currentStudent.Specialty,
                    YearOfStudy = DateTime.Now.Year - (int.Parse((currentStudent.FNumber)) / 10000 + 2000)
                };
               
                db.Answers.Add(answer);
            }

            db.Enrollments.Add(new Enrollment
            {
                StudentID = currentStudent.ID,
                SurveyID = surveyID    
            });
            
            db.SaveChanges();

            return RedirectToAction("Index", "Home");                        
        }

        // GET: Question id frow view
        [Authorize(Roles = "Admin")]
        public ActionResult Stats(int questionID, int? age, Gender? gender, int? yearOfStudy, Specialty? specialty)
        {
            var question = db.Questions.FirstOrDefault(x => x.ID == questionID);

            StatsClass results = new StatsClass { A = 0, B = 0, C = 0, D = 0, E = 0 };

            foreach (var answer in question.Answers)
            {
                checkForResult(answer, results, age, gender, yearOfStudy, specialty);
            }
            
            ViewBag.Stats = results;
            ViewBag.QuestionID = questionID;
            return View();
        }


        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Content,SurveyID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Name", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Name", question.SurveyID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Content,SurveyID")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "ID", "Name", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Additional Methods
        public void answerSwitcher(Answer a, StatsClass results)
        {
            switch (a.ChosenAnswer)
            {
                case ChosenAnswer.A:
                    results.A++;
                    break;
                case ChosenAnswer.B:
                    results.B++;
                    break;
                case ChosenAnswer.C:
                    results.C++;
                    break;
                case ChosenAnswer.D:
                    results.D++;
                    break;
                case ChosenAnswer.E:
                    results.E++;
                    break;
            }
        }

        public void checkForResult(Answer answer, StatsClass results, int? age, Gender? gender, int? yearOfStudy, Specialty? specialty)
        {
            if(age.HasValue == false && gender.HasValue == false && yearOfStudy.HasValue == false && specialty.HasValue == false)
            {
                answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue == false && yearOfStudy.HasValue == false && specialty.HasValue == false)
            {
                if (answer.StudentAge == age) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue && yearOfStudy.HasValue == false && specialty.HasValue == false)
            {
                if (answer.StudentAge == age && answer.StudentGender == gender) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue && yearOfStudy.HasValue && specialty.HasValue == false)
            {
                if (answer.StudentAge == age && answer.StudentGender == gender && answer.YearOfStudy == yearOfStudy) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue && yearOfStudy.HasValue && specialty.HasValue)
            {
                if (answer.StudentAge == age && answer.StudentGender == gender && answer.YearOfStudy == yearOfStudy && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue == false && yearOfStudy.HasValue && specialty.HasValue == false)
            {
                if(answer.StudentAge == age && answer.YearOfStudy == yearOfStudy) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue == false && yearOfStudy.HasValue && specialty.HasValue)
            {
                if (answer.StudentAge == age && answer.YearOfStudy == yearOfStudy && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue && gender.HasValue == false && yearOfStudy.HasValue == false && specialty.HasValue)
            {
                if (answer.StudentAge == age  && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue && yearOfStudy.HasValue == false && specialty.HasValue == false)
            {
                if (answer.StudentGender == gender) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue && yearOfStudy.HasValue && specialty.HasValue == false)
            {
                if (answer.StudentGender == gender && answer.YearOfStudy == yearOfStudy) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue && yearOfStudy.HasValue && specialty.HasValue)
            {
                if (answer.StudentGender == gender && answer.YearOfStudy == yearOfStudy && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue && yearOfStudy.HasValue == false && specialty.HasValue)
            {
                if (answer.StudentGender == gender && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue == false && yearOfStudy.HasValue && specialty.HasValue == false)
            {
                if (answer.YearOfStudy == yearOfStudy) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue == false && yearOfStudy.HasValue && specialty.HasValue)
            {
                if (answer.YearOfStudy == yearOfStudy && answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
            if (age.HasValue == false && gender.HasValue == false && yearOfStudy.HasValue == false && specialty.HasValue)
            {
                if (answer.StudentSpecialty == specialty) answerSwitcher(answer, results);
            }
        }
    }
}
