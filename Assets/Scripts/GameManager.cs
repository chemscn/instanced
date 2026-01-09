using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void ShowOptionsScreen()
    {
        SceneManager.LoadScene("OptionsScreen");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Applicaton");
        // This works only in a real build
        Application.Quit();
    }
    public void NextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }


}
