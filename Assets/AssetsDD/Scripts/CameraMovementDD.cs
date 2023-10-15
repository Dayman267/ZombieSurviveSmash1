using UnityEngine;
using Mirror;

public class CameraMovementDD : NetworkBehaviour
{
    public Transform player;

    private void Update()
    {
        if (isLocalPlayer)
        {
            transform.position = player.position;
        }
    }
}
