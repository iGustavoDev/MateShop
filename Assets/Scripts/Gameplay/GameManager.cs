using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const string PLAYER_PREFS_MELHOR_TEMPO_PREFIXO = "MateShop_MelhorTempo_";
    private const int MAX_ITENS_COLETADOS = 99;
    private const int MAX_ERROS = 5;
    private const int META_FACIL = 30;
    private const int META_MEDIO = 40;
    private const int META_DIFICIL = 50;

    [Header("Itens Coletados")]
    [SerializeField] private int itensColetados = 0;
    [SerializeField] private ItemData itemColetadoAtual;
    [SerializeField] private bool temItemSelecionado = false; 

    [Header("Catalogo da Loja")]
    [SerializeField] private List<ItemData> catalogoItens = new List<ItemData>();

    [Header("Referencias")]
    [SerializeField] private BalaoPedidoUI balaoPedidoUI;

    [Header("Pedido Atual")]
    [SerializeField] private ItemData itemPedidoAtual;
    [SerializeField] private Operacao operacaoAtual;

    [Header("Pontuacao")]
    [SerializeField] private int entregasBemSucedidas = 0;
    [SerializeField] private int errosAtuais = 0;
    [SerializeField] private bool jogoEncerrado = false;

    [Header("Tempo de Partida")]
    [SerializeField] private float tempoPartidaAtual = 0f;

    public event System.Action OnColetaAtualizada;

    public int ItensColetados => itensColetados;
    public ItemData ItemColetadoAtual => itemColetadoAtual;
    public ItemData ItemPedidoAtual => itemPedidoAtual;
    public Operacao OperacaoAtual => operacaoAtual;
    public int EntregasBemSucedidas => entregasBemSucedidas;
    public int ErrosAtuais => errosAtuais;
    public int MaxErros => MAX_ERROS;
    public int MetaEntregas => ObterMetaEntregas();
    public bool JogoEncerrado => jogoEncerrado;
    public float TempoPartidaAtual => tempoPartidaAtual;
    public bool TemMelhorTempoRegistrado => PlayerPrefs.HasKey(ObterChaveMelhorTempo());
    public float MelhorTempoRegistrado => PlayerPrefs.GetFloat(ObterChaveMelhorTempo(), 0f);

    private void Awake()
    {
        instance = this;

        if (balaoPedidoUI != null)
        {
            balaoPedidoUI.Esconder();
        }
    }

    private void Update()
    {
        if (jogoEncerrado)
            return;

        tempoPartidaAtual += Time.deltaTime;
    }

    public void GerarPedido()
    {
        if (jogoEncerrado)
            return;

        if (catalogoItens == null || catalogoItens.Count == 0)
        {
            Debug.LogError("GameManager sem itens cadastrados no catalogo.");
            return;
        }

        List<Operacao> operacoes = OperacoesPorDificuldade.Obter(GameSettings.SelectedDifficulty);

        if (operacoes == null || operacoes.Count == 0)
        {
            Debug.LogError($"Nenhuma operacao cadastrada para a dificuldade {GameSettings.SelectedDifficulty}.");
            return;
        }

        itemPedidoAtual = catalogoItens[Random.Range(0, catalogoItens.Count)];
        operacaoAtual = operacoes[Random.Range(0, operacoes.Count)];

        if (balaoPedidoUI != null)
        {
            balaoPedidoUI.Mostrar(itemPedidoAtual.Sprite, operacaoAtual.GetTexto());
        }
    }

    public void AdicionarItem(ItemData item)
    {
        if (jogoEncerrado)
            return;

        if (item == null)
        {
            Debug.LogWarning("Tentativa de coletar item nulo.");
            return;
        }

        bool trocouDeItem = temItemSelecionado && !SaoMesmoItem(itemColetadoAtual, item);

        if (trocouDeItem)
        {
            itensColetados = 0;
        }

        if (!temItemSelecionado || trocouDeItem)
        {
            itemColetadoAtual = item;
            temItemSelecionado = true;
        }

        if (itensColetados >= MAX_ITENS_COLETADOS)
            return;

        if (SaoMesmoItem(itemColetadoAtual, item))
        {
            itensColetados++;
        }

        NotificarColetaAtualizada();
    }

    public void RemoverItem()
    {
        if (jogoEncerrado)
            return;

        if (itensColetados <= 0)
            return;

        itensColetados--;

        if (itensColetados == 0)
        {
            itemColetadoAtual = null;
            temItemSelecionado = false;
        }

        NotificarColetaAtualizada();
    }

    public void ResetarColeta()
    {
        itensColetados = 0;
        itemColetadoAtual = null;
        temItemSelecionado = false;
        NotificarColetaAtualizada();
    }

    public void LimparPedidoAtual()
    {
        itemPedidoAtual = null;
        operacaoAtual = null;

        if (balaoPedidoUI != null)
        {
            balaoPedidoUI.Esconder();
        }
    }

    public bool SaoMesmoItem(ItemData itemA, ItemData itemB)
    {
        if (itemA == null || itemB == null)
            return false;

        return itemA.Corresponde(itemB);
    }

    public void RegistrarAcerto()
    {
        if (jogoEncerrado)
            return;

        entregasBemSucedidas++;

        if (entregasBemSucedidas >= MetaEntregas)
        {
            jogoEncerrado = true;
            RegistrarMelhorTempoSeNecessario();
        }
    }

    public void RegistrarErro()
    {
        if (jogoEncerrado)
            return;

        if (errosAtuais >= MAX_ERROS)
            return;

        errosAtuais++;

        if (errosAtuais >= MAX_ERROS)
        {
            jogoEncerrado = true;
        }
    }

    public bool VenceuJogo()
    {
        return entregasBemSucedidas >= MetaEntregas;
    }

    public bool PerdeuJogo()
    {
        return errosAtuais >= MAX_ERROS;
    }

    private void NotificarColetaAtualizada()
    {
        OnColetaAtualizada?.Invoke();
    }

    private int ObterMetaEntregas()
    {
        switch (GameSettings.SelectedDifficulty)
        {
            case GameDifficulty.Facil:
                return META_FACIL;
            case GameDifficulty.Medio:
                return META_MEDIO;
            case GameDifficulty.Dificil:
                return META_DIFICIL;
            default:
                return META_FACIL;
        }
    }

    private void RegistrarMelhorTempoSeNecessario()
    {
        string chave = ObterChaveMelhorTempo();

        if (!PlayerPrefs.HasKey(chave) || tempoPartidaAtual < PlayerPrefs.GetFloat(chave))
        {
            PlayerPrefs.SetFloat(chave, tempoPartidaAtual);
            PlayerPrefs.Save();
        }
    }

    private string ObterChaveMelhorTempo()
    {
        return PLAYER_PREFS_MELHOR_TEMPO_PREFIXO + GameSettings.SelectedDifficulty;
    }
}
