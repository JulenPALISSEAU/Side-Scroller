using UnityEngine;

public class ResetPlayerPrefsScript : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
