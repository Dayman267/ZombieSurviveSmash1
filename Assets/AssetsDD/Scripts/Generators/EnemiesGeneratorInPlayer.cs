using System.Collections;
using UnityEngine;
using Mirror;

public class EnemiesGeneratorInPlayer : NetworkBehaviour
{
    private ObjectsGenerator objectsGenerator;
    
    [SerializeField] private int waitForSecondsBetweenSpawning = 2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
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
