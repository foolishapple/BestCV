using FluentValidation;
using BestCV.Application.Models.CandidateProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Application.Validators.CandidateProjects
{
    public class CandidateProjectValidator : AbstractValidator<CandidateProjectsDTO>
    {
        public CandidateProjectValidator() 
        {
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Tên dự án không được để trống");
            RuleFor(x => x.ProjectName).MaximumLength(255).WithMessage("Tên dự án có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Customer).NotEmpty().WithMessage("Tên khách hàng không được để trống");
            RuleFor(x => x.Customer).MaximumLength(255).WithMessage("Tên khách hàng có độ dài tối đa 255 ký tự");

            RuleFor(x => x.TeamSize).NotEmpty().WithMessage("Số lượng thành viên không được để trống");


            RuleFor(x => x.Position).NotEmpty().WithMessage("Chức vụ không được để trống");
            RuleFor(x => x.Position).MaximumLength(255).WithMessage("Chức vụ có độ dài tối đa 255 ký tự");
           
            RuleFor(x => x.Responsibilities).NotEmpty().WithMessage("Trách nhiệm không được để trống");
            RuleFor(x => x.Responsibilities).MaximumLength(255).WithMessage("Trách nhiệm có độ dài tối đa 255 ký tự");


            RuleFor(x => x.TimePeriod).NotEmpty().WithMessage("Thời gian học không được để trống");
            RuleFor(x => x.TimePeriod).MaximumLength(255).WithMessage("Thời gian học có độ dài tối đa 255 ký tự");

            RuleFor(x => x.Info).MaximumLength(255).WithMessage("Mô tả có độ dài tối đa 500 ký tự");
        }
    }
}
