using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneController : MonoBehaviour
{
    int sceneID;

    public GameObject Chara;
    public GameObject CameraHub;
    public GameObject TitleMenuUI;

    public PlayerController CharaState;

    void Awake()
    {
        // Récupère l'ID de la scene actuellement chargé
        sceneID = SceneManager.GetActiveScene().buildIndex;

        // Vérifie si le jeu est lancé pour la première fois ou non
        bool checkerGameLaunchOnce = PlayerPrefs.HasKey("firstLaunch");
        // Si le joueur n'a jamais lancé le jeu, initialise les valeurs firstLaunch, wherePlayer, playerWon et playerLose
        if (checkerGameLaunchOnce == false) {
            Debug.Log("Patate");
            PlayerPrefs.SetInt("firstLaunch", 0);               // Pour savoir si c'est la première fois que le jeu est lancé
            PlayerPrefs.SetInt("wherePlayer", sceneID);         // Pour savoir où est/était le joueur
            PlayerPrefs.SetInt("playerWon", 0);                 // Pour savoir si le joueur à perdu ou non
            PlayerPrefs.SetInt("playerLose", 0);                // Pour savoir si le joueur à gagné ou non

            PlayerPrefs.SetInt("playerLevel", 1);               // Pour conserver le niveau du joueur
            PlayerPrefs.SetInt("currentEXP", 0);                // Pour conserver la quantité d'EXP obtenu par le joueur
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        switch (sceneID) {
            // Initialisation de la scene "Hub"
            case 0:
                // Si le joueur a perdu
                if ((PlayerPrefs.GetInt("playerLose")) == 1) {
                    Instantiate(Chara, new Vector2(-3.6f, -0.95f), Quaternion.Euler(0f, 0f, 0f));
                    CharaState.isFighting = false;
                    CharaState.isPaused = false;
                    Instantiate(CameraHub);

                    PlayerPrefs.SetInt("playerLose", 0);
                    PlayerPrefs.Save();
                }
                else {
                    Instantiate(CameraHub);
                    Instantiate(TitleMenuUI);
                }
                break;

            // Initialisation de la scene "level"
            case 1:
                Instantiate(Chara, new Vector2(-7f, -0.85f), Quaternion.Euler(0f, 0f, 0f));
                break;
        }

        // Termine en sauvegardant l'ID de scene dans laquelle le joueur ce situe
        PlayerPrefs.SetInt("wherePlayer", sceneID);
        PlayerPrefs.Save();
    }
}
