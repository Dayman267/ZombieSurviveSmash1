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
        objectsGenerator.GenerateObjects(transform.position.x, transform.position.y);
        Destroy(this);
    }
}
