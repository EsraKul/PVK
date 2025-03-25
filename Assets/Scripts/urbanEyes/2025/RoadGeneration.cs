using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
    Generate road between two points, how
 */
public class RoadGeneration : MonoBehaviour {
    public GameObject RoadPrefab;
    public GameObject stripePrefab;
    public GameObject sideWalkPrefab;
    public Vector3 pointA = new Vector3(0, 0, 0);
    public Vector3 pointB = new Vector3(10, 10, 10);


    private float roadLen;//= 110.192f;
    private float roadWidth;
    private float swLen;
    private float swWidth;
    // Start is called before the first frame update
    void Start() {
        roadLen = Vector3.Dot(RoadPrefab.GetComponent<Renderer>().bounds.size, RoadPrefab.transform.localRotation * Vector3.right);
        roadWidth = Vector3.Dot(RoadPrefab.GetComponent<Renderer>().bounds.size, RoadPrefab.transform.localRotation * Vector3.forward);
        swLen = Vector3.Dot(sideWalkPrefab.GetComponent<Renderer>().bounds.size, sideWalkPrefab.transform.localRotation * Vector3.forward);
        swWidth = Vector3.Dot(sideWalkPrefab.GetComponent<Renderer>().bounds.size, sideWalkPrefab.transform.localRotation * Vector3.right);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            ScaleRoad();
    }
    public float remainingDistance = 0.5f * 110.192f - 12;
    private void ScaleRoad() {

        float dist = Vector3.Distance(pointA, pointB);
        Vector3 direction = (pointB - pointA).normalized;
        gameObject.transform.rotation = Quaternion.LookRotation(direction); // this might make Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0) unnecesary


        float numRoads = Mathf.Abs(Mathf.FloorToInt(dist / roadLen));

        Vector3 startPos = pointA + direction * (roadLen / 2);

        for (int i = 0; i < numRoads; i++) {
            Vector3 spawnPos = startPos + direction * roadLen * i;
            GameObject roadSegment = Instantiate(RoadPrefab, spawnPos, Quaternion.identity);
            roadSegment.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0);
            roadSegment.transform.SetParent(transform);
        }

        // NOTE: this would be necessary if the disance isn't a multiple of roadlen but if we use grids I can just set roadlen to fit in a grid

        // float remainingDistance = Vector3.Distance(pointA + direction * numRoads * roadLen, pointB);
        /*        float remainingDistance = dist - roadLen * numRoads;
               if (remainingDistance > 0) {
                   // starting position of the last road segment
                   Vector3 lastPos = startPos + direction * roadLen * numRoads;
                   GameObject lastRoad = Instantiate(RoadPrefab, lastPos, Quaternion.identity);
                   lastRoad.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0);
                   lastRoad.transform.SetParent(transform);

                   Vector3 scale = lastRoad.transform.localScale;
                   scale.x = remainingDistance / roadLen;
                   lastRoad.transform.localScale = scale;


               } */
        SideWalk();
        PlaceStripes(numRoads * roadLen, direction);
    }
    void SideWalk() {
        float dist = Vector3.Distance(pointA, pointB);
        Vector3 direction = (pointB - pointA).normalized;


        float numSegs = Mathf.Abs(Mathf.FloorToInt(dist / swLen));
        Debug.Log(roadWidth);
        for (int j = -1; j < 2; j += 2) {
            Vector3 startPos = pointA + direction * (swLen / 2) + j * gameObject.transform.right * (roadWidth / 2 - swWidth / 2);

            for (int i = 0; i < numSegs; i++) {
                Vector3 spawnPos = startPos + direction * roadLen * i;
                GameObject sideWalkSegment = Instantiate(sideWalkPrefab, spawnPos, Quaternion.identity);
                sideWalkSegment.transform.rotation = gameObject.transform.rotation;
                sideWalkSegment.transform.SetParent(transform);
            }
        }
    }
    public float StripeSpacing = 1;
    void PlaceStripes(float roadLen, Vector3 direction) {
        Vector3 right = gameObject.transform.localRotation * Vector3.right;
        // float roadLen = Vector3.Dot(gameObject.GetComponent<Renderer>().bounds.size, right);
        float stripeLen = Vector3.Dot(stripePrefab.GetComponent<Renderer>().bounds.size, stripePrefab.transform.localRotation * Vector3.right);
        // float spacing = roadLen / (numStripes + 1);

        // calculate number of road stripes based on road length
        float numStripes = Mathf.Abs(Mathf.FloorToInt(roadLen / StripeSpacing));

        if (numStripes * StripeSpacing > roadLen) {
            numStripes--;
        }

        Vector3 startPos = gameObject.transform.position + gameObject.transform.forward * stripeLen / 2;// - gameObject.transform.right * (roadLen / 2) + stripePrefab.transform.forward * (stripeLen / 2);
        for (int i = 0; i < numStripes; i++) {
            // Calculate the position for each new object along the target's local forward direction
            // Vector3 position = startPos + gameObject.transform.forward * StripeSpacing * i + gameObject.transform.up * 0.01f;
            Vector3 position = startPos + direction * StripeSpacing * i + gameObject.transform.up * 0.01f;

            // Instantiate the object at the calculated position
            GameObject stripe = Instantiate(stripePrefab, position, Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0)/*  Quaternion.LookRotation(gameObject.transform.right) */);
            stripe.transform.SetParent(transform);
        }
    }
}
