using UnityEngine;
using Mirror;

public class Pistol : NetworkBehaviour
{
    public GameObject bullet;

    private float secLeft;
    public float secBetweenShots = 0.3f;

    private void Update()
    {
        if(!isLocalPlayer) return;
        
        if (secLeft <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                CmdSpawnBullet(netId, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                secLeft = secBetweenShots;
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
        bulletGo.GetComponent<PistolBullet>().Init(owner, mousePos);
    }
}