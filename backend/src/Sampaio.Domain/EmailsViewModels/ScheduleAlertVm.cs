using System;
using Sampaio.Shared.Enums;

namespace Sampaio.Domain.EmailsViewModels
{
    public class ScheduleAlertVm
    {
        public ScheduleAlertVm(ESchedulingType type, string description, string obs, DateTime date, TimeSpan time, string name, string email)
        {
            Type = type;
            Description = description;
            Obs = obs;
            Date = date;
            Time = time;
            Name = name;
            Email = email;
        }

        public ESchedulingType Type { get; set; }

        public string Description { get; set; }

        public string Obs { get; set; }
        
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }
    }
}