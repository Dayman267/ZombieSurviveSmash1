using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        for(int i = Random.Range(5, 50), k = 0; k<i; k++)
        {
            float randPositionX = Random.Range(-12, 12);
            float randPositionY = Random.Range(-12, 12);
            Vector3 position = new Vector3(transform.position.x + randPositionX, transform.position.y + randPositionY, 0);
            Instantiate(objects[Random.Range(0, objects.Length)], position, Quaternion.identity);
        }
    }
}
