namespace BestCV.Core.Utilities
{

    public class CronUtil
    {
        public class Benefit
        {
            /// <summary>
            /// Thời gian tối đa để cập nhật ví vị trí công việc nổi bật
            /// </summary>
            public const int DEFAULT_REFRESH_MINUTES = 10;
            public const string HIGHEST_PRIORITY_POSITION = "HIGHEST_PRIORITY_POSITION";
            /// <summary>
            /// Số lượng tin tuyển dụng đứng top trong trang đầu tiên
            /// </summary>
            public const int TOTAL_AREA_ON_TOP = 15;
            /// <summary>
            /// Key of cron job area on top
            /// </summary>
            public const string AREA_ON_TOP = "AREA_ON_TOP";
        }
        public class Time
        {
            public static PeriodTime[] GOLDEN_TIMES = new PeriodTime[]
            {
                new PeriodTime()
                {
                    StartHour="06:30",
                    EndHour = "08:00"
                },
                new PeriodTime()
                {
                    StartHour="12:00",
                    EndHour = "14:00"
                },
                new PeriodTime()
                {
                    StartHour="20:00",
                    EndHour = "22:00"
                },
            };
        }

        public static int CalculateTotalHighestPriorityPositionMinutes(int totalJobFeature)
        {
            //1,440 is total minutes in a day
            //0.05 là sai số tối đa
            int total = (totalJobFeature == 0 ? 1 : totalJobFeature);
            int minutes = (1440*95) / (100*total);
            for(int i =1;i<= 36; i++)// i is total on top in day
            {
                int totalMinutes = minutes / i;
                if(totalMinutes>=5 && totalMinutes <= Benefit.DEFAULT_REFRESH_MINUTES)
                {
                    return totalMinutes;
                }
            }
            return Benefit.DEFAULT_REFRESH_MINUTES;
        }

        public static int CalculateTotalTopAreaMinutes(int totalTopArea)
        {
            //1,440 is total minutes in a day
            //0.05 là sai số tối đa
            if (totalTopArea <= 15)
            {
                return Benefit.DEFAULT_REFRESH_MINUTES;
            }
            double total = ((double)totalTopArea) /15;
            double minutes =(((double)1440) * 0.95) / total; 
            for (int i = 1; i <= 36; i++)// i is total on top in day
            {
                double totalMinutes = minutes / i;
                if (totalMinutes >= 5 && totalMinutes <= Benefit.DEFAULT_REFRESH_MINUTES)
                {
                    return (int)totalMinutes;
                }
            }
            return Benefit.DEFAULT_REFRESH_MINUTES;
        }
    }
    public class PeriodTime
    {
        public string StartHour { get; set; } = null!;
        public string EndHour { get; set; } = null!;
    }
}


