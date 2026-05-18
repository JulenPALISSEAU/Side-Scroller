using UnityEngine;

public class ResetPlayerPrefsScript : MonoBehaviour
{
    void Start()
    {
        // Utilisé uniquement pour reset et tester les informations stocker dans le PlayerPrefs tels que le niveau joueur
        PlayerPrefs.DeleteAll();
    }
}
