using UnityEngine;
using Mirror;

public class Shotgun : NetworkBehaviour
{
    public GameObject bullet;
    //private BulletGenerator generator;
    private float secLeft;
    public float secBetweenShots = 0.1f;
    public int bulletsCount = 5;

    private void Start()
    {
        if(!isLocalPlayer) return;
        //generator = GameObject.FindWithTag("BulletGenerator").GetComponent<BulletGenerator>();
    }

    private void Update()
    {
        if(!isLocalPlayer) return;
        
        if (secLeft <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < bulletsCount; i++)
                {
                    CmdSpawnBullet(netId, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    secLeft = secBetweenShots;
                }
            }
        }
        else
        {
            secLeft -= Time.deltaTime;
        }
    }
    
    [Command]
    public void CmdSpawnBullet(uint owner, Vector3 mousePos)
    {
        GameObject bulletGo = Instantiate(bullet, transform.position, Quaternion.identity);
        NetworkServer.Spawn(bulletGo);
        bulletGo.GetComponent<ShotgunBullet>().Init(owner, mousePos);
    }
}
