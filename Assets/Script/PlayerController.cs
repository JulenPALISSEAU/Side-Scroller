using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;

    [Header("Gravity / Jump")]
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpForce = 5f;

    Rigidbody2D Rigidbody;
    float VectorInput;
    public LayerMask GroundLayer;
    public LayerMask HoleLayer;
    Vector2 LastSafeSpot;

    InputSystemActions PlayerInputActions;
    InputAction Move;
    InputAction Jump;

    void Awake() {
        // Récupère le RigideBody de l'élèment pour l'utiliser plus tard plus facilement
        Rigidbody = GetComponent<Rigidbody2D>();

        PlayerInputActions = new InputSystemActions();
    }

    private void OnEnable()
    {
        Move = PlayerInputActions.Player.Move;
        Move.Enable();
        Debug.Log(Move);

        Jump = PlayerInputActions.Player.Jump;
        Jump.Enable();
        Debug.Log(Jump);
    }

    void Update()
    {
        // Récupère l'input du joueur pour le déplacement
        VectorInput = Input.GetAxisRaw("Horizontal");

        // Vérifie avec 2 Raycasts sur les bords inférieurs droite et gauche du personnage joueur si celui-ci touche le sol
        Vector2 RaycastGroundedLeft = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        Vector2 RaycastGroundedRight = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f);
        bool isGroundedLeft = Physics2D.Raycast(RaycastGroundedLeft, Vector2.down, 0.1f, GroundLayer);
        bool isGroundedRight = Physics2D.Raycast(RaycastGroundedRight, Vector2.down, 0.1f, GroundLayer);
        
        // Définie le dernier emplacement "safe" sur lequel le joueur était et la sauvegarde
        if (isGroundedLeft || isGroundedRight)
        {
            LastSafeSpot = transform.position;
        }
        
        // Effectue le saut du personnage joueur si la touche et presser et si l'un des Raycasts qui vérifie si le joueur touche le sol et positif
        if (Input.GetButtonDown("Jump") && (isGroundedLeft || isGroundedRight))
        {
            Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, jumpForce);
        }

        // Vérifie et sauvegarde si le personnage joueur est rentrer en contact avec l'élèment qui sert de vide
        bool haveFallen = this.GetComponent<Collider2D>().IsTouchingLayers(HoleLayer);

        // Repositionne le joueur à son dernier emplacement sécuriser s'il est tombé dans le vide
        if (haveFallen)
        {
            transform.position = LastSafeSpot;
        }
    }

    private void FixedUpdate() {

        //
        var v = Rigidbody.linearVelocity;
        v.x = VectorInput * moveSpeed;

        Rigidbody.linearVelocity = v;
    }
}
















//V1 - Legacy Controls

//public class CharacterController : MonoBehaviour
//{
//    [Header("Move variables")]
//    [SerializeField] float moveSpeed = 5f;
//    [SerializeField] float acceleration = 20f;

////    [Header("Gravity / Jump")]
////    [SerializeField] float gravity = -10f;
////    [SerializeField] float jumpHeight = 1.5f;

////   Rigidbody2D Rigidbody;
//    Vector2 input;

//    void Awake()
//    {
//        Rigidbody = GetComponent<Rigidbody2D>();
//    }

//    void Update()
//    {
//        input = new Vector2();
//        input.Normalize();
//    }

//    private void FixedUpdate()
//{
//    Rigidbody.linearVelocity = input * moveSpeed;
//}
//}




//V2 - Legacy Controls