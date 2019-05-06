using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager
{
    private LineRenderer lineInstance;

    public LineManager()
    {
        lineInstance = GameManager.Instance.lineRenderer;
    }

    public void SetDrawing(bool enabled)
    {
        lineInstance.enabled = enabled;
    }

    public void SetupLinePoints(Vector3[] points)
    {
        lineInstance.SetPositions(points);
    }
}
