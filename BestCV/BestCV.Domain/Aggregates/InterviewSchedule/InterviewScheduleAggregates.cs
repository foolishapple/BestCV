using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Domain.Aggregates.InterviewSchedule
{
    public class InterviewScheduleAggregates
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int InterviewscheduleTypeId { get; set; }
        public string InterviewscheduleTypeName { get; set; }
        public int InterviewscheduleStatusId { get; set; }
        public string InterviewscheduleStatusName { get; set; }

        public string Color { get; set; }

        public long CandidateApplyJobId { get; set; }
        public string Link { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Search { get; set; }
    }
}
