using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramController : MonoBehaviour
{
    public static ProgramController programController;

    Camera mainCamera;
    bool grabImage;

    float width;
    float height;
    public float drawBoundY;

    // Use this for initialization
    void Start()
    {
        if (programController != null)
            return;

        programController = this;
        mainCamera = Camera.main;

        height = mainCamera.orthographicSize * 2;
        width = mainCamera.aspect * height;
        drawBoundY = (int) mainCamera.transform.position.y + (height / 2) - width;

        DrawBound();
    }

    private void DrawBound()
    {
        GameObject go = new GameObject("Line");
        LineRenderer lineRenderer = go.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(-width / 2, drawBoundY - .05f, 0));
        lineRenderer.SetPosition(1, new Vector3(width / 2, drawBoundY - .05f, 0));
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = true;
    }

    private void Update()
    {
        //Press space to start the screen grab
        if (Input.GetKeyDown(KeyCode.Space))
            grabImage = true;
    }

    private void OnPostRender()
    {
        if (grabImage)
        {
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Screen.width, Screen.width, TextureFormat.RGB24, false);
            
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.width), 0, 0, false);
            texture.Apply();

            //Reset the grab state
            grabImage = false;

            //TextureScale.Bilinear(texture, 28, 28);

            byte[] bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes(@"D:\workspace\unity\image.png", bytes);
        }
    }
}
