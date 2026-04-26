using UnityEngine;

[System.Serializable]
public class Operacao
{
    public int numeroA;
    public int numeroB;
    public int numeroC;

    public string operador1;
    public string operador2;

    public bool usarTerceiroNumero;

    public int CalcularResultado()
    {
        int resultadoParcial = Calcular(numeroA, numeroB, operador1);

        if (usarTerceiroNumero)
        {
            return Calcular(resultadoParcial, numeroC, operador2);
        }

        return resultadoParcial;
    }

    int Calcular(int a, int b, string op)
    {
        switch (op)
        {
            case "+": return a + b;
            case "-": return a - b;
            case "*": return a * b;
            case "/": return b != 0 ? a / b : 0;
        }

        return 0;
    }

    public string GetTexto()
    {
        if (usarTerceiroNumero)
        {
            return numeroA + FormatarOperador(operador1) + numeroB + FormatarOperador(operador2) + numeroC;
        }

        return numeroA + FormatarOperador(operador1) + numeroB;
    }

    string FormatarOperador(string op)
    {
        switch (op)
        {
            case "*": return "x";
            case "/": return "÷";
            default: return op;
        }
    }
}
