using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletGenerator : NetworkBehaviour
{
    // public Transform parentToSet;
    //
    // public GameObject[] gameObjectPrefabs;
    //
    // [Server]
    // public void SpawnPistolBullet(uint owner, Vector2 position)
    // {
    //     GameObject prefab = Instantiate(gameObjectPrefabs[0], position, Quaternion.identity, parentToSet);
    //     NetworkServer.Spawn(prefab);
    //     //prefab.GetComponent<PistolBullet>().Init(owner);
    // }
}
