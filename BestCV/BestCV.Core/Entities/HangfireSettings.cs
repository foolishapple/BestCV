using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class HangfireSettings
    {
        public string Route { get; set; }
        public string ServerName { get; set; }
        public Dashboard Dashboard { get; set; }
        public string ConnectionString { get; set; }
    }
    public class Dashboard
    {
        public string AppPath { get; set; }
        public int StatsPollingInterval { get; set; }
        public string DashboardTitle { get; set; }
    }
}
