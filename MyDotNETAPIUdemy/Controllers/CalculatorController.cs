using Microsoft.AspNetCore.Mvc;

namespace MyDotNETAPIUdemy.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber)
    {
        if (IsNumric(firstNumber) && IsNumric(secondNumber))
        {
            var calcular = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }


    [HttpGet("subtraction/{firstNumber}/{secondNumber}")]
    public IActionResult Subtraction(string firstNumber, string secondNumber)
    {
        if (IsNumric(firstNumber) && IsNumric(secondNumber))
        {
            var calcular = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }

    [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
    public IActionResult Multiplication(string firstNumber, string secondNumber)
    {
        if (IsNumric(firstNumber) && IsNumric(secondNumber))
        {
            var calcular = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }


    [HttpGet("division/{firstNumber}/{secondNumber}")]
    public IActionResult Division(string firstNumber, string secondNumber)
    {
        if (IsNumric(firstNumber) && IsNumric(secondNumber))
        {
            var calcular = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }


    [HttpGet("mean/{firstNumber}/{secondNumber}")]
    public IActionResult Mean(string firstNumber, string secondNumber)
    {
        if (IsNumric(firstNumber) && IsNumric(secondNumber))
        {
            var calcular = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }


    [HttpGet("squareRoot/{firstNumber}")]
    public IActionResult SquareRoot(string firstNumber)
    {
        if (IsNumric(firstNumber))
        {
            //Calculo de Raiz quadrada de um valor inserido, usando a fun��o do c# Math.Sqrt()
            var calcular = Math.Sqrt((double)ConvertToDecimal(firstNumber));
            return Ok(calcular.ToString());
        }
        return BadRequest("Valor introduzido inv�lido");
    }




    #region METODOS DE COVERS�O
    private bool IsNumric(string strNumber)
    {
        double number;
        bool IsNumber = double.TryParse(strNumber, System.Globalization.NumberStyles.Any,
            System.Globalization.NumberFormatInfo.InvariantInfo, out number);
        return IsNumber;
    }
    private decimal ConvertToDecimal(string strNumber)
    {
        decimal decimalValue;
        if (decimal.TryParse(strNumber, out decimalValue))
        {
            return decimalValue;
        }
        return 0;
    }
    #endregion
}
