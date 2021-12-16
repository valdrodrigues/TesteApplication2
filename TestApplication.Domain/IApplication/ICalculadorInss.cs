using System;

namespace TestApplication.Domain.IApplication
{
    public interface ICalculadorInss
    {
        decimal CalcularDesconto(DateTime data, decimal salario);
    }
}