using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    Generate road based on size of the road
 */
public class RoadRender : MonoBehaviour {

    public GameObject stripePrefab;
    // Start is called before the first frame update
    /* void Start() {
        PlaceStripes();
    } */
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
            PlaceStripes();
    }

    public float spacing = 1;

    public void PlaceStripes() {
        Vector3 right = gameObject.transform.localRotation * Vector3.right;
        float roadLen = Vector3.Dot(gameObject.GetComponent<Renderer>().bounds.size, right);
        float stripeLen = Vector3.Dot(stripePrefab.GetComponent<Renderer>().bounds.size, stripePrefab.transform.localRotation * Vector3.right);
        // float spacing = roadLen / (numStripes + 1);

        // calculate number of road stripes based on road length
        float numStripes = Mathf.Abs(Mathf.FloorToInt(roadLen / spacing));

        if (numStripes * spacing > roadLen) {
            numStripes--;
        }

        Vector3 startPos = gameObject.transform.position - gameObject.transform.right * (roadLen / 2) + stripePrefab.transform.forward * (stripeLen / 2);
        for (int i = 0; i < numStripes; i++) {
            // Calculate the position for each new object along the target's local forward direction
            Vector3 position = startPos + gameObject.transform.right * spacing * i + gameObject.transform.up * 0.01f;

            // Instantiate the object at the calculated position
            Instantiate(stripePrefab, position, Quaternion.LookRotation(gameObject.transform.forward));

        }
    }
}
