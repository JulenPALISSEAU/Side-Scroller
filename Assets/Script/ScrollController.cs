using UnityEngine;

public class ScrollController : MonoBehaviour
{
    void FixedUpdate()
    {
        // Modifie la position des côtés de l'écran pour les faire se déplacer à droite toutes les secondes (Time.deltaTime)
        transform.position += new Vector3(1f * Time.deltaTime, 0, 0);
    }
}
