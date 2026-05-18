using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public PlayerController CharaController;
    
    public GameObject TitleMenuUI;
    public GameObject PauseMenuUI;
    public GameObject Chara;

    public PlayerController CharaState;

    public void Play()
    {
        // Fait appraître le personnage au hub et retire le menu titre
        Instantiate(Chara, new Vector2(-3.6f, -0.95f), Quaternion.Euler(0f, 0f, 0f));
        CharaState.isFighting = false;
        CharaState.isPaused = false;
        Destroy(TitleMenuUI);
    }

    public void Resume()
    {
        // Retire le menu pause
        CharaController.isPaused = false;
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }

    public void Setting()
    {
        // Placeholder pour les paramètres
        TitleMenuUI.SetActive(false);
    }

    public void Quit()
    {
        // Ferme l'application
        Application.Quit();
    }
}