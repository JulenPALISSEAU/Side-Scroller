using UnityEngine;

public class CharacterController : MonoBehaviour
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
    bool touchingPlatformSide;

    void Awake() {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //bool (Collider2D Hole);
        //public bool IsTouchingLayers(int layerMask = Physics2D.AllLayers);

        VectorInput = Input.GetAxisRaw("Horizontal");
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, GroundLayer);
        if (isGrounded)
        {
            LastSafeSpot = transform.position;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, jumpForce);
        }

        bool touchingPlatformSide = Physics2D.Raycast(transform.position.x - 0.5f, Vector2.left, 0.6f, GroundLayer);
        bool touchingPlatformSide = Physics2D.Raycast(position, Vector2.left, 0.6f, GroundLayer);
        bool touchingPlatformSide = Physics2D.Raycast(position, Vector2.right, 0.6f, GroundLayer);
        bool touchingPlatformSide = Physics2D.Raycast(position, Vector2.right, 0.6f, GroundLayer);


        bool haveFallen = this.GetComponent<Collider2D>().IsTouchingLayers(HoleLayer);
        if (haveFallen)
        {
            transform.position = LastSafeSpot;
        }
    }

    private void FixedUpdate() {
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