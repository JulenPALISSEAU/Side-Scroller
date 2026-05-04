using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Player Modifier")]
    public Rigidbody2D rbPlayer;

    [Header("Player Stats")]
    float speed = 4.5f;
    float jumpForce = 5.5f;
    bool isBlocked = false;
    int deathCount = 0;
    int spamKeyMeter = 0;

    [Header("CheckerGrounded")]
    bool isGroundedLeft;
    bool isGroundedRight;

    public Vector2 moveInput;
    public Vector2 LastSafeSpot;
    public InputActionReference move;
    public InputActionReference jump;
    public LayerMask GroundLayer;
    public LayerMask HoleLayer;

    public InputActionReference test;

    void Test(InputAction.CallbackContext obj)
    {
        // Bloque le joueur, augmente le compteur de mort du joueur et détermine le nombre de fois le joueur dois presser la touche de saut pour ce débloquer
        isBlocked = true;
        deathCount++;
        spamKeyMeter += deathCount * 5;
    }
        
    void Jump(InputAction.CallbackContext obj)
    {
        // Vérifie si le joueur est bloquer puis effectue saute
        if (!isBlocked && (isGroundedLeft || isGroundedRight)) {
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, jumpForce);
        }
        // Sinon vérifie si le joueur a suffisamment taper la touche de saut pour se débloquer. Si c'est le cas il est débloquer, sinon le compteur diminue de 1
        else {
            if (spamKeyMeter <= 1)
            {
                spamKeyMeter = 0;
                isBlocked = false;
            }
            else
            {
                spamKeyMeter--;
            }
        }
    }

    IEnumerator playerDeath()
    {
        // Bloque le joueur, augmente son compteur de mort, défini le nombre de fois le joueur doit spam une touche pour ce libérer
        isBlocked = true;
        deathCount++;
        spamKeyMeter += deathCount * 5;

        // Repositionne le joueur à son dernier emplacement safe est retire l'état qui indique qu'il est tombé
        transform.position = LastSafeSpot;
        bool haveFallen = false;

        // Attends une demi seconde pour éviter de compter plusieurs morts
        yield return new WaitForSeconds(0.5f);
    }

    private void OnEnable()
    {
        test.action.started += Test;

        jump.action.started += Jump;
    }

    private void OnDisable()
    {
        test.action.started -= Test;

        jump.action.started -= Jump;
    }

    // Vérifie si le joueur à touché le bord gauche de l'écran et ---
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeathWall")
        {
            Debug.Log("Patate");
        }
    }

    void Update() {
        // Sauvegarde la pression des touches/du joystick pour se déplacer dans un Vector2
        moveInput = move.action.ReadValue<Vector2>();



        // Vérifie avec 2 Raycasts sur les bords inférieurs droite et gauche du personnage joueur si celui-ci touche le sol
        Vector2 RaycastGroundedLeft = new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f);
        Vector2 RaycastGroundedRight = new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f);
        isGroundedLeft = Physics2D.Raycast(RaycastGroundedLeft, Vector2.down, 0.1f, GroundLayer);
        isGroundedRight = Physics2D.Raycast(RaycastGroundedRight, Vector2.down, 0.1f, GroundLayer);

        // Définie le dernier emplacement "safe" sur lequel le joueur était et la sauvegarde
        if (isGroundedLeft && isGroundedRight)
        {
            LastSafeSpot = transform.position;
        }

        // Vérifie et sauvegarde si le personnage joueur est rentrer en contact avec l'élèment qui sert de vide
        bool haveFallen = this.GetComponent<Collider2D>().IsTouchingLayers(HoleLayer);

        // Vérifie si le joueur est tombé et si celui-ci est n'est pas bloqué. Si c'est le cas, le joueur est repositioné à son dernier emplacement sécuriser s'il est tombé dans le vide avant de le mettre KO
        if (haveFallen) if (!isBlocked) StartCoroutine(playerDeath());
    }

    void FixedUpdate() {
        // Si le joueur n'est pas bloquer, il est 
        if (!isBlocked) {
            rbPlayer.linearVelocity = new Vector2(moveInput.x * speed, rbPlayer.linearVelocityY);
        }
    }
}
