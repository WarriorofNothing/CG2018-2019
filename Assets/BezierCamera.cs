using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class BezierCamera : MonoBehaviour {
    public Transform[] controlPoints;
    public LineRenderer lineRenderer;
    public GameObject myCamera;
    public GameObject myLight;

    private int curveCount = 0;
    private int layerOrder = 0;
    private int SEGMENT_COUNT = 100;

    private Vector3[] vectors;
    private int segmentPositionIndex = 0;


    void Start() {
        if (!lineRenderer) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
        curveCount = (int)controlPoints.Length / 3;
        vectors = new Vector3[curveCount * SEGMENT_COUNT + 1];

        DrawCurve();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void Update() {

        if (myCamera) {
            myCamera.transform.LookAt(new Vector3(0,0.04f,0));
            myLight.transform.LookAt(new Vector3(0, 0.04f, 0));

            if (Input.GetKey(KeyCode.RightArrow)) {
                //Clockwise
                segmentPositionIndex++;
                if (segmentPositionIndex >= vectors.Length - 1) {
                    segmentPositionIndex = 0;
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow)) {
                //Counterclockwise
                segmentPositionIndex--;
                if (segmentPositionIndex < 1) {
                    segmentPositionIndex = vectors.Length - 1;
                }
            }

            myCamera.transform.position = vectors[segmentPositionIndex];
            myLight.transform.position = vectors[segmentPositionIndex];
        }

    }

    void DrawCurve() {
        lineRenderer.SetPosition(0, controlPoints[0].position);
        vectors[0] = controlPoints[0].position;
        for (int j = 0; j < curveCount; j++) {
            for (int i = 1; i <= SEGMENT_COUNT; i++) {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 vector = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].position, controlPoints[nodeIndex + 1].position, controlPoints[nodeIndex + 2].position, controlPoints[nodeIndex + 3].position);
                lineRenderer.positionCount = j * SEGMENT_COUNT + i + 1;
                lineRenderer.SetPosition(j * SEGMENT_COUNT + i, vector);
                vectors[j * SEGMENT_COUNT + i] = vector;
            }
        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}

