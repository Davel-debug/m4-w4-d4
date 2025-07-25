using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void Level2()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Level2"); 
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneTracker.Instance.lastLevel);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menù"); 
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Esci");

    }
    public void RealExitGame()
    {
        
        Application.Quit(); 
    }
}
