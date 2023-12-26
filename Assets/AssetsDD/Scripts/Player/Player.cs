using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public GameObject BulletPrefab;
    
    void Update()
    {
        if (isOwned) //проверяем, есть ли у нас права изменять этот объект
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 pos = Input.mousePosition;
                pos.z = 10f;
                pos = Camera.main.ScreenToWorldPoint(pos);
    
                if (isServer)
                    SpawnBullet(netId, pos);
                else
                    CmdSpawnBullet(netId, pos);
            }
        }
    }
    
    [Server]
    public void SpawnBullet(uint owner, Vector3 target)
    {
        GameObject bulletGo = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(bulletGo);
        bulletGo.GetComponent<BulletDD>().Init(owner, target); //инифиализируем поведение пули
    }
    
    
    [Command]
    public void CmdSpawnBullet(uint owner, Vector3 target)
    {
        SpawnBullet(owner, target);
    }
}