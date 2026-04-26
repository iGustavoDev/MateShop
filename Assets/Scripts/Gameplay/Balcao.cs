using UnityEngine;

public class Balcao : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    private bool playerPerto = false;
    private NPCCliente npcAtual;

    private void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            EntregarPedido();
        }
    }

    private void EntregarPedido()
    {
        if (GameManager.instance == null || GameManager.instance.JogoEncerrado || GameManager.instance.OperacaoAtual == null)
            return;

        if (GameManager.instance.ItensColetados <= 0 || GameManager.instance.ItemColetadoAtual == null)
        {
            if (uiManager != null)
            {
                uiManager.MostrarAviso("Aviso!", "Você precisa coletar um item antes de entregar.");
            }

            return;
        }

        int itens = GameManager.instance.ItensColetados;
        int pedido = GameManager.instance.OperacaoAtual.CalcularResultado();

        ItemData itemPedido = GameManager.instance.ItemPedidoAtual;
        ItemData itemColetado = GameManager.instance.ItemColetadoAtual;

        bool quantidadeCorreta = itens == pedido;
        bool itemCorreto = GameManager.instance.SaoMesmoItem(itemPedido, itemColetado);
        bool acertou = quantidadeCorreta && itemCorreto;

        if (acertou)
        {
            GameManager.instance.RegistrarAcerto();
        }
        else
        {
            GameManager.instance.RegistrarErro();
        }

        if (uiManager != null)
        {
            uiManager.MostrarFeedback(acertou);
        }

        bool venceu = GameManager.instance.VenceuJogo();
        bool perdeu = GameManager.instance.PerdeuJogo();

        if (npcAtual != null)
        {
            npcAtual.PedidoEntregue();
            npcAtual = null;
        }

        GameManager.instance.ResetarColeta();
        GameManager.instance.LimparPedidoAtual();

        if (uiManager != null && (venceu || perdeu))
        {
            uiManager.MostrarFimDeJogo(venceu, GameManager.instance.EntregasBemSucedidas, GameManager.instance.ErrosAtuais);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
        }

        if (other.CompareTag("NPC"))
        {
            npcAtual = other.GetComponent<NPCCliente>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
        }

        if (other.CompareTag("NPC"))
        {
            npcAtual = null;
        }
    }
}
