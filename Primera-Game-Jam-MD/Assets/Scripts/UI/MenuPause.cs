using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        menuPause.SetActive(false);
        flameBackground.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
