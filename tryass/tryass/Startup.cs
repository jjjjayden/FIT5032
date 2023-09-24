using Microsoft.Owin;
using Owin;
using Quartz.Impl;
using Quartz;

[assembly: OwinStartupAttribute(typeof(tryass.Startup))]
namespace tryass
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ConfigureJobs();  
        }

        public void ConfigureJobs()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<AppointmentGenerationJob>()
                .WithIdentity("appointmentJob", "defaultGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("dailyTrigger", "defaultGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(24)  // every 24hours
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
