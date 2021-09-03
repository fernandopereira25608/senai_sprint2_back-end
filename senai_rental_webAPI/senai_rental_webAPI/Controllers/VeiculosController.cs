using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_rental_webAPI.Domains;
using senai_rental_webAPI.Interfaces;
using senai_rental_webAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _VeiculoRepository { get; set; }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<VeiculoDomain> veiculosListados = _VeiculoRepository.ListarTodos();
            return Ok(veiculosListados);
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain novoVeiculo)
        {
            _VeiculoRepository.Inserir(novoVeiculo);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _VeiculoRepository.Deletar(id);
            return StatusCode(204);
        }


    }
}
