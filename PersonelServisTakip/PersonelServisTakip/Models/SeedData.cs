using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonelServisTakip.Models;
using PersonelServisTakip.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            // Eğer herhangi bir veri varsa seed işlemi yapılmasın
            if (context.Departments.Any() || context.Personels.Any() || context.Leaves.Any() || context.PerformanceReviews.Any() || context.PersonelTasks.Any() || context.ServiceVehicles.Any() || context.Trainings.Any())
            {
                return; // DB has been seeded
            }

            // Departmanları ekleyelim
            var departments = new List<Department>
            {
                new Department { Name = "IT", Description = "Information Technology" },
                new Department { Name = "HR", Description = "Human Resources" },
                new Department { Name = "Finance", Description = "Finance Department" }
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();

            // Servis araçlarını ekleyelim
            var serviceVehicles = new List<ServiceVehicle>
            {
                new ServiceVehicle { VehicleNumber = "34XYZ78", DriverName = "Ahmet Yılmaz", ServiceDate = DateTime.Now },
                new ServiceVehicle { VehicleNumber = "35ABC67", DriverName = "Mehmet Kaya", ServiceDate = DateTime.Now }
            };
            context.ServiceVehicles.AddRange(serviceVehicles);
            context.SaveChanges();

            // Personelleri ekleyelim
            var personels = new List<Personel>
            {
                new Personel { Name = "Ali Veli", DepartmentId = departments[0].Id, ServiceVehicleId = serviceVehicles[0].Id },
                new Personel { Name = "Ayşe Fatma", DepartmentId = departments[1].Id, ServiceVehicleId = serviceVehicles[1].Id }
            };
            context.Personels.AddRange(personels);
            context.SaveChanges();

            // İzinleri ekleyelim
            var leaves = new List<Leave>
            {
                new Leave { PersonelId = personels[0].Id, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now, Reason = "Vacation" },
                new Leave { PersonelId = personels[1].Id, StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now, Reason = "Sick Leave" }
            };
            context.Leaves.AddRange(leaves);
            context.SaveChanges();

            // Performans değerlendirmelerini ekleyelim
            var performanceReviews = new List<PerformanceReview>
            {
                new PerformanceReview { PersonelId = personels[0].Id, ReviewDate = DateTime.Now, Comments = "Excellent work", Rating = 5 },
                new PerformanceReview { PersonelId = personels[1].Id, ReviewDate = DateTime.Now, Comments = "Good job", Rating = 4 }
            };
            context.PerformanceReviews.AddRange(performanceReviews);
            context.SaveChanges();

            // Personel görevlerini ekleyelim
            var personelTasks = new List<PersonelTask>
            {
                new PersonelTask { PersonelId = personels[0].Id, Title = "Task 1", Description = "Complete report", AssignedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(7) },
                new PersonelTask { PersonelId = personels[1].Id, Title = "Task 2", Description = "Prepare presentation", AssignedDate = DateTime.Now, DueDate = DateTime.Now.AddDays(3) }
            };
            context.PersonelTasks.AddRange(personelTasks);
            context.SaveChanges();

            // Eğitimleri ekleyelim
            var trainings = new List<Training>
            {
                new Training { Title = "Safety Training", Description = "Safety procedures and guidelines", TrainingDate = DateTime.Now },
                new Training { Title = "Leadership Training", Description = "Leadership skills and management", TrainingDate = DateTime.Now }
            };
            context.Trainings.AddRange(trainings);
            context.SaveChanges();

            // Eğitimlere katılımcıları ekleyelim
            var training1 = context.Trainings.First(t => t.Title == "Safety Training");
            training1.Attendees.Add(personels[0]);
            var training2 = context.Trainings.First(t => t.Title == "Leadership Training");
            training2.Attendees.Add(personels[1]);
            context.SaveChanges();
        }
    }
}