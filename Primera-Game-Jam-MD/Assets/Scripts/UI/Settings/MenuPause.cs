using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject menuPause;
    public GameObject flameBackground;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Pausa");
            menuPause.SetActive(true);
            flameBackground.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        menuPause.SetActive(false);
        flameBackground.SetActive(true);
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        ScreenFader.Instance.FadeToScene("MainMenu");
    }
    public void NextLevel(string level)
    {
        Time.timeScale = 1f;
        if (ScreenFader.Instance == null)
        {
            Debug.LogError("No hay ScreenFader en la escena");
            return;
        }
        ScreenFader.Instance.FadeToScene(level);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        ScreenFader.Instance.FadeToScene("Matu");

        menuPause.SetActive(false);
        flameBackground.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
