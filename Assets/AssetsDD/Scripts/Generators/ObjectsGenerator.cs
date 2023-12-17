using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering.Universal;

public class ObjectsGenerator : NetworkBehaviour
{
    public GameObject parentToSet;

    public GameObject sizeTemplate;
    private float objectSize;
    
    public GameObject[] gameObjectPrefabs;
    public GameObject[] enemiesPrefabs;
    
    [SerializeField] private int minObjectsPerTile = 1;
    [SerializeField] private int maxObjectsPerTile = 1;
    [SerializeField] private int minEnemiesPerSpawn = 1;
    [SerializeField] private int maxEnemiesPerSpawn = 1;
    
    [SerializeField] private float maxCubeRadius = 5;
    
    public void Start()
    {
        if (isServer)
        {
            objectSize = sizeTemplate.transform.localScale.x/2;
        }
    }

    [Server]
    public void GenerateObjects(float startX, float startY)
    {
        for(int i = Random.Range(minObjectsPerTile, maxObjectsPerTile+1), k = 0; k<i; k++)
        {
            float randPositionX = Random.Range(-objectSize, objectSize+1);
            float randPositionY = Random.Range(-objectSize, objectSize+1);
            Vector3 position = new Vector3(startX + randPositionX, startY + randPositionY, 0);
            GameObject prefab = Instantiate(
                gameObjectPrefabs[Random.Range(0, gameObjectPrefabs.Length)], 
                position,
                Quaternion.identity,
                parentToSet.transform);
            NetworkServer.Spawn(prefab);
        }
    }

    [Server]
    public void GenerateEnemiesAroundPlayer(Vector2 playerPosition)
    {
        for(int i = Random.Range(minEnemiesPerSpawn, maxEnemiesPerSpawn+1), k = 0; k<i; k++)
        {
            float randPositionX = Random.Range(-maxCubeRadius, maxCubeRadius+1);
            float randPositionY = Random.Range(-maxCubeRadius, maxCubeRadius+1);
            Vector3 position = new Vector3(playerPosition.x + randPositionX, playerPosition.y + randPositionY, 0);
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero);
            bool isHas = false;
            foreach (var hit in hits) if (hit.collider.gameObject.GetComponent<Light2D>()) isHas = true;
            if (isHas) continue;
            GameObject prefab = Instantiate(
                enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], 
                position,
                Quaternion.identity,
                parentToSet.transform);
            NetworkServer.Spawn(prefab);
        }
    }
}
