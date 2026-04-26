using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalaoPedidoUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image iconeItem;
    [SerializeField] private TMP_Text textoOperacao;

    private void Start()
    {
        Esconder();
    }

    private void OnDisable()
    {
        Esconder();
    }

    public void Mostrar(Sprite icone, string texto)
    {
        if (container == null)
            return;

        container.SetActive(true);

        if (iconeItem != null)
        {
            iconeItem.sprite = icone;
            iconeItem.enabled = icone != null;
        }

        if (textoOperacao != null)
        {
            textoOperacao.text = texto;
        }
    }

    public void Esconder()
    {
        if (container != null)
        {
            container.SetActive(false);
        }
    }
}
