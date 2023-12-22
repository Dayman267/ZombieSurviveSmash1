using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    public InputField inputField;
    public NetworkManager networkManager;
 
    private void Start()
    {
        /*if (!Application.isBatchMode)
        {
            networkManager.StartClient();
        }*/
    }


    public void JoinClient()
    {
        if (inputField.text == "" || inputField.text == "Enter IP address")
        {
            networkManager.networkAddress = "localhost";
        }
        else
        {
            networkManager.networkAddress = inputField.text;
        }

        networkManager.StartClient();
    }
}