using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Dados do item")]
    [SerializeField] private ItemData itemData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public ItemData ItemData => itemData;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        AtualizarVisual();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.RegistrarItemProximo(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.RemoverItemProximo(this);
        }
    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += AtualizarVisualNoEditor;
#endif
    }

    private void AtualizarVisual()
    {
        if (itemData == null || itemData.Sprite == null)
            return;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = itemData.Sprite;
        }
    }

#if UNITY_EDITOR
    private void AtualizarVisualNoEditor()
    {
        if (this == null)
            return;

        UnityEditor.EditorApplication.delayCall -= AtualizarVisualNoEditor;
        AtualizarVisual();
    }
#endif
}
