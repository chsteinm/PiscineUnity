using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void OnClickStartButton()
   {
       // Start the game
       SceneManager.LoadScene(1);
   }

   public void OnClickQuitButton()
   {
       // Quit the game
       Application.Quit();
   }
}
