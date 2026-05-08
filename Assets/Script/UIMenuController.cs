using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public PlayerController CharaController;
    
    public GameObject TitleMenuUI;
    public GameObject PauseMenuUI;
    public GameObject Chara;

    public void Play()
    {
        Instantiate(Chara);
        Destroy(TitleMenuUI);
    }

    public void Resume()
    {
        CharaController.isPaused = false;
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }

    public void Setting()
    {
        TitleMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}