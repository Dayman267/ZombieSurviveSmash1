using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : MonoBehaviour
{
    private List<TestPlayer> players = new List<TestPlayer>();

    public void AddPlayer(TestPlayer player)
    {
        players.Add(player);
    }
}