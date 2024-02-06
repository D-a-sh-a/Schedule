using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Schedule.Data;
using Schedule.Models;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Schedule.Entities
{
    public class Seeder
    {
        public static async Task SeedDataAsync(IServiceProvider services,
            IWebHostEnvironment env, IConfiguration config)
        {
            try
            {
                using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                    var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    string email;

                    string gr_name;
                    int gr_course;
                    DbUser user;
                    var result = managerRole.CreateAsync(new DbRole
                    {
                        Name = "Admin"
                    }).Result;

                    result = managerRole.CreateAsync(new DbRole
                    {
                        Name = "Student"
                    }).Result;

                    result = managerRole.CreateAsync(new DbRole
                    {
                        Name = "Lecturer"
                    }).Result;
                    email = "admin@gmail.com";
                    user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new DbUser
                        {
                            Email = email,
                            UserName = email,
                        };
                        result = await manager.CreateAsync(user, "@123Admin");
                        result = await manager.AddToRoleAsync(user, "Admin");
                        context.SaveChanges();
                    }
                    email = "student@gmail.com";
                    user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new DbUser
                        {
                            Email = email,
                            PIB = "Демянюк Дарія Тарасівна",
                            UserName = email,
                            id_group = 1,
                        };
                        result = await manager.CreateAsync(user, "@123Student");
                        result = await manager.AddToRoleAsync(user, "Student");
                        context.SaveChanges();
                    }

                    email = "lecturer@gmail.com";
                    user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new DbUser
                        {
                            Email = email,
                            PIB = "Волошин Володимир Степанович",
                            UserName = email,
                        };
                        result = await manager.CreateAsync(user, "@123Admin");
                        result = await manager.AddToRoleAsync(user, "Lecturer");
                        context.SaveChanges();
                    }

                    email = "lecturer1@gmail.com";
                    user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new DbUser
                        {
                            Email = email,
                            PIB = "Шевченко Ірина Мавіївна",
                            UserName = email,
                        };
                        result = manager.CreateAsync(user, "@123Admin").Result;
                        result = manager.AddToRoleAsync(user, "Lecturer").Result;
                        context.SaveChanges();
                    }
                    email = "lecturer2@gmail.com";
                    user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new DbUser
                        {
                            Email = email,
                            PIB = "Бабич Тетяна Юріївна",
                            UserName = email,
                        };
                        result = manager.CreateAsync(user, "@123Admin").Result;
                        result = manager.AddToRoleAsync(user, "Lecturer").Result;
                        context.SaveChanges();
                    }

                    string sj_name;
                    sj_name = "Веб-технології та веб-дизайн";
                    var Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 3
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Комп`ютерна графіка";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 3
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Програмування";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 4
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Методи обчислень";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 5
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Хмарні технології";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 5
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Технології тестування програмних продуктів";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 4
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }
                    sj_name = "Організація баз даних та знань";
                    Subject = await context.Subjects.FirstOrDefaultAsync(p => p.subject_name == sj_name);
                    if (Subject == null)
                    {
                        Subject = new Subject
                        {
                            subject_name = sj_name,
                            id_lecturer = 3
                        };
                        context.Subjects.Add(Subject);
                        context.SaveChanges();
                    }

                    var start = DateTime.Parse("8:00");
                    var Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("9:20")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("9:40");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("11:00")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("11:15");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("12:35")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("13:00");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("14:20")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("14:35");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("15:55")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("16:05");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("17:25")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }
                    start = DateTime.Parse("17:35");
                    Time = context.Timetable.FirstOrDefault(p => p.start_time == start);
                    if (Time == null)
                    {
                        Time = new Timetable
                        {
                            start_time = start,
                            end_time = DateTime.Parse("18:55")
                        };
                        context.Timetable.Add(Time);
                        context.SaveChanges();
                    }

                    gr_name = "ЦТ";
                    gr_course = 3;
                    var Group = await context.Groups.FirstOrDefaultAsync(p => p.group_name == gr_name && p.group_course == gr_course);
                    if (Group == null)
                    {
                        Group = new Group
                        {
                            group_name = gr_name,
                            group_course = gr_course,
                            group_specialty = "Професійна освіта. Цифрові технології",
                            group_specialty_id = "015.39"
                        };
                        context.Groups.AddAsync(Group);
                        context.SaveChanges();
                    }

                    string day_name = DayOfWeek.Monday.ToString();
                    int time = 2;
                    var day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 2,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    time = 3;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 7,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    day_name = DayOfWeek.Tuesday.ToString();
                    time = 1;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 3,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    time = 2;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 6,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    day_name = DayOfWeek.Wednesday.ToString();
                    time = 3;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 5,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    time = 2;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 5,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    day_name = DayOfWeek.Thursday.ToString();
                    time = 3;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 1,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    time = 4;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 1,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    day_name = DayOfWeek.Friday.ToString();
                    time = 1;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 3,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }
                    time = 2;
                    day_result = await context.ScheduleModels.FirstOrDefaultAsync(p => p.day == day_name && p.id_timetable == time);
                    if (day_result == null)
                    {
                        day_result = new ScheduleModel
                        {
                            day = day_name,
                            id_group = 1,
                            id_subject = 4,
                            id_timetable = time
                        };
                        context.ScheduleModels.AddAsync(day_result);
                        context.SaveChanges();
                    }

                    int student = 2;
                    int subj = 1;
                    var date = DateTime.Parse("01.02");
                    var mark_result = await context.Marks.FirstOrDefaultAsync(p => p.id_student == student && p.id_subject == subj && p.mark_date == date);
                    if (mark_result == null)
                    {
                        mark_result = new Mark
                        {
                            id_group = 1,
                            id_subject = subj,
                            id_student = student,
                            mark = 2,
                            mark_date = date,
                            mark_sort = "ПТ"
                        };
                        context.Marks.AddAsync(mark_result);
                        context.SaveChanges();
                    }
                    student = 2;
                    subj = 1;
                    date = DateTime.Parse("08.02");
                    mark_result = await context.Marks.FirstOrDefaultAsync(p => p.id_student == student && p.id_subject == subj && p.mark_date == date);
                    if (mark_result == null)
                    {
                        mark_result = new Mark
                        {
                            id_group = 1,
                            id_subject = subj,
                            id_student = student,
                            mark = 6,
                            mark_date = date,
                            mark_sort = "ПТ"
                        };
                        context.Marks.AddAsync(mark_result);
                        context.SaveChanges();
                    }
                    student = 2;
                    subj = 1;
                    date = DateTime.Parse("15.02");
                    mark_result = await context.Marks.FirstOrDefaultAsync(p => p.id_student == student && p.id_subject == subj && p.mark_date == date);
                    if (mark_result == null)
                    {
                        mark_result = new Mark
                        {
                            id_group = 1,
                            id_subject = subj,
                            id_student = student,
                            mark = 16.55,
                            mark_date = date,
                            mark_sort = "МК"
                        };
                        context.Marks.AddAsync(mark_result);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
