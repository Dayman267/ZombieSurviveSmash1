using UnityEngine;

public class FiveSecDestroyer : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}