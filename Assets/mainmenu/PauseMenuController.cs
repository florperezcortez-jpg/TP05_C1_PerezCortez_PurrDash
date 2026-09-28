using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Scene Config")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Update()
    {
        // Al presionar Escape en la escena Gameplay
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        // Asegura que si el tiempo estaba pausado vuelva a la velocidad normal (1)
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}