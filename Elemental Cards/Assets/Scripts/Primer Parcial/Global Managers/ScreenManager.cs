using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager: MonoBehaviour
{
    public static ScreenManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        GlobalVariables.Instance.Restore();
        SceneManager.LoadScene(1);
    }

    public void ReturnToMap() => SceneManager.LoadScene(1);

    public void GoToMainMenu() => SceneManager.LoadScene(0);

    public void ShowScoreBoard() => SceneManager.LoadScene(5);

    public void PlayerLose() => SceneManager.LoadScene(4);

    public void PlayerWin() => SceneManager.LoadScene(3);

    public void PlayerBattle() => SceneManager.LoadScene(2);

    public void Quit() => Application.Quit();

    public Scene CurrentScene() => SceneManager.GetActiveScene();
}
