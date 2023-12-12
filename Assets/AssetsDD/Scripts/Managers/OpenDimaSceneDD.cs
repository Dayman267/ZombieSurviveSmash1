using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class OpenDimaSceneDD : NetworkBehaviour
{
    public void Transition()
    {
        SceneManager.LoadScene("DimaSceneDD");
    }
}