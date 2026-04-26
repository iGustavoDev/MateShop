using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Feedback Rapido")]
    [SerializeField] private GameObject containerFeedback;
    [SerializeField] private TextMeshProUGUI textoFeedback;
    [SerializeField] private Image iconeFeedback;
    [SerializeField] private Sprite spriteAcerto;
    [SerializeField] private Sprite spriteErro;
    [SerializeField] private AudioSource audioSourceFeedback;
    [SerializeField] private AudioClip somAcerto;
    [SerializeField] private AudioClip somErro;

    [Header("Pontuacao")]
    [SerializeField] private TextMeshProUGUI textoAcertos;
    [SerializeField] private TextMeshProUGUI textoErros;

    [Header("Painel de Item Coletado")]
    [SerializeField] private GameObject painelItemColetado;
    [SerializeField] private Image iconeItemColetado;
    [SerializeField] private TextMeshProUGUI textoQuantidadeItemColetado;

    [Header("Painel de Aviso")]
    [SerializeField] private GameObject painelAviso;
    [SerializeField] private TMP_Text tituloAviso;
    [SerializeField] private TMP_Text textoAviso;
    [SerializeField] private Button botaoConfirmarAviso;

    [Header("Painel de Pausa")]
    [SerializeField] private GameObject painelPausa;
    [SerializeField] private Button botaoContinuar;
    [SerializeField] private Button botaoReiniciar;
    [SerializeField] private Button botaoMenu;

    [Header("Painel de Tutorial")]
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private Button botaoComecarTutorial;

    [Header("Painel de Fim de Jogo")]
    [SerializeField] private GameObject painelFimDeJogo;
    [SerializeField] private TMP_Text tituloFimDeJogo;
    [SerializeField] private TMP_Text textoFimDeJogo;
    [SerializeField] private Button botaoPrimarioFimDeJogo;
    [SerializeField] private TMP_Text textoBotaoPrimarioFimDeJogo;
    [SerializeField] private Button botaoSecundarioFimDeJogo;
    [SerializeField] private TMP_Text textoBotaoSecundarioFimDeJogo;

    private bool jogoPausado = false;
    private bool tutorialAberto = false;

    private void Start()
    {
        RegistrarEventoColeta();
        LimparFeedback();
        Time.timeScale = 1f;

        if (painelAviso != null)
        {
            painelAviso.SetActive(false);
        }

        if (botaoConfirmarAviso != null)
        {
            botaoConfirmarAviso.onClick.RemoveListener(EsconderAviso);
            botaoConfirmarAviso.onClick.AddListener(EsconderAviso);
        }

        if (painelPausa != null)
        {
            painelPausa.SetActive(false);
        }

        ConfigurarBotaoTutorial();
        ConfigurarBotoesPausa();
        ConfigurarBotoesFimDeJogo();

        if (painelFimDeJogo != null)
        {
            painelFimDeJogo.SetActive(false);
        }

        AtualizarPontuacao();
        AtualizarPainelItemColetado();
        MostrarTutorial();
    }

    private void OnEnable()
    {
        RegistrarEventoColeta();
    }

    private void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnColetaAtualizada -= AtualizarPainelItemColetado;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !FimDeJogoAberto() && !tutorialAberto)
        {
            AlternarPausa();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F8))
        {
            RegistrarErroDebug();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            RegistrarAcertoDebug(1);
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            RegistrarAcertoDebug(10);
        }
