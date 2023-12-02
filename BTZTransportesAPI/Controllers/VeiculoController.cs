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
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _veiculoRepository;
        public VeiculoController(IVeiculoRepository veiculoRepository) => _veiculoRepository = veiculoRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Motorista>> GetVeiculos()
        {
            try
            {
                var result = _veiculoRepository.GetVeiculos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Veiculo> GetVeiculoById([FromRoute] int id)
        {
            try 
            {
                var result = _veiculoRepository.GetVeiculoById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Veiculo> RegisterVeiculo([FromBody] Veiculo veiculo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _veiculoRepository.RegisterVeiculo(veiculo);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
