using GlobalBlue.Dtos;
using GlobalBlue.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
            return Ok(_service.Get(id));
        }
        [Route("update")]
        [HttpPut]
        public ActionResult Put([FromBody] CustomerDto model)
        {
            try
            {
                _service.Update(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

            return Ok();
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDto model)
        {
            try
            {

                _service.Create(model);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }



            
        }

        [HttpDelete]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


            return Ok();
        }
    }
}

