using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcArrow : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lineRenderer;

    public int segments;

    public float angle;
    public float velocity;
    public float gravity = 9.8f;
    private float radianAngle;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void renderArc()
    {
        if (segments > 0)
        {
            lineRenderer.positionCount = segments + 1;
            lineRenderer.SetPositions(CalculateArcArray());
        }

    }

    private Vector3[] CalculateArcArray()
    {
        Vector3[] arcArr = new Vector3[segments + 1];
        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (float)(velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;
        for (int i = 0; i <= segments; i++)
        {
            float t = (float)i / (float)segments;
            arcArr[i] = calculateArcPoint(t, maxDistance);
        }

        return arcArr;
    }

    private Vector3 calculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        renderArc();
    }
}
