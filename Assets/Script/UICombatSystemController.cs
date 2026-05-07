using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICombatSystemController : MonoBehaviour
{
    public PlayerController Chara;

    [Header("Menu UI")]
    public GameObject MenuCombatUI;
    public GameObject MagieUI;

    [Header("Boutons")]
    public Button attackButton;
    public Button magicButton;
    public Button magicAButton;
    public Button magicBButton;
    public Button magicCButton;
    public Button magicDButton;
    public Button magicEButton;

    // -------Attaque & Magie----------
    public void Attack(){
        MenuCombatUI.SetActive(false);
        Chara.isFighting = false;
    }

    public void Magic()
    {
        MagieUI.SetActive(true);
        magicAButton.Select();
    }

    // -------Attaques Magiques----------
    public void MagicA()
    {
        MagieUI.SetActive(false);
        attackButton.Select();
    }
    public void MagicB()
    {
        MagieUI.SetActive(false);
        attackButton.Select();
    }
    public void MagicC()
    {
        MagieUI.SetActive(false);
        attackButton.Select();
    }
    public void MagicD()
    {
        MagieUI.SetActive(false);
        attackButton.Select();
    }
    public void MagicE()
    {
        MagieUI.SetActive(false);
        attackButton.Select();
    }
}