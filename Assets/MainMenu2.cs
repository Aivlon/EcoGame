using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene(1);
   }
   public void QuitGame()
   {
      Debug.Log("Quit!!!");
      Application.Quit();
   }
   
}
