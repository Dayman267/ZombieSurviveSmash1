using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class DeleteLighter : NetworkBehaviour
{
    [SerializeField] private string sceneName = "DimaSceneDD";
    void Start()
    {
        if(SceneManager.GetActiveScene().name != sceneName) Destroy(gameObject);
    }
}
