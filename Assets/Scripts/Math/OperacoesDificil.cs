using System.Collections.Generic;

public static class OperacoesDificil
{
    public static List<Operacao> Criar()
    {
        return new List<Operacao>
        {
            new Operacao { numeroA = 11, numeroB = 7, operador1 = "+" }, // Resultado: 18
            new Operacao { numeroA = 14, numeroB = 9, operador1 = "+" }, // Resultado: 23
            new Operacao { numeroA = 18, numeroB = 6, operador1 = "+" }, // Resultado: 24
            new Operacao { numeroA = 22, numeroB = 8, operador1 = "+" }, // Resultado: 30
            new Operacao { numeroA = 25, numeroB = 11, operador1 = "+" }, // Resultado: 36
            new Operacao { numeroA = 30, numeroB = 14, operador1 = "+" }, // Resultado: 44
            new Operacao { numeroA = 17, numeroB = 5, operador1 = "-" }, // Resultado: 12
            new Operacao { numeroA = 21, numeroB = 9, operador1 = "-" }, // Resultado: 12
            new Operacao { numeroA = 26, numeroB = 7, operador1 = "-" }, // Resultado: 19
            new Operacao { numeroA = 32, numeroB = 15, operador1 = "-" }, // Resultado: 17
            new Operacao { numeroA = 40, numeroB = 18, operador1 = "-" }, // Resultado: 22
            new Operacao { numeroA = 45, numeroB = 19, operador1 = "-" }, // Resultado: 26
            new Operacao { numeroA = 6, numeroB = 7, operador1 = "*" }, // Resultado: 42
            new Operacao { numeroA = 8, numeroB = 6, operador1 = "*" }, // Resultado: 48
            new Operacao { numeroA = 9, numeroB = 7, operador1 = "*" }, // Resultado: 63
            new Operacao { numeroA = 11, numeroB = 8, operador1 = "*" }, // Resultado: 88
            new Operacao { numeroA = 12, numeroB = 7, operador1 = "*" }, // Resultado: 84
            new Operacao { numeroA = 9, numeroB = 3, operador1 = "/" }, // Resultado: 3
            new Operacao { numeroA = 16, numeroB = 4, operador1 = "/" }, // Resultado: 4
            new Operacao { numeroA = 21, numeroB = 7, operador1 = "/" }, // Resultado: 3
            new Operacao { numeroA = 32, numeroB = 8, operador1 = "/" }, // Resultado: 4
            new Operacao { numeroA = 45, numeroB = 9, operador1 = "/" }, // Resultado: 5
            new Operacao { numeroA = 12, numeroB = 5, numeroC = 3, operador1 = "+", operador2 = "+", usarTerceiroNumero = true }, // Resultado: 20
            new Operacao { numeroA = 20, numeroB = 6, numeroC = 4, operador1 = "-", operador2 = "+", usarTerceiroNumero = true }, // Resultado: 18
            new Operacao { numeroA = 18, numeroB = 7, numeroC = 5, operador1 = "+", operador2 = "-", usarTerceiroNumero = true }, // Resultado: 20
            new Operacao { numeroA = 25, numeroB = 8, numeroC = 6, operador1 = "-", operador2 = "-", usarTerceiroNumero = true }, // Resultado: 11
            new Operacao { numeroA = 30, numeroB = 9, numeroC = 7, operador1 = "+", operador2 = "+", usarTerceiroNumero = true }, // Resultado: 46
            new Operacao { numeroA = 28, numeroB = 10, numeroC = 8, operador1 = "-", operador2 = "+", usarTerceiroNumero = true }, // Resultado: 26
            new Operacao { numeroA = 24, numeroB = 6, numeroC = 9, operador1 = "+", operador2 = "-", usarTerceiroNumero = true }, // Resultado: 21
            new Operacao { numeroA = 35, numeroB = 12, numeroC = 10, operador1 = "-", operador2 = "-", usarTerceiroNumero = true }, // Resultado: 13
        };
    }
}
