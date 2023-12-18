using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BestCV.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="A">type of entity</typeparam>
    /// <typeparam name="B">type of insert DTO</typeparam>
    /// <typeparam name="C">type of update DTO</typeparam>
    /// <typeparam name="D">type of view model</typeparam>
    /// <typeparam name="TKEY">type of entity primary key</typeparam>
    public interface IBaseController<A,B,C,D,TKEY>
    {
        Task<IActionResult> Add([FromBody] B obj);
        Task<IActionResult> AddMany([FromBody] IEnumerable<B> objs);
        Task<IActionResult> Update([FromBody] C obj);
        Task<IActionResult> UpdateMany([FromBody] IEnumerable<C> objs);
        Task<IActionResult> Delete([Required(ErrorMessage ="Id không được để trống.")]TKEY id);
        Task<IActionResult> Detail([Required(ErrorMessage = "Id không được để trống.")] TKEY id);
        Task<IActionResult> ListAll();
    }
}
