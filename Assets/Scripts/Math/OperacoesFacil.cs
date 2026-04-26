using System.Collections.Generic;

public static class OperacoesFacil
{
    public static List<Operacao> Criar()
    {
        return new List<Operacao>
        {
            new Operacao { numeroA = 1, numeroB = 1, operador1 = "+" }, // Resultado: 2
            new Operacao { numeroA = 2, numeroB = 1, operador1 = "+" }, // Resultado: 3
            new Operacao { numeroA = 2, numeroB = 2, operador1 = "+" }, // Resultado: 4
            new Operacao { numeroA = 3, numeroB = 1, operador1 = "+" }, // Resultado: 4
            new Operacao { numeroA = 3, numeroB = 2, operador1 = "+" }, // Resultado: 5
            new Operacao { numeroA = 4, numeroB = 1, operador1 = "+" }, // Resultado: 5
            new Operacao { numeroA = 4, numeroB = 2, operador1 = "+" }, // Resultado: 6
            new Operacao { numeroA = 4, numeroB = 3, operador1 = "+" }, // Resultado: 7
            new Operacao { numeroA = 5, numeroB = 1, operador1 = "+" }, // Resultado: 6
            new Operacao { numeroA = 5, numeroB = 2, operador1 = "+" }, // Resultado: 7
            new Operacao { numeroA = 5, numeroB = 3, operador1 = "+" }, // Resultado: 8
            new Operacao { numeroA = 6, numeroB = 1, operador1 = "+" }, // Resultado: 7
            new Operacao { numeroA = 6, numeroB = 2, operador1 = "+" }, // Resultado: 8
            new Operacao { numeroA = 7, numeroB = 1, operador1 = "+" }, // Resultado: 8
            new Operacao { numeroA = 7, numeroB = 2, operador1 = "+" }, // Resultado: 9
            new Operacao { numeroA = 2, numeroB = 1, operador1 = "-" }, // Resultado: 1
            new Operacao { numeroA = 3, numeroB = 1, operador1 = "-" }, // Resultado: 2
            new Operacao { numeroA = 3, numeroB = 2, operador1 = "-" }, // Resultado: 1
            new Operacao { numeroA = 4, numeroB = 1, operador1 = "-" }, // Resultado: 3
            new Operacao { numeroA = 4, numeroB = 2, operador1 = "-" }, // Resultado: 2
            new Operacao { numeroA = 4, numeroB = 3, operador1 = "-" }, // Resultado: 1
            new Operacao { numeroA = 5, numeroB = 1, operador1 = "-" }, // Resultado: 4
            new Operacao { numeroA = 5, numeroB = 2, operador1 = "-" }, // Resultado: 3
            new Operacao { numeroA = 5, numeroB = 3, operador1 = "-" }, // Resultado: 2
            new Operacao { numeroA = 5, numeroB = 4, operador1 = "-" }, // Resultado: 1
            new Operacao { numeroA = 6, numeroB = 1, operador1 = "-" }, // Resultado: 5
            new Operacao { numeroA = 6, numeroB = 2, operador1 = "-" }, // Resultado: 4
            new Operacao { numeroA = 6, numeroB = 4, operador1 = "-" }, // Resultado: 2
            new Operacao { numeroA = 7, numeroB = 3, operador1 = "-" }, // Resultado: 4
            new Operacao { numeroA = 8, numeroB = 5, operador1 = "-" }, // Resultado: 3
        };
    }
}
