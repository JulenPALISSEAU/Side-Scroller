using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneController : MonoBehaviour
{
    // Pour la navigation entre les différentes scènes en rentrant en collision contre certains mur précis
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switch (this.gameObject.tag){
                // Pour accéder au niveau par le hub
                case "AccessLevel":
                    SceneManager.LoadScene(1);
                    break;
                // Pour accéder au hub par le niveau (défaite)
                case "DeathWall":
                    PlayerPrefs.SetInt("playerLose", 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene(0);
                    break;
                // Placeholder pour la condition de victoire
                case "VictoryWall":
                    Debug.Log("WinScreen");
                    break;
            }
        }
    }
}