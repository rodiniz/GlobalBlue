using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalBlue.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public IBaseService<CustomerDto> _service { get; set; }

        public CustomerController(IBaseService<CustomerDto> baseService)
        {
            _service = baseService;
        }

        [Route("List")]
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> List()
        {
            var model = _service.GetAll();
            return Ok(model);
        }


        [HttpGet]
        [Route("Get")]
        public ActionResult<CustomerDto> Get(int id)
        {

            var ret= _service.Get(id);

            return ret == null ? NotFound() as ActionResult: Ok(ret);
        }
        [Route("update")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CustomerDto model)
        {
            try
            {
                await _service.Update(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok();
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDto model)
        {
            try
            {

                var created=await _service.Create(model);
                return Ok(created);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }



            
        }

        [HttpDelete]
        public async Task<IActionResult>  Delete(int id)
        {
            try
            {
                await _service.Delete(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


            return Ok();
        }
    }
}

