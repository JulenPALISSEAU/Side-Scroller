using UnityEngine;

public class UICombatSystemController : MonoBehaviour
{
    public Transform Follow;

        private Camera MainCamera;

        public int OffsetX;
        public int OffsetY;

    // Start is called before the first frame update
    void Start()
        {
            MainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            var screenPos = MainCamera.WorldToScreenPoint(Follow.position);

        //transform.position = new Vector3(screenPos.x, screenPos.y + 150, screenPos.z);
        transform.position = new Vector3(screenPos.x + OffsetX, screenPos.y + OffsetY, 0);
    }
}