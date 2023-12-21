using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text PlayerText;
    private TestPlayer player;

    public void SetPlayer(TestPlayer player)
    {
        this.player = player;
        PlayerText.text = "Имя";
    }
}