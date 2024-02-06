using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Schedule.Data;
using Schedule.Entities;
using Schedule.Enums;
using Schedule.Models;
using Schedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;

namespace Schedule.Controllers
{
    public class StudyingProcessController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<DbUser> userManager;

        public StudyingProcessController(ApplicationDbContext dbContext, UserManager<DbUser> userManager)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public IActionResult GetMarks()
        {
            dbContext.SaveChanges();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditMarks([FromQuery] string subjectId = "", [FromQuery] string groupId = "0")
        {
            int intgroupid = 0, intsubjectId = 0;
            try
            {
                intgroupid = int.Parse(groupId);
                intsubjectId = int.Parse(subjectId);
            }
            catch (Exception ex)
            {

            }
            int userId = 0;
            try
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
            catch { }

            var viewModel = new EditMarkViewModel();
            var students = new List<DbUser>();

            if (User.IsInRole("Admin") || User.IsInRole("Lecturer"))
            {
                viewModel.Groups = await dbContext.Groups.Select(group => new ViewModelsDictionary
                {
                    id = group.id_group,
                    value = group.group_name
                }).ToListAsync();

                students = await dbContext.Users.Where(p => p.id_group == intgroupid).ToListAsync();
                if (students == null || !students.Any())
                    return View("Error");

                viewModel.Subjects = await dbContext.Subjects.Where(p => p.id_lecturer == userId).Select(group => new ViewModelsDictionary
                {
                    id = group.id_subject,
                    value = group.subject_name
                }).ToListAsync();
            }

            if (User.IsInRole("Student"))
            {
                var user = await dbContext.Users.FindAsync(userId);
                if (user is not null)
                    intgroupid = user.id_group;
                else
                    return View("Error");

                var shedules = await dbContext.ScheduleModels.Where(p => p.id_group == intgroupid).ToListAsync();
                viewModel.Subjects = new List<ViewModelsDictionary>();
                foreach (var shedule in shedules)
                {
                    var subject = await dbContext.Subjects.FindAsync(shedule.id_subject);
                    if (subject is not null)
                        if (!viewModel.Subjects.Any(p => p.id == subject.id_subject))
                            viewModel.Subjects.Add(new ViewModelsDictionary
                            {
                                id = subject.id_subject,
                                value = subject.subject_name
                            });
                }

                students.Add(user);
            }
            viewModel.groupId = intgroupid;

            ViewBag.role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (intgroupid is not 0 && intsubjectId is not 0)
            {
                viewModel.Rows = new List<MarksRow>();
                viewModel.subjectId = intsubjectId;

                var marks = await dbContext.Marks.Where(p => p.id_subject == intsubjectId).ToListAsync();
                var ScheduleModels = await dbContext.ScheduleModels.Where(p => p.id_group == intgroupid && p.id_subject == intsubjectId).ToListAsync();


                var dateList = new List<DateTime>();
                DateTime startDate = new DateTime(DateTime.Now.Year, 1, 29);
                DateTime endDate = new DateTime(DateTime.Now.Year, 5, 31);

                if (ScheduleModels != null)
                {
                    if (ScheduleModels.Count == 1)
                    {
                        var counter = 0;
                        while (counter < 1000)
                        {
                            if (startDate.DayOfWeek.ToString() == ScheduleModels.First().day)
                            {
                                break;
                            }
                            startDate = startDate.AddDays(1);
                        }

                        while (startDate <= endDate)
                        {
                            dateList.Add(startDate);
                            startDate = startDate.AddDays(7);
                        }
                    }
                    else
                    {
                        while (startDate <= endDate)
                        {
                            foreach (var model in ScheduleModels)
                            {
                                var counter = 0;
                                while (counter < 1000)
                                {
                                    if (startDate.DayOfWeek.ToString() == model.day)
                                    {
                                        dateList.Add(startDate);
                                        startDate = startDate.AddDays(1);
                                        break;
                                    }
                                    startDate = startDate.AddDays(1);
                                }
                            }
                        }
                    }
                }

                viewModel.RowNames = new List<string>();
                foreach (var date in dateList)
                {
                    viewModel.RowNames.Add(date.ToShortDateString());
                }

                foreach (var student in students)
                {
                    var row = new MarksRow();
                    row.StudentName = student.PIB;
                    row.StudentMarks = new List<string>();
                    foreach (var date in dateList)
                    {
                        var mark = marks.Where(p => p.mark_date == date && p.id_student == student.Id).FirstOrDefault();

                        var str = mark != null ? mark.mark.ToString() : "";

                        row.StudentMarks.Add(str);
                    }
                    viewModel.Rows.Add(row);
                }


            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditMarks(List<string> dates, List<string> names, List<string> marks, int groupId, int subjectId)
        {
            for (int i = 0; i < dates.Count; i++)
            {
                var student = await dbContext.Users.SingleOrDefaultAsync(p => p.PIB == names[i]);
                Mark mark = null;
                try
                {
                    if (student is not null)
                    {
                        var myMarks = await dbContext.Marks.Where(p => p.id_group == groupId && p.id_subject == subjectId && p.id_student == student.Id).ToListAsync();
                        mark = myMarks.FirstOrDefault(p => p.mark_date.ToShortDateString() == dates[i]);
                    }
                    if (mark is not null)
                        mark.mark = double.Parse(marks[i]);
                    else
                        await dbContext.Marks.AddAsync(new Mark { id_group = groupId, id_subject = subjectId, id_student = student.Id, mark = double.Parse(marks[i]), mark_date = DateTime.ParseExact(dates[i], "dd.MM.yyyy", null) });
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }
            }
            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> CreateSubject()
        {
            var model = new SubjectViewModel();

            var users = await userManager.GetUsersInRoleAsync("Student");

            model.Lecturers = users.Select(entity => new ViewModelsDictionary
            {
                id = entity.Id,
                value = $"{entity.PIB}"
            })
            .ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectViewModel Model)
        {
            var prewSubject = await dbContext.Subjects.FirstOrDefaultAsync(p => p.subject_name == Model.subject_name);

            if (prewSubject is not null)
            {
                ViewBag.Error = $"Група з таким ім'ям вже існує.";
                return View("Error");
            }

            await dbContext.Subjects.AddAsync(new Models.Subject { id_lecturer = Model.id_lecturer, id_subject = prewSubject.id_subject, subject_name = Model.subject_name });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditSubject(int id)
        {
            var prewSubject = await dbContext.Subjects.SingleOrDefaultAsync(p => p.id_subject == id);

            if (prewSubject is not null)
            {
                var model = new SubjectViewModel
                {
                    id_subject = prewSubject.id_subject,
                    id_lecturer = prewSubject.id_lecturer,
                    subject_name = prewSubject.subject_name
                };

                var users = await userManager.GetUsersInRoleAsync("Student");

                model.Lecturers = users.Select(entity => new ViewModelsDictionary
                {
                    id = entity.Id,
                    value = $"{entity.PIB}"
                })
                .ToList();
                return View(model);
            }
            return NotFound("Такої дисципліни не знайдено");
        }

        [HttpPost]
        public async Task<IActionResult> EditSubject(SubjectViewModel Model)
        {
            var prewSubject = await dbContext.Subjects.FirstOrDefaultAsync(p => p.subject_name == Model.subject_name);

            if (prewSubject is not null)
            {
                ViewBag.Error = $"Група з таким ім'ям вже існує.";
                return View("Error");
            }

            dbContext.Subjects.Update(new Models.Subject
            {
                id_lecturer = Model.id_lecturer,
                id_subject = prewSubject.id_subject,
                subject_name = Model.subject_name
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task DeleteSubject(int id)
        {

            var prewSubject = await dbContext.Subjects.FindAsync(id);
            if (prewSubject is not null)
            {
                dbContext.Subjects.Remove(prewSubject);
            }
        }


        public async Task<IActionResult> Regimen()
        {
            var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
                return View("Error");

            var user = await dbContext.Users.FindAsync(int.Parse(userid));

            if (user == null)
                return View("Error");

            var viewmodel = new RegimenViewModel();
            viewmodel.timetable = new List<string>();

            var timetable = await dbContext.Timetable.ToListAsync();

            foreach (var t in timetable)
            {
                viewmodel.timetable.Add($"{t.start_time.ToShortTimeString()} \n {t.end_time.ToShortTimeString()}");
            }
            viewmodel.days = new List<SheduleDayViewModel>();


            var userSheduleSubjects = new List<ScheduleModel>();

            if (User.IsInRole("Student"))
            {
                userSheduleSubjects = await dbContext.ScheduleModels.Where(p => p.id_group == user.id_group).ToListAsync();
            }

            if (User.IsInRole("Admin") || User.IsInRole("Lecturer"))
            {

                var subjects = await dbContext.Subjects.Where(p => p.id_lecturer == user.Id).ToListAsync();

                foreach (var subject in subjects)
                {
                    userSheduleSubjects.AddRange(await dbContext.ScheduleModels.Where(p => p.id_subject == subject.id_subject).ToListAsync());
                }
            }

            foreach (var sheduleSubject in userSheduleSubjects)
            {
                SheduleDayViewModel viewmodelDay = null;
                bool isDayCreated = false;

                foreach (var day in viewmodel.days)
                {
                    if (day.day == sheduleSubject.day)
                    {
                        viewmodelDay = day;
                        isDayCreated = true;
                        break;
                    }
                }

                if (viewmodelDay is null)
                {
                    viewmodelDay = new SheduleDayViewModel();
                    viewmodelDay.day = sheduleSubject.day;
                }

                var subject = await dbContext.Subjects.FindAsync(sheduleSubject.id_subject);
                if (subject is not null)
                {
                    if (viewmodelDay.subjectNames is null)
                        viewmodelDay.subjectNames = new List<ViewModelsDictionary>();

                    int subjectTimeId = 0;
                    var subjectTime = await dbContext.Timetable.FindAsync(sheduleSubject.id_timetable);
                    if (subjectTime is not null)
                    {
                        subjectTimeId = viewmodel.timetable.IndexOf($"{subjectTime.start_time.ToShortTimeString()} \n {subjectTime.end_time.ToShortTimeString()}");
                    }

                    viewmodelDay.subjectNames.Add(new ViewModelsDictionary { value = subject.subject_name, id = subjectTimeId });

                    var lecturer = await dbContext.Users.FindAsync(subject.id_lecturer);
                    if (lecturer is not null)
                    {
                        if (viewmodelDay.subjectLecturerNames is null)
                            viewmodelDay.subjectLecturerNames = new List<ViewModelsDictionary>();
                        viewmodelDay.subjectLecturerNames.Add(new ViewModelsDictionary { value = lecturer.PIB, id = subjectTimeId });
                    }
                }

                if (!isDayCreated)
                    viewmodel.days.Add(viewmodelDay);
            }
            viewmodel.SortDays();
            ViewBag.role = User.FindFirst(ClaimTypes.Role)?.Value;

            return View(viewmodel);
        }
    }
}
