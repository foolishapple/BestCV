using Hangfire;
using BestCV.Application.Services.Interfaces;
using BestCV.Core.Services;
using BestCV.Core.Utilities;
using BestCV.Domain.Entities;
using BestCV.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Implement
{
    public class CronJobService : ICronJobService
    {
        private readonly ITopFeatureJobRepository _topFeatureJobRepository;
        private readonly ILogger _logger;
        private readonly IScheduledJobService _scheduledJobService;
        private readonly ITopAreaJobRepository _topAreaJobRepository;
        private readonly IRefreshJobRepository _refreshJobRepository;
        public CronJobService(ITopFeatureJobRepository topFeatureJobRepository, ILoggerFactory loggerFactory, IScheduledJobService scheduledJobService, ITopAreaJobRepository topAreaJobRepository, IRefreshJobRepository refreshJobRepository)
        {
            _topFeatureJobRepository = topFeatureJobRepository;
            _logger = loggerFactory.CreateLogger<CronJobService>();
            _scheduledJobService = scheduledJobService;
            _topAreaJobRepository = topAreaJobRepository;
            _refreshJobRepository = refreshJobRepository;
        }

        public async Task AddRefreshDaily()
        {
            try
            {
                var refreshJobs = await _refreshJobRepository.FindByConditionAsync(c => c.RefreshDate.Date == DateTime.Now.Date && c.Active);
                if(refreshJobs!=null && refreshJobs.Count > 0)
                {
                    foreach(var job in refreshJobs)
                    {
                        _scheduledJobService.Schedule(() => _refreshJobRepository.RefreshJob(job.JobId), job.RefreshDate);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add scheule refresh job");
                await Task.Delay(6000);
                await AddRefreshDaily();
            }
        }

        public void Delete(string key)
        {
            _scheduledJobService.Delete(key);
        }

        public void DeleteRecurring(string key)
        {
            _scheduledJobService.DeleteRecurringJob(key);
        }


        [Obsolete]
        public async Task<int> UpdateHighestPriorityPosition()
        {
            try
            {
                List<TopFeatureJob> topFeatureJobs = await _topFeatureJobRepository.FindByConditionAsync(c => c.Active);
                if (topFeatureJobs != null && topFeatureJobs.Count > 1)
                {
                    var firstJob = topFeatureJobs.MinBy(c => c.OrderSort);
                    var lastJob = topFeatureJobs.MaxBy(c => c.OrderSort);
                    int lastOrderSort = lastJob.OrderSort;
                    foreach (var j in topFeatureJobs)
                    {
                        if (j.Id != firstJob.Id)
                        {
                            j.OrderSort++;
                        }
                        else
                        {
                            j.OrderSort = lastOrderSort;
                        }
                    }
                    await _topFeatureJobRepository.UpdateListAsync(topFeatureJobs);
                    await _topFeatureJobRepository.SaveChangesAsync();
                }
                int minutes = CronUtil.CalculateTotalHighestPriorityPositionMinutes(topFeatureJobs == null || topFeatureJobs.Count == 0 ? 1 : topFeatureJobs.Count);
                _scheduledJobService.AddOrUpdate(CronUtil.Benefit.HIGHEST_PRIORITY_POSITION, () => UpdateHighestPriorityPosition(), Cron.MinuteInterval(minutes));
                return minutes;
            }
            catch (Exception e)
            {
                await Task.Delay(6000);
                return await UpdateHighestPriorityPosition();
            }
        }


        [Obsolete]
        public async Task UpdateTopArea()
        {
            try
            {
                List<TopAreaJob> topAreaJobs = await _topAreaJobRepository.FindByCondition(c => c.Active).OrderBy(c=>c.OrderSort).ToListAsync();
                if (topAreaJobs != null && topAreaJobs.Count > 0)
                {
                    if (topAreaJobs.Count <= CronUtil.Benefit.TOTAL_AREA_ON_TOP)
                    {
                        var random = new Random();
                        //Random order sort
                        int[] orderSort = topAreaJobs.Select(c => c.OrderSort).ToArray();
                        int n = orderSort.Length;
                        for (int i = n - 1; i > 0; i--)
                        {
                            int j = random.Next(0, i + 1);
                            // Swap list[i] and list[j]
                            int temp = orderSort[i];
                            orderSort[i] = orderSort[j];
                            orderSort[j] = temp;
                        }
                        int index = 0;
                        foreach(var item in topAreaJobs)
                        {
                            item.OrderSort = orderSort[index];
                            index++;
                        }
                    }
                    else if(topAreaJobs.Count < 2*CronUtil.Benefit.TOTAL_AREA_ON_TOP){
                        int totalOutTop = topAreaJobs.Count - CronUtil.Benefit.TOTAL_AREA_ON_TOP;
                        int lastOrderSort = topAreaJobs.Last().OrderSort;
                        foreach(var item in topAreaJobs)
                        {
                            item.OrderSort -= totalOutTop;
                        }
                        for(int i = totalOutTop - 1; i >= 0; i--)
                        {
                            topAreaJobs[i].OrderSort = lastOrderSort;
                            lastOrderSort--;
                        }
                    }
                    else
                    {
                        int lastOrderSort = topAreaJobs.Last().OrderSort;
                        foreach (var item in topAreaJobs)
                        {
                            item.OrderSort -= CronUtil.Benefit.TOTAL_AREA_ON_TOP;
                        }
                        for (int i = CronUtil.Benefit.TOTAL_AREA_ON_TOP - 1; i >= 0; i--)
                        {
                            topAreaJobs[i].OrderSort = lastOrderSort;
                            lastOrderSort--;
                        }
                    }
                    await _topAreaJobRepository.UpdateListAsync(topAreaJobs);
                    await _topAreaJobRepository.SaveChangesAsync();
                }
                int minutes = CronUtil.CalculateTotalTopAreaMinutes(topAreaJobs == null ? 0 : topAreaJobs.Count);
                _scheduledJobService.AddOrUpdate(CronUtil.Benefit.AREA_ON_TOP, () => UpdateTopArea(), Cron.MinuteInterval(minutes));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update top area");
                await Task.Delay(6000);
                await UpdateTopArea();
            }
        }
    }
}
