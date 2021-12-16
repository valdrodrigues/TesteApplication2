﻿using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Domain.IApplication;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INSSController : ControllerBase
    {
        private readonly ICalculadorInss _calculadorInssApplication;

        public INSSController(ICalculadorInss calculadorInssApplication)
        {
            _calculadorInssApplication = calculadorInssApplication;
        }

        // Adicionado documentação completa só pra mostrar o payload
        /// <summary>
        /// Calcula o desconto do INSS sobre o salário
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="salario">Salario</param>
        /// <returns>Valor do desconto sobre o salário</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "data": 12/12/2011,
        ///        "salario": 2500.00
        ///     }
        ///
        /// </remarks>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("CalcularDescontoInss")]
        public IActionResult CalcularDescontoInss([FromQuery] DateTime data, decimal salario)
        {
            try
            {
                decimal retorno =  _calculadorInssApplication.CalcularDesconto(data, salario);

                return StatusCode((int)HttpStatusCode.OK, retorno);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
