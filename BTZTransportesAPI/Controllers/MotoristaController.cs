using BTZTransportesAPI.Models;
using BTZTransportesAPI.Repositories;
using BTZTransportesAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTZTransportesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotoristaController : ControllerBase
    {
        private readonly IMotoristaRepository _motoristaRepository;
        public MotoristaController(IMotoristaRepository motoristaRepository) => _motoristaRepository = motoristaRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Motorista>> GetMotoristas()
        {
            try
            {
                var motoristas = _motoristaRepository.GetMotoristas();
                return Ok(motoristas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Motorista> GetMotoristaById([FromRoute] int id)
        {
            try
            {
                var result = _motoristaRepository.GetMotoristasById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Motorista> RegisterMotorista([FromBody] Motorista motorista)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _motoristaRepository.RegisterMotorista(motorista);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public ActionResult<Motorista> EditMotorista([FromBody] Motorista motorista)
        {
            try
            {
                if (motorista.Id == 0)
                    ModelState.AddModelError("id", "id nulo");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _motoristaRepository.EditMotorista(motorista);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Motorista> DeleteMotorista([FromRoute] int id)
        {
            try
            {
                var result = _motoristaRepository.DeleteMotorista(id);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
