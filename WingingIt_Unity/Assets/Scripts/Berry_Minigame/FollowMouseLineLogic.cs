using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseLineLogic : MonoBehaviour
{
    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 endOfTheLinePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endOfTheLinePosition.y = 0;
        lineRenderer.SetPosition(1, endOfTheLinePosition);
    }
}
