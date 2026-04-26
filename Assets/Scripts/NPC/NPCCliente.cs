using UnityEngine;

public class NPCCliente : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private Transform pontoSaida;
    [SerializeField] private float velocidade = 2f;

    [Header("Visual do NPC")]
    [SerializeField] private RuntimeAnimatorController[] npcControllers;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 direcao;
    private Vector2 destino;

    private bool indoEmbora = false;
    private bool chegouNoBalcao = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        AplicarVisualAleatorio();
        FilaManager.instance.EntrarNaFila(this);
    }

    private void Update()
    {
        if (indoEmbora)
        {
            if (pontoSaida == null)
            {
                Debug.LogError("NPC sem ponto de saida configurado.");
                return;
            }

            MoverPara(pontoSaida.position);

            if (Vector2.Distance(transform.position, pontoSaida.position) < 0.2f)
            {
                Destroy(gameObject);
            }

            return;
        }

        MoverPara(destino);

        float distancia = Vector2.Distance(transform.position, destino);

        if (distancia < 0.2f)
        {
            direcao = Vector2.zero;
            anim.SetInteger("Movimento", 0);

            if (!chegouNoBalcao &&
                FilaManager.instance.fila.Count > 0 &&
                FilaManager.instance.fila[0] == this)
            {
                chegouNoBalcao = true;
                GameManager.instance.GerarPedido();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(
            rb.position + direcao.normalized * velocidade * Time.fixedDeltaTime
        );
    }

    private void MoverPara(Vector2 alvo)
    {
        direcao = alvo - (Vector2)transform.position;

        if (direcao.sqrMagnitude > 0.01f)
        {
            direcao = direcao.normalized;
            anim.SetInteger("Movimento", 1);
        }
        else
        {
            direcao = Vector2.zero;
            anim.SetInteger("Movimento", 0);
        }

        Flip();
    }

    private void Flip()
    {
        if (direcao.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }
        else if (direcao.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    public void DefinirDestino(Vector2 pos)
    {
        destino = pos;
    }

    public void ConfigurarPontoSaida(Transform novoPontoSaida)
    {
        pontoSaida = novoPontoSaida;
    }

    public void PedidoEntregue()
    {
        indoEmbora = true;
        FilaManager.instance.SairDaFila(this);
    }

    private void AplicarVisualAleatorio()
    {
        if (npcControllers == null || npcControllers.Length == 0)
        {
            Debug.LogWarning("Nenhum controller de NPC configurado!");
            return;
        }

        int index = Random.Range(0, npcControllers.Length);
        anim.runtimeAnimatorController = npcControllers[index];
    }
}
