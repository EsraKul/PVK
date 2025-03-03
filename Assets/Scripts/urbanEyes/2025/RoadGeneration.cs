using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
    Generate road between two points, how
 */
public class RoadGeneration : MonoBehaviour {
    public GameObject RoadPrefab;
    public GameObject stripePrefab;
    public Vector3 pointA = new Vector3(0, 0, 0);
    public Vector3 pointB = new Vector3(10, 10, 10);


    private float roadLen = 110.192f;
    // Start is called before the first frame update
    void Start() {
        roadLen = Vector3.Dot(RoadPrefab.GetComponent<Renderer>().bounds.size, RoadPrefab.transform.localRotation * Vector3.right);
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


        float numRoads = Mathf.Abs(Mathf.FloorToInt(dist / roadLen));

        Vector3 startPos = pointA + direction * (roadLen / 2);

        for (int i = 0; i < numRoads; i++) {
            Vector3 spawnPos = startPos + direction * roadLen * i;
            GameObject roadSegment = Instantiate(RoadPrefab, spawnPos, Quaternion.identity);
            roadSegment.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0);
            roadSegment.transform.SetParent(transform);
        }

        // float remainingDistance = Vector3.Distance(pointA + direction * numRoads * roadLen, pointB);
        float remainingDistance = dist - roadLen * numRoads;
        if (remainingDistance > 0) {
            // starting position of the last road segment
            Vector3 lastPos = startPos + direction * roadLen * numRoads;
            GameObject lastRoad = Instantiate(RoadPrefab, lastPos, Quaternion.identity);
            lastRoad.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 270, 0);
            lastRoad.transform.SetParent(transform);

            Vector3 scale = lastRoad.transform.localScale;
            scale.x = remainingDistance / roadLen;
            lastRoad.transform.localScale = scale;


        }
    }
    public float StripeSpacing = 1;
    void PlaceStripes() {
        Vector3 right = gameObject.transform.localRotation * Vector3.right;
        float roadLen = Vector3.Dot(gameObject.GetComponent<Renderer>().bounds.size, right);
        float stripeLen = Vector3.Dot(stripePrefab.GetComponent<Renderer>().bounds.size, stripePrefab.transform.localRotation * Vector3.right);
        // float spacing = roadLen / (numStripes + 1);

        // calculate number of road stripes based on road length
        float numStripes = Mathf.Abs(Mathf.FloorToInt(roadLen / StripeSpacing));

        if (numStripes * StripeSpacing > roadLen) {
            numStripes--;
        }

        Vector3 startPos = gameObject.transform.position - gameObject.transform.right * (roadLen / 2) + stripePrefab.transform.forward * (stripeLen / 2);
        for (int i = 0; i < numStripes; i++) {
            // Calculate the position for each new object along the target's local forward direction
            Vector3 position = startPos - gameObject.transform.right * StripeSpacing * i + gameObject.transform.up * 0.01f;

            // Instantiate the object at the calculated position
            Instantiate(stripePrefab, position, Quaternion.LookRotation(gameObject.transform.forward));

        }
    }
}
