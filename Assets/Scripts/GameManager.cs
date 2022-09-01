using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public Player player;

    public Sprite[] itemSprites;

    public ManagerUI managerUI;

    public int playerBalance = 999999;

    public void Start()
    {
        foreach (Sprite x in itemSprites)
        {
            managerUI.PopulatePanel(x, "shop");
        }
    }
    public void ChangeHair(string arg)
    {

        player.EquipHair(arg);
    }
}
