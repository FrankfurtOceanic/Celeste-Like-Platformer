using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineRendererController : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }   
    }
}