#endif

        AtualizarPontuacao();
    }

    public void MostrarFeedback(bool acertou)
    {
        if (containerFeedback != null)
        {
            containerFeedback.SetActive(true);
        }

        if (textoFeedback != null)
        {
            if (acertou)
            {
                textoFeedback.text = "Certo!";
            }
            else
            {
                textoFeedback.text = "Errado!";
            }
        }

        if (iconeFeedback != null)
        {
            iconeFeedback.gameObject.SetActive(true);
            iconeFeedback.sprite = acertou ? spriteAcerto : spriteErro;
        }

        TocarSomFeedback(acertou);

        CancelInvoke(nameof(LimparFeedback));
        Invoke(nameof(LimparFeedback), 2f);
    }

    public void MostrarAviso(string titulo, string mensagem)
    {
        if (painelAviso == null)
            return;

        painelAviso.SetActive(true);

        if (tituloAviso != null)
        {
            tituloAviso.text = titulo;
        }

        if (textoAviso != null)
        {
            textoAviso.text = mensagem;
        }
    }

    public void EsconderAviso()
    {
        if (painelAviso != null)
        {
            painelAviso.SetActive(false);
        }
    }

    public void AlternarPausa()
    {
        if (tutorialAberto || FimDeJogoAberto())
            return;

        if (jogoPausado)
        {
            ContinuarJogo();
        }
        else
        {
            PausarJogo();
        }
    }

    public void ContinuarJogo()
    {
        jogoPausado = false;
        Time.timeScale = 1f;

        if (painelPausa != null)
        {
            painelPausa.SetActive(false);
        }
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        jogoPausado = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        jogoPausado = false;
        tutorialAberto = false;
        SceneManager.LoadScene("Menu");
    }

    public void MostrarTutorial()
    {
        if (painelTutorial == null)
            return;

        tutorialAberto = true;
        jogoPausado = false;
        Time.timeScale = 0f;
        painelTutorial.SetActive(true);
    }

    public void FecharTutorial()
    {
        tutorialAberto = false;
        Time.timeScale = 1f;

        if (painelTutorial != null)
        {
            painelTutorial.SetActive(false);
        }
    }

    public void MostrarFimDeJogo(bool venceu, int acertos, int erros)
    {
        Time.timeScale = 0f;
        jogoPausado = false;

        if (painelPausa != null)
        {
            painelPausa.SetActive(false);
        }

        if (painelFimDeJogo == null)
            return;

        painelFimDeJogo.SetActive(true);

        if (tituloFimDeJogo != null)
        {
            tituloFimDeJogo.text = venceu ? "Você venceu!" : "Você perdeu!";
        }

        if (textoFimDeJogo != null && GameManager.instance != null)
        {
            string melhorTempo = GameManager.instance.TemMelhorTempoRegistrado
                ? FormatarTempo(GameManager.instance.MelhorTempoRegistrado)
                : "--:--";

            textoFimDeJogo.text =
                $"Acertos: {acertos}/{GameManager.instance.MetaEntregas}\n" +
                $"Erros: {erros}/{GameManager.instance.MaxErros}\n" +
                $"Tempo total: {FormatarTempo(GameManager.instance.TempoPartidaAtual)}\n" +
                $"Melhor tempo: {melhorTempo}";
        }

        if (textoBotaoPrimarioFimDeJogo != null)
        {
            textoBotaoPrimarioFimDeJogo.text = venceu ? "Jogar novamente" : "Tentar novamente";
        }

        if (textoBotaoSecundarioFimDeJogo != null)
        {
            textoBotaoSecundarioFimDeJogo.text = "Voltar ao menu";
        }

        if (botaoSecundarioFimDeJogo != null)
        {
            botaoSecundarioFimDeJogo.gameObject.SetActive(true);
        }
    }

    private void LimparFeedback()
    {
        if (containerFeedback != null)
        {
            containerFeedback.SetActive(false);
        }

        if (textoFeedback != null)
        {
            textoFeedback.text = "";
        }

        if (iconeFeedback != null)
        {
            iconeFeedback.sprite = null;
            iconeFeedback.gameObject.SetActive(false);
        }
    }

    private void TocarSomFeedback(bool acertou)
    {
        if (audioSourceFeedback == null)
            return;

        AudioClip clip = acertou ? somAcerto : somErro;

        if (clip == null)
            return;

        audioSourceFeedback.PlayOneShot(clip);
    }

    private void AtualizarPontuacao()
    {
        if (GameManager.instance == null)
            return;

        if (textoAcertos != null)
        {
            textoAcertos.text = $"{GameManager.instance.EntregasBemSucedidas}/{GameManager.instance.MetaEntregas}";
        }

        if (textoErros != null)
        {
            textoErros.text = $"{GameManager.instance.ErrosAtuais}/{GameManager.instance.MaxErros}";
        }
    }

    private void AtualizarPainelItemColetado()
    {
        if (painelItemColetado == null)
            return;

        if (GameManager.instance == null || GameManager.instance.ItemColetadoAtual == null || GameManager.instance.ItensColetados <= 0)
        {
            painelItemColetado.SetActive(false);

            if (iconeItemColetado != null)
            {
                iconeItemColetado.sprite = null;
                iconeItemColetado.enabled = false;
            }

            if (textoQuantidadeItemColetado != null)
            {
                textoQuantidadeItemColetado.text = string.Empty;
            }

            return;
        }

        painelItemColetado.SetActive(true);

        if (iconeItemColetado != null)
        {
            Sprite spriteItem = GameManager.instance.ItemColetadoAtual.Sprite;
            iconeItemColetado.sprite = spriteItem;
            iconeItemColetado.enabled = spriteItem != null;
        }

        if (textoQuantidadeItemColetado != null)
        {
            textoQuantidadeItemColetado.text = GameManager.instance.ItensColetados.ToString();
        }
    }

    private void PausarJogo()
    {
        jogoPausado = true;
        Time.timeScale = 0f;

        if (painelPausa != null)
        {
            painelPausa.SetActive(true);
        }
    }

    private void ConfigurarBotoesPausa()
    {
        if (botaoContinuar != null)
        {
            botaoContinuar.onClick.RemoveListener(ContinuarJogo);
            botaoContinuar.onClick.AddListener(ContinuarJogo);
        }

        if (botaoReiniciar != null)
        {
            botaoReiniciar.onClick.RemoveListener(ReiniciarJogo);
            botaoReiniciar.onClick.AddListener(ReiniciarJogo);
        }

        if (botaoMenu != null)
        {
            botaoMenu.onClick.RemoveListener(VoltarAoMenu);
            botaoMenu.onClick.AddListener(VoltarAoMenu);
        }
    }

    private void ConfigurarBotaoTutorial()
    {
        if (botaoComecarTutorial != null)
        {
            botaoComecarTutorial.onClick.RemoveListener(FecharTutorial);
            botaoComecarTutorial.onClick.AddListener(FecharTutorial);
        }

        if (painelTutorial != null)
        {
            painelTutorial.SetActive(false);
        }
    }

    private void ConfigurarBotoesFimDeJogo()
    {
        if (botaoPrimarioFimDeJogo != null)
        {
            botaoPrimarioFimDeJogo.onClick.RemoveListener(ReiniciarJogo);
            botaoPrimarioFimDeJogo.onClick.AddListener(ReiniciarJogo);
        }

        if (botaoSecundarioFimDeJogo != null)
        {
            botaoSecundarioFimDeJogo.onClick.RemoveListener(VoltarAoMenu);
            botaoSecundarioFimDeJogo.onClick.AddListener(VoltarAoMenu);
        }
    }

    private bool FimDeJogoAberto()
    {
        return painelFimDeJogo != null && painelFimDeJogo.activeSelf;
    }

    private void RegistrarEventoColeta()
    {
        if (GameManager.instance == null)
            return;

        GameManager.instance.OnColetaAtualizada -= AtualizarPainelItemColetado;
        GameManager.instance.OnColetaAtualizada += AtualizarPainelItemColetado;
    }

    private string FormatarTempo(float tempoEmSegundos)
    {
        int minutos = Mathf.FloorToInt(tempoEmSegundos / 60f);
        int segundos = Mathf.FloorToInt(tempoEmSegundos % 60f);

        return $"{minutos:00}:{segundos:00}";
    }

#if UNITY_EDITOR
    private void RegistrarErroDebug()
    {
        if (GameManager.instance == null || GameManager.instance.JogoEncerrado)
            return;

        GameManager.instance.RegistrarErro();
        VerificarFimDeJogoDebug();
    }

    private void RegistrarAcertoDebug(int quantidade)
    {
        if (GameManager.instance == null || GameManager.instance.JogoEncerrado)
            return;

        for (int i = 0; i < quantidade; i++)
        {
            if (GameManager.instance.JogoEncerrado)
                break;

            GameManager.instance.RegistrarAcerto();
        }

        VerificarFimDeJogoDebug();
    }

    private void VerificarFimDeJogoDebug()
    {
        if (GameManager.instance == null || FimDeJogoAberto())
            return;

        bool venceu = GameManager.instance.VenceuJogo();
        bool perdeu = GameManager.instance.PerdeuJogo();

        if (venceu || perdeu)
        {
            MostrarFimDeJogo(venceu, GameManager.instance.EntregasBemSucedidas, GameManager.instance.ErrosAtuais);
        }
    }
#endif
}
