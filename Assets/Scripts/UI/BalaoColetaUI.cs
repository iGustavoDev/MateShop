using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BalaoColetaUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image iconeItem;
    [SerializeField] private TMP_Text textoQuantidade;

    private void Start()
    {
        Esconder();
    }

    private void OnDisable()
    {
        Esconder();
    }

    public void Mostrar(ItemData item, int quantidade)
    {
        if (container == null || item == null)
            return;

        container.SetActive(true);

        if (iconeItem != null)
        {
            iconeItem.sprite = item.Sprite;
            iconeItem.enabled = item.Sprite != null;
        }

        AtualizarQuantidade(quantidade);
    }

    public void AtualizarQuantidade(int quantidade)
    {
        if (textoQuantidade == null)
            return;

        textoQuantidade.gameObject.SetActive(true);
        textoQuantidade.text = Mathf.Clamp(quantidade, 0, 99).ToString();
    }

    public void Esconder()
    {
        if (container != null)
        {
            container.SetActive(false);
        }
    }
}
