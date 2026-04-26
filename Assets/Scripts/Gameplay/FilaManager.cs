using System.Collections.Generic;
using UnityEngine;

public class FilaManager : MonoBehaviour
{
    public static FilaManager instance;

    [Header("Pontos da fila (ordem importa!)")]
    public List<Transform> pontosFila = new List<Transform>();

    [Header("NPCs na fila")]
    public List<NPCCliente> fila = new List<NPCCliente>();

    void Awake()
    {
        instance = this;
    }

    public void EntrarNaFila(NPCCliente npc)
    {
        fila.Add(npc);
        AtualizarFila();
    }

    public void SairDaFila(NPCCliente npc)
    {
        fila.Remove(npc);
        AtualizarFila();
    }

    public bool TemEspacoNaFila()
    {
        return fila.Count < pontosFila.Count;
    }

    void AtualizarFila()
    {
        for (int i = 0; i < fila.Count; i++)
        {
            if (i >= pontosFila.Count)
                return;

            fila[i].DefinirDestino(pontosFila[i].position);

            // ❌ NÃO gera pedido aqui mais
        }
    }
}