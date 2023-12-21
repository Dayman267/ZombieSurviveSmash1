using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class TestPlayer : NetworkBehaviour
{
    public static TestPlayer localPlayer;
    [SyncVar] public string matchID;

    private NetworkMatch networkMatch;
    
    private void Start()
    {
        networkMatch = GetComponent<NetworkMatch>();

        if(isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            TestMainMenu.instance.SpawnPlayerUIPrefab(this);
        }
    }
    
    public void HostGame()
    {
        string ID = TestMainMenu.GetRandomID();
        CmdHostGame(ID);
    }
    
    [Command]
    public void CmdHostGame(string ID)
    {
        matchID = ID;
        if(TestMainMenu.instance.HostGame(ID, gameObject))
        {
            Debug.Log("Лобби было создано успешно");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("Ошибка в создании лобби");
            TargetHostGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        TestMainMenu.instance.HostSuccess(success, ID);
    }

    public void JoinGame(string inputID)
    {
        CmdJoinGame(inputID);
    }

    [Command]
    public void CmdJoinGame(string ID)
    {
        matchID = ID;
        if (TestMainMenu.instance.JoinGame(ID, gameObject))
        {
            Debug.Log("Успешное подключение к лобби");
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }
        else
        {
            Debug.Log("Не удалось подключиться");
            TargetJoinGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID} == {ID}");
        TestMainMenu.instance.JoinSuccess(success, ID);
    }

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    public void CmdBeginGame()
    {
        TestMainMenu.instance.BeginGame(matchID);
        Debug.Log("Игра начилась");
    }

    public void StartGame()
    {
        TargetBeginGame();
    }

    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"ID {matchID} | начало");
        DontDestroyOnLoad(gameObject);
        TestMainMenu.instance.inGame = true;
        transform.localScale = new Vector3(0.6575683f, 0.6575683f, 0.6575683f); //Размер вашего игрока (x, y, z)
        SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
    }
    
    private void Update()
    {
        if (hasAuthority)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float speed = 6f * Time.deltaTime;
            transform.Translate(new Vector2(horizontal * speed, vertical * speed));
        }
    }
}
