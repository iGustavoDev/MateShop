using System.Collections.Generic;

public static class OperacoesPorDificuldade
{
    public static List<Operacao> Obter(GameDifficulty difficulty)
    {
        switch (difficulty)
        {
            case GameDifficulty.Facil:
                return OperacoesFacil.Criar();
            case GameDifficulty.Medio:
                return OperacoesMedio.Criar();
            case GameDifficulty.Dificil:
                return OperacoesDificil.Criar();
            default:
                return OperacoesFacil.Criar();
        }
    }
}
