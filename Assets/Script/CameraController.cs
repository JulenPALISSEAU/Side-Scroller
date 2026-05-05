using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    int sceneID;
    bool checkerGameLaunchOnce;

    public GameObject CameraHub;

    // Camera Hub

    // Camera Level
    public Transform target;
    public float SmoothTime = 0.0f;
    public Vector3 offset = new Vector3(9f, 0f, -5f);

    public float Distance = 5f;

    Vector3 Velocity = Vector3.zero;



    private void Awake()
    {
        // Récupère l'ID de la scene actuellement chargé
        sceneID = SceneManager.GetActiveScene().buildIndex;

        checkerGameLaunchOnce = PlayerPrefs.HasKey("firstLaunch");
    }

    private void Start()
    {
        switch (sceneID)
        {
            // Camera pour la scene "Hub"
            case 0:
                Instantiate(CameraHub);
                if (!checkerGameLaunchOnce || (PlayerPrefs.GetInt("playerLose")) == 0) {

                }
                break;

            // Camera pour la scene "Level"
            case 1:

                break;
        }
    }


    void FixedUpdate()
    {
        switch (sceneID) {
            // Camera pour la scene "Hub"
            case 0:
                
                break;

            // Camera pour la scene "Level"
            case 1:
                // Sécurité pour empêcher que le jeu crash s'il n'y a aucune cible désigner en ignorant la suite du script
                if (target == null) return;

                // Défini la position de la caméra par rapport à la position de l'élèment cible défini et additionne un Vector3 à celle-ci
                Vector3 targetPosition = target.position + offset;

                // Défini la distance de la caméra et donc la zone afficher
                GetComponent<Camera>().orthographicSize = Distance;

                // Sert à smooth le déplacement de la caméra pour la rendre moins "sec"
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, SmoothTime);
                break;
        }
    }
}
