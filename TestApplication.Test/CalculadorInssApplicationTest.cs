using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TestApplication.Application;
using TestApplication.Domain.Dto;
using TestApplication.Domain.Entity;
using TestApplication.Domain.IRepository;

namespace TestApplication.Test
{
    public class CalculadorInssApplicationTest
    {
        private ITabelaDescontoService _tabelaDescontoService;
        private CalculadorSalarioDto calculadorSalarioDto;
        private TabelaDesconto tabelaDesconto;

        [SetUp]
        public void Setup()
        {
            calculadorSalarioDto = new CalculadorSalarioDto() { Data = DateTime.Now.AddYears(-10), Salario = (decimal)2000.00 };

            Mock<ITabelaDescontoService> mockITabelaDescontoService = new Mock<ITabelaDescontoService>();

            List<FaixaTabelaDesconto> faixaSalarialList = new List<FaixaTabelaDesconto>();
            faixaSalarialList.Add(new FaixaTabelaDesconto() { SalarioInicial = (decimal)1006.90, SalarioFinal = (decimal)1006.90, Aliquota = 8 });
            faixaSalarialList.Add(new FaixaTabelaDesconto() { SalarioInicial = (decimal)1006.91, SalarioFinal = (decimal)1844.83, Aliquota = 9 });
            faixaSalarialList.Add(new FaixaTabelaDesconto() { SalarioInicial = (decimal)1844.84, SalarioFinal = (decimal)3689.66, Aliquota = 11 });

            tabelaDesconto = new TabelaDesconto()
            {
                Ano = 2011,
                Teto = (decimal)405.86,
                FaixaSalarial = faixaSalarialList
            };

            mockITabelaDescontoService.Setup(x => x.BuscarTabelaPeloAno(2011)).Returns(tabelaDesconto);

            _tabelaDescontoService = mockITabelaDescontoService.Object;
        }

        [Test]
        public void CalcularDescontoTest()
        {
            var result = new CalculadorInss(_tabelaDescontoService).CalcularDesconto(calculadorSalarioDto.Data, calculadorSalarioDto.Salario);

            Assert.AreEqual((decimal)220.00, result);
        }
    }
}