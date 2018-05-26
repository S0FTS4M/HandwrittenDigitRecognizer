using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine2D : MonoBehaviour
{
    private Camera mainCamera;
    private LineRenderer lineRenderer;
	private List<Vector2> points;

	public LineRenderer LineRenderer
	{
		get
		{
			return lineRenderer;
		}
	}

	public List<Vector2> Points
	{
		get
		{
			return points;
		}
	}

	private void Awake ()
	{
		if ( lineRenderer == null )
		{
			Debug.LogWarning ( "DrawLine: Line Renderer not assigned, Adding and Using default Line Renderer." );
			CreateDefaultLineRenderer ();
		}

		if ( mainCamera == null ) {
			mainCamera = Camera.main;
		}

		points = new List<Vector2> ();
	}

	private void Update ()
	{
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
        }

        if ( Input.GetMouseButton ( 0 ) )
		{
			Vector2 mousePosition = mainCamera.ScreenToWorldPoint ( Input.mousePosition );
			Vector2 lineRendererPosition = lineRenderer.transform.position;

			if ( !points.Contains ( mousePosition ) && mousePosition.y > ProgramController.programController.drawBoundY + lineRenderer.startWidth / 2 )
			{
				points.Add ( mousePosition );
				lineRenderer.positionCount = points.Count;
				lineRenderer.SetPosition ( lineRenderer.positionCount - 1, mousePosition);
			}
		}

	}

	public void Reset ()
	{
		if ( lineRenderer != null )
		{
			lineRenderer.positionCount = 0;
		}
		if ( points != null )
		{
			points.Clear ();
		}
	}

	private void CreateDefaultLineRenderer ()
	{
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.positionCount = 0;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startColor = Color.white;
		lineRenderer.endColor = Color.white;
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.useWorldSpace = true;
	}
}