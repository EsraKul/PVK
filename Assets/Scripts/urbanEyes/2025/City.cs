using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    List<(Vector2, Vector2)> BuildingCoordinates = new List<(Vector2, Vector2)>();
    List<(Vector2, Vector2)> RoadCoordinates = new List<(Vector2, Vector2)>();
    List<(Vector2, Vector2)> WalkwayCoordinates = new List<(Vector2, Vector2)>();
    List<(Vector2, Vector2)> ParkCoordinates = new List<(Vector2, Vector2)>();
    bool drones = false;
    bool pedestrians = false;
    bool traffic = false; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
