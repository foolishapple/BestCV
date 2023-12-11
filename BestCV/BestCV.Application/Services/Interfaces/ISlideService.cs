using BestCV.Application.Models.Slides;
using BestCV.Application.Models.SystemConfigs;
using BestCV.Core.Entities;
using BestCV.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Services.Interfaces
{
    public interface ISlideService: IServiceQueryBase<int, InsertSlideDTO, UpdateSlideDTO, SlideDTO>
    {
        Task<bool> ChangeOrderSlide(ChangeSlideDTO model);
        Task<DionResponse> ListSlideShowonHomepage();
    }
}
