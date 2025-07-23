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
        //Application.Quit(); se vuoi chiuderlo
    }
}
