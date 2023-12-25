using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletGenerator : NetworkBehaviour
{
    public GameObject shotgunPrefab;
    public float percentage = 0.1f;
    
    // [Server]
    // public void SpawnShotgunBullet(uint owner, Vector3 mousePos, Vector3 position)
    // {
    //     mousePos.x += Random.Range(0, mousePos.x * percentage);
    //     mousePos.y += Random.Range(0, mousePos.y * percentage);
    //     mousePos.z = 0;
    //     GameObject prefab = Instantiate(shotgunPrefab, position, Quaternion.identity);
    //     NetworkServer.Spawn(prefab);
    //     prefab.GetComponent<ShotgunBullet>().Init(owner, mousePos);
    // }
}
