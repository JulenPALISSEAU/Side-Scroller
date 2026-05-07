using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeSceneController : MonoBehaviour
{
    int sceneID;

    public GameObject Chara;
    public GameObject CameraHub;
    public GameObject TitleMenuUI;

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

                    Instantiate(Chara);
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
                Instantiate(Chara);
                break;
        }

        // Termine en sauvegardant l'ID de scene dans laquelle le joueur ce situe
        PlayerPrefs.SetInt("wherePlayer", sceneID);
        PlayerPrefs.Save();
    }

    void Update()
    {
        
    }
}
