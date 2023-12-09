using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestCV.Core.Entities
{
    public class BestCVResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object Resources { get; set; }
        public object Errors { get; set; }
        public bool IsSucceeded { get; set; }

        private BestCVResponse()
        {
        }

        public static BestCVResponse Success(string? message = "Success")
        {
            return new BestCVResponse()
            {
                Status = 200,
                Message = message,
                IsSucceeded = true,
            };
        }

        public static BestCVResponse Success(object data, string? message = "Success")
        {
            return new BestCVResponse()
            {
                Status = 200,
                Message = message,
                Resources = data,
                IsSucceeded = true
            };
        }

        public static BestCVResponse Success<T>(T? data, string? message = "Success") where T : class
        {
            return new BestCVResponse()
            {
                Status = 200,
                Message = message,
                IsSucceeded = true,
                Resources = data
            };
        }


        public static BestCVResponse Success<T>(T data, int total, int totalFiltered, int currentPage, int pageSize) where T : class
        {
            return new BestCVResponse()
            {
                Status = 200,
                Message = "Success",
                Resources = new PagingData<T>()
                {
                    DataSource = data,
                    Total = total,
                    TotalFiltered = totalFiltered,
                    CurrentPage = currentPage,
                    PageSize = pageSize,

                }, IsSucceeded = true
            };
        }

        public static BestCVResponse Created<T>(T data)
        {
            return new BestCVResponse()
            {
                Status = 201,
                Message = "Created",
                IsSucceeded = true,
                Resources = data
            };
        }

        public static BestCVResponse Error(int status, object errors, string? message = null)
        {
            return new BestCVResponse()
            {
                Status = status,
                Message = message,
                Errors = errors,
                IsSucceeded = false
            };
        }

        public static BestCVResponse Error(string? message = "Error")
        {
            return new BestCVResponse()
            {
                Status = 400,
                Message = message,
                IsSucceeded = false
            };
        }

        public static BestCVResponse NotFound(string Message, object errors)
        {
            return Error(404, errors, Message);
        }

        public static BestCVResponse BadRequest(object errors)
        {
            return Error(400, errors, "BadRequest");
        }


        public static BestCVResponse Unauthorized(object errors)
        {
            return Error(401, errors, "Unauthorized");
        }

        public static BestCVResponse Forbidden(object errors)
        {
            return Error(403, errors, "Forbidden");
        }
    }
}
