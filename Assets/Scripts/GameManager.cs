using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    public void ChangeHair(string arg)
    {

        player.EquipHair(arg, "Hair");
    }
}
