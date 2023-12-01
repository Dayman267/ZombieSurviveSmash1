using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ObjectsGenerator : NetworkBehaviour
{
    public GameObject parentToSet;
    
    public GameObject[] gameObjectPrefabs;
    
    private int minObjectsPerTile = 1;
    private int maxObjectsPerTile = 6;

    private float objectSize;
    
    public void Start()
    {
        if (isServer)
        {
            objectSize = gameObjectPrefabs[0].transform.localScale.x;
        }
    }

    [Server]
    public void GenerateObjects(float startX, float startY)
    {
        for(int i = Random.Range(minObjectsPerTile, maxObjectsPerTile+1), k = 0; k<i; k++)
        {
            float randPositionX = Random.Range(-objectSize-1, objectSize+2);
            float randPositionY = Random.Range(-objectSize-1, objectSize+2);
            Vector3 position = new Vector3(startX + randPositionX, startY + randPositionY, 0);
            GameObject prefab = Instantiate(
                gameObjectPrefabs[Random.Range(0, gameObjectPrefabs.Length)], 
                position,
                Quaternion.identity,
                parentToSet.transform);
            //RpcSpawn(prefab);
            NetworkServer.Spawn(prefab);
        }
    }
    
    // [ClientRpc]
    // public void RpcSpawn(GameObject prefab)
    // {
    //     NetworkServer.Spawn(prefab);
    // }
}
