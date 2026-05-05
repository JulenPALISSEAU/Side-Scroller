using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch (this.gameObject.tag){
                case "AccessLevel":
                    SceneManager.LoadScene(1);
                    break;
                case "DeathWall":
                    PlayerPrefs.SetInt("playerLose", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(0);
                    break;
                case "VictoryWall":
                    Debug.Log("WinScreen");
                    break;
            }
        }
    }
}