using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class EnemiesGeneratorInPlayer : NetworkBehaviour
{
    private ObjectsGenerator objectsGenerator;
    [SerializeField] private string sceneName = "DimaSceneDD";
    
    [SerializeField] private int waitForSecondsBetweenSpawning = 2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != sceneName)
        {
            Destroy(this);
            return;
        }
        
        objectsGenerator = GameObject
            .FindWithTag("ObjectsGenerator")
            .GetComponent<ObjectsGenerator>();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitForSecondsBetweenSpawning);
            objectsGenerator.GenerateEnemiesAroundPlayer(transform.position);
        }
    }
}
