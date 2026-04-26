using System.Collections.Generic;

public static class OperacoesMedio
{
    public static List<Operacao> Criar()
    {
        return new List<Operacao>
        {
            new Operacao { numeroA = 6, numeroB = 5, operador1 = "+" }, // Resultado: 11
            new Operacao { numeroA = 7, numeroB = 4, operador1 = "+" }, // Resultado: 11
            new Operacao { numeroA = 8, numeroB = 6, operador1 = "+" }, // Resultado: 14
            new Operacao { numeroA = 9, numeroB = 3, operador1 = "+" }, // Resultado: 12
            new Operacao { numeroA = 10, numeroB = 7, operador1 = "+" }, // Resultado: 17
            new Operacao { numeroA = 12, numeroB = 5, operador1 = "+" }, // Resultado: 17
            new Operacao { numeroA = 14, numeroB = 8, operador1 = "+" }, // Resultado: 22
            new Operacao { numeroA = 15, numeroB = 6, operador1 = "+" }, // Resultado: 21
            new Operacao { numeroA = 18, numeroB = 4, operador1 = "+" }, // Resultado: 22
            new Operacao { numeroA = 20, numeroB = 9, operador1 = "+" }, // Resultado: 29
            new Operacao { numeroA = 9, numeroB = 4, operador1 = "-" }, // Resultado: 5
            new Operacao { numeroA = 11, numeroB = 5, operador1 = "-" }, // Resultado: 6
            new Operacao { numeroA = 13, numeroB = 6, operador1 = "-" }, // Resultado: 7
            new Operacao { numeroA = 15, numeroB = 7, operador1 = "-" }, // Resultado: 8
            new Operacao { numeroA = 18, numeroB = 9, operador1 = "-" }, // Resultado: 9
            new Operacao { numeroA = 20, numeroB = 8, operador1 = "-" }, // Resultado: 12
            new Operacao { numeroA = 24, numeroB = 11, operador1 = "-" }, // Resultado: 13
            new Operacao { numeroA = 27, numeroB = 13, operador1 = "-" }, // Resultado: 14
            new Operacao { numeroA = 30, numeroB = 12, operador1 = "-" }, // Resultado: 18
            new Operacao { numeroA = 35, numeroB = 14, operador1 = "-" }, // Resultado: 21
            new Operacao { numeroA = 3, numeroB = 4, operador1 = "*" }, // Resultado: 12
            new Operacao { numeroA = 4, numeroB = 5, operador1 = "*" }, // Resultado: 20
            new Operacao { numeroA = 6, numeroB = 3, operador1 = "*" }, // Resultado: 18
            new Operacao { numeroA = 7, numeroB = 4, operador1 = "*" }, // Resultado: 28
            new Operacao { numeroA = 8, numeroB = 5, operador1 = "*" }, // Resultado: 40
            new Operacao { numeroA = 12, numeroB = 3, operador1 = "/" }, // Resultado: 4
            new Operacao { numeroA = 16, numeroB = 4, operador1 = "/" }, // Resultado: 4
            new Operacao { numeroA = 18, numeroB = 6, operador1 = "/" }, // Resultado: 3
            new Operacao { numeroA = 24, numeroB = 8, operador1 = "/" }, // Resultado: 3
            new Operacao { numeroA = 36, numeroB = 9, operador1 = "/" }, // Resultado: 4
        };
    }
}
