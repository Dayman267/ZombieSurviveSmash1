using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDebager : MonoBehaviour
{
    // // void Update()
    // // {
    // //     if (Input.GetMouseButtonDown(0))
    // //     {
    // //         //Debug.Log("ssas");
    // //         Vector3 mousePosition = Input.mousePosition;
    // //
    // //         Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    // //
    // //         RaycastHit hit;
    // //         
    // //         if (Physics.Raycast(ray, out hit))
    // //         {
    // //             Color color = hit.collider.GetComponent<Renderer>().material.color;
    // //
    // //             Debug.Log("Цвет в точке клика: " + color);
    // //         }
    // //         else
    // //         {
    // //             Debug.Log("else");
    // //         }
    // //     }
    // // }
    //
    // // Set Renderer to a GameObject that has a Renderer component and a material that displays a texture
    // // private Renderer screenGrabRenderer;
    // //
    // // private Texture2D destinationTexture;
    // // private bool isPerformingScreenGrab;
    // //
    // // void Start()
    // // {
    // //     screenGrabRenderer = Physics2D.Raycast(Vector2.zero, Vector2.zero)
    // //         .collider
    // //         .gameObject
    // //         .GetComponent<SpriteRenderer>();
    // //     
    // //     // Create a new Texture2D with the width and height of the screen, and cache it for reuse
    // //     destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    // //
    // //     // Make screenGrabRenderer display the texture.
    // //     screenGrabRenderer.material.mainTexture = destinationTexture;
    // //
    // //     // Add the onPostRender callback
    // //     Camera.onPostRender += OnPostRenderCallback;
    // // }
    // //
    // // void Update()
    // // {
    // //     // When the user presses the Space key, perform the screen grab operation
    // //     if (Input.GetKeyDown(KeyCode.Space))
    // //     {
    // //         isPerformingScreenGrab = true;
    // //     }
    // // }
    // //
    // // void OnPostRenderCallback(Camera cam)
    // // {
    // //     if (isPerformingScreenGrab)
    // //     {
    // //         // Check whether the Camera that just finished rendering is the one you want to take a screen grab from
    // //         if (cam == Camera.main)
    // //         {
    // //             // Define the parameters for the ReadPixels operation
    // //             Rect regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
    // //             int xPosToWriteTo = Mathf.RoundToInt(Input.mousePosition.x);
    // //             int yPosToWriteTo = Mathf.RoundToInt(Input.mousePosition.y);
    // //             bool updateMipMapsAutomatically = false;
    // //
    // //             // Copy the pixels from the Camera's render target to the texture
    // //             destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);
    // //
    // //             // Upload texture data to the GPU, so the GPU renders the updated texture
    // //             // Note: This method is costly, and you should call it only when you need to
    // //             // If you do not intend to render the updated texture, there is no need to call this method at this point
    // //             destinationTexture.Apply();
    // //
    // //             // Reset the isPerformingScreenGrab state
    // //             isPerformingScreenGrab = false;
    // //         }
    // //     }
    // // }
    // //
    // // // Remove the onPostRender callback
    // // void OnDestroy()
    // // {
    // //     Camera.onPostRender -= OnPostRenderCallback;
    // // }
    //
    // public Camera cam; // наша камера
    // RenderTexture tex;
    // Texture2D _tex;
    //
    // void Start () {
    //     // Создаем изображение для "скриншота". 
    //     // Да, он всего в один пиксель размером - больше не надо.
    //     // Depth лучше на 0 не ставить - появляются различные баги.
    //     tex = new RenderTexture (1, 1, 8); 
    //     // RenderTexture "читать" нельзя, 
    //     // поэтому создаем текстуру, в которую его переводим.
    //     _tex = new Texture2D (1, 1, TextureFormat.RGB24, false);
    //     StartCoroutine(MaybeUpdate());
    // }
    //
    // // void Update () {
    // //     // назначаем текстуру "скриншота" камере
    // //     cam.targetTexture = tex; 
    // //     cam.Render ();
    // //     // делаем полученный скриншот активным
    // //     RenderTexture.active = tex;
    // //     // записываем в Texture2D
    // //     _tex.ReadPixels (new Rect (0, 0, 1, 1), 0, 0); 
    // //     _tex.Apply ();
    // //     Color col = _tex.GetPixel (0, 0);
    // //     float vis = (col.r + col.g + col.b) / 3;
    // // }
    //
    // private IEnumerator MaybeUpdate()
    // {
    //     // назначаем текстуру "скриншота" камере
    //     cam.targetTexture = tex; 
    //     cam.Render ();
    //     // делаем полученный скриншот активным
    //     RenderTexture.active = tex;
    //
    //     Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    //     // записываем в Texture2D
    //     _tex.ReadPixels (new Rect (mousePosition.x,mousePosition.y, 1, 1), 0, 0); 
    //     _tex.Apply();
    //     Color col = _tex.GetPixel (0, 0);
    //     float vis = (col.r + col.g + col.b) / 3;
    //     Debug.Log(vis);
    //     yield return new WaitForSeconds(2);
    // }
}
