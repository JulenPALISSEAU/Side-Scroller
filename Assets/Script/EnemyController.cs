using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController Chara;
    int Type = 0;
    int enemyHP = 2;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Chara.InitializeCombatStart();
        }
    }

    void Update()
    {
        
    }
}