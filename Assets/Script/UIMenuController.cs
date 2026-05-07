using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public GameObject TitleMenuUI;
    public GameObject Chara;

    public void Play()
    {
        Instantiate(Chara); 
        //Chara.SetActive(true);
        Destroy(TitleMenuUI);
    }

    public void Resume()
    {
        Destroy(TitleMenuUI);
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