using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
     public GameObject ui;

     public SceneFader sceneFader;

     public string menuSceneName = "MainMenu";

     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
               Toogle();
     }

     public void Toogle()
     {
          ui.SetActive(!ui.activeSelf);

          if (ui.activeSelf)
               Time.timeScale = 0f;
          else
               Time.timeScale = 1f;
     }
     public void Retry()
     {
          Toogle();
          sceneFader.FadeTo(SceneManager.GetActiveScene().name);
     }
     public void Menu()
     {
          Toogle();
          sceneFader.FadeTo(menuSceneName);
     }
}
