using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paineis")]
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuDificuldade;

    [Header("Cena")]
    [SerializeField] private string nomeCenaGameplay = "GamePlay";

    private void Start()
    {
        MostrarMenuPrincipal();
    }

    public void AbrirSelecaoDeDificuldade()
    {
        if (menuPrincipal != null)
        {
            menuPrincipal.SetActive(false);
        }

        if (menuDificuldade != null)
        {
            menuDificuldade.SetActive(true);
        }
    }

    public void VoltarParaMenuPrincipal()
    {
        MostrarMenuPrincipal();
    }

    public void JogarFacil()
    {
        IniciarJogo(GameDifficulty.Facil);
    }

    public void JogarMedio()
    {
        IniciarJogo(GameDifficulty.Medio);
    }

    public void JogarDificil()
    {
        IniciarJogo(GameDifficulty.Dificil);
    }

    public void Sair()
    {
        Application.Quit();
    }

    private void IniciarJogo(GameDifficulty difficulty)
    {
        GameSettings.SetDifficulty(difficulty);
        SceneManager.LoadScene(nomeCenaGameplay);
    }

    private void MostrarMenuPrincipal()
    {
        if (menuPrincipal != null)
        {
            menuPrincipal.SetActive(true);
        }

        if (menuDificuldade != null)
        {
            menuDificuldade.SetActive(false);
        }
    }
}
