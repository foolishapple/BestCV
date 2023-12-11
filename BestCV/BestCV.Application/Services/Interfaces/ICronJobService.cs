using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ICronJobService
    {
        /// <summary>
        /// Author: TUNGTD
        /// Created: 24/09/2023
        /// Description: Auto Update Highest Priority Position
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateHighestPriorityPosition();
        void Delete(string key);
        void DeleteRecurring(string key);
        /// <summary>
        /// Author: TUNGTD
        /// Created: 27/09/2023
        /// Description: Auto update top area job
        /// </summary>
        Task UpdateTopArea();
        /// <summary>
        /// Author: TUNGTD
        /// Created: 02/10/2023
        /// Description: Add refresh job daily
        /// </summary>
        /// <returns></returns>
        Task AddRefreshDaily();
    }
}
