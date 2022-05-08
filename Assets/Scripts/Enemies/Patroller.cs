using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    //speed of the object
    [SerializeField] 
    private float moveSpeed;

    //list of all waypoints in the object's patrol path
    [SerializeField]
    private List<Transform> patrolPoints = new List<Transform>();

    //target index in the list of waypoints. serves as the next waypoint
    //in the object's patrol path
    [SerializeField]
    public int nextPoint = 0;

    //number used to set the next waypoint object based
    //on its last destination
    private int changeValue;

    //placeholdrs for the original measurements of the object
    private float sizeX;
    private float sizeY;
    private float sizeZ;

    private void Start()
    {
        //records the measurements of the object
        sizeX = transform.localScale.x;
        sizeY = transform.localScale.y;
        sizeZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    //makes the object move in a designated pattern
    void Patrol()
    {
        //sets the next waypoint
        Transform destinationPoint = patrolPoints[nextPoint];

        //flips sprite depending on whether if is facing left or right
        if (destinationPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(sizeX * -1, sizeY, sizeZ);
        }
        else
        {
            transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
        }

        //moves object in the direction of the next waypoint
        transform.position = Vector2.MoveTowards(transform.position, destinationPoint.position, moveSpeed * Time.deltaTime);

        //if the waypoint has been reached, it determines the next waypoint
        if (Vector2.Distance(transform.position, destinationPoint.position)<0.2f)
        {
            //if there are no more waypoints in the list, it will start moving back in the opposite direction
            if(nextPoint == patrolPoints.Count - 1)
            {
                changeValue = -1;
                //Debug.Log(nextPoint);
            }

            //if the object is back at the starting point, it will switch directions again
            if(nextPoint == 0)
            {
                changeValue = 1;
                //Debug.Log(nextPoint);
            }

            //once the direction has been determined, this sets the next waypoint 
            nextPoint += changeValue;  
        }


    }
}
