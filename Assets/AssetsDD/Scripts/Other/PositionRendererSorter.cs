using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PositionRendererSorter : NetworkBehaviour
{
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField] private float offset = 0;
    [SerializeField] private bool runOnlyOnce = false;
    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - transform.position.y*10 + offset);
        if (runOnlyOnce)
        {
            Destroy(this);
        }
    }
}
