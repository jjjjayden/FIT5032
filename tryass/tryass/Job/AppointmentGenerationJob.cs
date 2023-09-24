using Quartz;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System;
using tryass.Models;
using System.Linq;

public class AppointmentGenerationJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        // 这里是你生成预定时间的逻辑
        try
        {
            GenerateAppointmentsForAllHospitals();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately.
            // For now, I'm just writing it to the console.
            Console.WriteLine($"Error generating appointments: {ex.Message}");
        }
        return Task.CompletedTask;
    }

    public void GenerateAppointmentsForAllHospitals()
    {
        using (var dbContext = new ApplicationDbContext())
        {
            var hospitals = dbContext.Maps.ToList(); // Execute the query immediately
            foreach (var hospital in hospitals)
            {
                GenerateAppointmentsForHospital(hospital.Id, dbContext);
            }
        }
    }

    public void GenerateAppointmentsForHospital(int mapId, ApplicationDbContext dbContext)
    {
        DateTime today = DateTime.Today;

        for (int i = 0; i < 7; i++)
        {
            DateTime currentDay = today.AddDays(i);

            for (int j = 9; j <= 16; j++)
            {
                DateTime appointmentTime = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, j, 0, 0);
                CreateAppointment(mapId, appointmentTime, dbContext);

                if (j != 16)
                {
                    DateTime halfPastAppointmentTime = new DateTime(currentDay.Year, currentDay.Month, currentDay.Day, j, 30, 0);
                    CreateAppointment(mapId, halfPastAppointmentTime, dbContext);
                }
            }
        }
    }

    public void CreateAppointment(int mapId, DateTime dateTime, ApplicationDbContext dbContext)
    {
        Appointment appointment = new Appointment
        {
            MapId = mapId,
            DateTime = dateTime,
            IsBooked = false,
            UserId = null
        };

        dbContext.Appointments.Add(appointment);
        try
        {
            dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving appointment: {ex.Message}");
        }
    }

}

