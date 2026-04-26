using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform pontoSpawn;
    [SerializeField] private Transform pontoSaida;
    [SerializeField] private float tempoEntreNPCs = 5f;

    private void Start()
    {
        InvokeRepeating(nameof(TentarSpawnar), 2f, tempoEntreNPCs);
    }

    private void TentarSpawnar()
    {
        if (!FilaManager.instance.TemEspacoNaFila())
            return;

        GameObject npcInstanciado = Instantiate(npcPrefab, pontoSpawn.position, Quaternion.identity);
        NPCCliente npcCliente = npcInstanciado.GetComponent<NPCCliente>();

        if (npcCliente != null)
        {
            npcCliente.ConfigurarPontoSaida(pontoSaida);
        }
    }
}
