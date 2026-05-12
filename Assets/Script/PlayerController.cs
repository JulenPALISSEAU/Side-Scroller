using System.Collections;
using Unity.Multiplayer.PlayMode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Modifier")]
    public Rigidbody2D rbPlayer;

    [Header("Player Stats")]
    float speed = 4.5f;
    float jumpForce = 5.5f;
    bool isBlocked = false;
    public bool isFighting = false;
    public bool isPaused = false;
    int deathCount = 0;
    int spamKeyMeter = 0;
    int necessaryEXP;
    public int maxPlayerHP;
    public int currentPlayerHP;
    public int maxPlayerMP;
    public int currentPlayerMP;

    [Header("CheckerGrounded")]
    bool isGroundedLeft;
    bool isGroundedRight;

    [Header("")]
    public Vector2 moveInput;
    public Vector2 LastSafeSpot;
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference pause;
    public LayerMask GroundLayer;
    public LayerMask HoleLayer;

    [Header("UI Elements")]
    public GameObject MenuCombatUI;
    public Button AttackButton;
    public Button MagicAButton;

    public GameObject PauseMenuUI;
    public Button ResumeButton;
    

    public InputActionReference test;

    void Test(InputAction.CallbackContext obj)
    {
        //
        

        // Bloque le personnage joueur et active l'interface de combat
        if (!isPaused)
        {
            isFighting = true;
            MenuCombatUI.SetActive(true);
            AttackButton.Select();
        }
    }
        
    void Jump(InputAction.CallbackContext obj)
    {
        // Vérifie si le joueur n'est pas bloquer ou en combat puis effectue saute
        if (!isBlocked && !isFighting && (isGroundedLeft || isGroundedRight)) {
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

    void Pause(InputAction.CallbackContext obj)
    {
        // Met le jeu en pause
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            PauseMenuUI.SetActive(true);
            ResumeButton.Select();



            // ATTEMPT TO MAKE A SLOWDOWN THE GAME BEFORE PAUSING IT INSTEAD OF BEING INSTANT
            //for (int i = 1; Time.timeScale <= 0f; i++)
            //{
            //    Time.timeScale -= 0.1f;
            //    if (Time.timeScale <= 0f)
            //    {
            //        Time.timeScale = 0f;
            //    }
            //}
            //isPaused = true;
            //AudioListener.pause = true;
            //Debug.Log(isPaused + " " + Time.timeScale);
        }
        // Retire la pause
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            PauseMenuUI.SetActive(false);



            // ATTEMPT TO MAKE A SLOWDOWN THE GAME BEFORE PAUSING IT INSTEAD OF BEING INSTANT
            //for (int i = 1; Time.timeScale >= 1; i++)
            //{
            //  Time.timeScale += 0.1f;
            //}
            //if (Time.timeScale >= 1)
            //{
            //  Time.timeScale = 1;
            //  isPaused = false;
            //  AudioListener.pause = false;
            //}
        }
    }

    void InitializeCombatStats()
    {
        switch (PlayerPrefs.GetInt("playerLevel"))
        {
            case 1:
                necessaryEXP = 2;
                maxPlayerHP = 3;
                currentPlayerHP = maxPlayerHP;
                maxPlayerMP = 1;
                currentPlayerMP = maxPlayerMP;
                break;
            case 2:
                necessaryEXP = 5;
                maxPlayerHP = 4;
                currentPlayerHP = maxPlayerHP;
                maxPlayerMP = 2;
                currentPlayerMP = maxPlayerMP;
                break;
            case 3:
                necessaryEXP = 8;
                maxPlayerHP = 5;
                currentPlayerHP = maxPlayerHP;
                maxPlayerMP = 3;
                currentPlayerMP = maxPlayerMP;
                break;
            case 4:
                necessaryEXP = 15;
                maxPlayerHP = 7;
                currentPlayerHP = maxPlayerHP;
                maxPlayerMP = 4;
                currentPlayerMP = maxPlayerMP;
                break;
            default:
                necessaryEXP = 99999999;
                maxPlayerHP = 10;
                currentPlayerHP = maxPlayerHP;
                maxPlayerMP = 5;
                currentPlayerMP = maxPlayerMP;
                break;
        }
    }

    public void InitializeCombatStart()
    {

        // OSKOUR

        Debug.Log("FightBegin");
        Vector2 currentPosition = transform.position;
        if (moveInput.x < 0)
        {
            transform.position = new Vector2(currentPosition.x + 3f, currentPosition.y);
            Debug.Log("IsMoved");
        }
        else
        {
            transform.position = new Vector2(currentPosition.x - 3f, currentPosition.y);
        }
        isFighting = true;
        MenuCombatUI.SetActive(true);
        AttackButton.Select();
    }

    void EndCombat()
    {
        if (PlayerPrefs.GetInt("currentEXP") >= necessaryEXP)
        {
            PlayerPrefs.SetInt("playerLevel", (PlayerPrefs.GetInt("playerLevel") + 1));
        }
        else
        {
            PlayerPrefs.SetInt("currentEXP", (PlayerPrefs.GetInt("currentEXP") + 1));
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
        pause.action.started += Pause;
    }

    private void OnDisable()
    {
        test.action.started -= Test;

        jump.action.started -= Jump;
        pause.action.started -= Pause;
    }

    void Awake() {
        InitializeCombatStats();
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
        if (!isBlocked && !isFighting) {
            rbPlayer.linearVelocity = new Vector2(moveInput.x * speed, rbPlayer.linearVelocityY);
        }
        else
        {
            rbPlayer.linearVelocity = new Vector2(0, rbPlayer.linearVelocityY);
        }
    }
}
