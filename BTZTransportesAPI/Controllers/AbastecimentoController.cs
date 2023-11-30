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
    public class AbastecimentoController : ControllerBase
    {
        private readonly IAbastecimentoRepository _abastecimentoRepository;
        public AbastecimentoController(IAbastecimentoRepository abastecimentoRepository) => _abastecimentoRepository = abastecimentoRepository;

        [HttpPost]
        public ActionResult<Abastecimento> RegisterAbastecimento([FromBody] Abastecimento abastecimento)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = _abastecimentoRepository.RegisterAbastecimento(abastecimento);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
