using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject settingsPanel;

    [Header("escena configuracion")]
    [SerializeField] private string gameplaySceneName = "Gameplay";


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // si el panel de opciones está abierto, lo cierra
            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                // si estamos en el menu Principal, sale del juego
                // si estas en la escena del gameplay, vas al menun principla
                ExitGame();
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void ExitGame()
    { 

        Application.Quit();
    }
}

