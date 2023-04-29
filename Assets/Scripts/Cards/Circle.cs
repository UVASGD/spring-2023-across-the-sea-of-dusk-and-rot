using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://stackoverflow.com/questions/13708395/how-can-i-draw-a-circle-in-unity3d
public class Circle : MonoBehaviour
{

    public float thetaScale = 0.01f;
    public float radius = 15;
    public float yOffset = 18.0f;
    private int numPoints;
    private LineRenderer lineDrawer;
    private float theta = 0f;

    public List<Vector3>circlePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        lineDrawer = GetComponent<LineRenderer>();
        createCirclePoints();
    }

    void createCirclePoints(){
        theta = 0f;
        numPoints = (int)((1f / thetaScale) + 1f);
        // lineDrawer.positionCount = numPoints;
        for (int i = 0; i < numPoints; i++) {
            theta += (2.0f * Mathf.PI * thetaScale);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta) - yOffset;
            // lineDrawer.SetPosition(i, new Vector3(x, y, 0));

            Vector3 point = new Vector3(x, y, theta);
            circlePoints.Add(point);
        }

    }

    public List<Vector3> getCirclePoints(){
        return circlePoints;
    }

    public float getCircleCenter(){
        // return Camera.main.ScreenToWorldPoint(new Vector3(0, -yOffset, 0)).y;
        return -yOffset;
    }

    public float getCircleRadius(){
        // return Camera.main.ScreenToWorldPoint(new Vector3(radius, 0, 0)).x;
        return radius;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
