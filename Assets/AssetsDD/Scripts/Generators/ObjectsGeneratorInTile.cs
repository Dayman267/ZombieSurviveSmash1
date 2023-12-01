using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ObjectsGeneratorInTile : NetworkBehaviour
{
    private ObjectsGenerator objectsGenerator;
    
    public void Start()
    {
        if (!isServer) return;
        objectsGenerator = GameObject.
            FindWithTag("ObjectsGenerator").
            GetComponent<ObjectsGenerator>();
        CmdGenerateObjects(transform.position.x, transform.position.y);
        Destroy(this);
    }

    void CmdGenerateObjects(float startX, float startY)
    {
        objectsGenerator.GenerateObjects(startX, startY);
    }
}
