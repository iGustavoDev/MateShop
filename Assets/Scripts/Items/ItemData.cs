using UnityEngine;

[CreateAssetMenu(fileName = "NovoItem", menuName = "MateShop/Itens/Item")]
public class ItemData : ScriptableObject
{
    [Header("Identificacao")]
    [SerializeField] private string id = "novo-item";
    [SerializeField] private string nomeExibicao = "Novo Item";

    [Header("Visual")]
    [SerializeField] private Sprite sprite;

    public string Id => id;
    public string NomeExibicao => string.IsNullOrWhiteSpace(nomeExibicao) ? name : nomeExibicao;
    public Sprite Sprite => sprite;

    public bool Corresponde(ItemData outroItem)
    {
        if (outroItem == null)
            return false;

        if (ReferenceEquals(this, outroItem))
            return true;

        if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(outroItem.id))
            return id == outroItem.id;

        return false;
    }
}
