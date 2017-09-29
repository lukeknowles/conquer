using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinding : MonoBehaviour
{
    public GameObject origin;
    public GameObject goal;

    public Vector2 originPos;
    public Vector2 goalPos;

    public List<GameObject> path = new List<GameObject>();

    public List<GameObject> openList = new List<GameObject>();
    public List<GameObject> closedList = new List<GameObject>();

    public void init(GameObject start, GameObject end)
    {
        this.origin = start;
        this.goal = end;

        this.originPos = new Vector2(start.transform.position.x, start.transform.position.z);
        this.goalPos = new Vector2(end.transform.position.x, end.transform.position.z);
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void constructPath()
    {
        closedList.Add(origin);

        GameObject[] neighbors = origin.GetComponent<Hex>().getNeighbors();

        for(int i = 0; i <= neighbors.Length - 1; i++)
        {
            openList.Add(neighbors[i]);
        }

        decideNextNode(origin);
    }

    public void decideNextNode(GameObject obj)
    {
        bool finished = false;
        
        for(int i = 0; i <= openList.Count - 1; i++)
        {
            if (openList[i] == goal)
            {
                // finished
                Debug.Log("DONE");
                finished = true;
            }

            if((openList[i].GetComponent<Hex>().owner == null) || (openList[i].GetComponent<Hex>().owner.id != origin.GetComponent<Hex>().owner.id))
            {
                Debug.Log(openList[i].GetComponent<Hex>().id);

                // can't go through other player's hexes
                closedList.Add(openList[i]);
                openList.RemoveAt(i);
            }

            float fCost = Mathf.Round(Vector2.Distance(new Vector2(obj.transform.position.x, obj.transform.position.z), originPos) * 100) / 100;
            float gCost = Mathf.Round(Vector2.Distance(new Vector2(obj.transform.position.x, obj.transform.position.z), goalPos) * 100) / 100;
            float hCost = fCost + gCost;


        }

        Debug.Log("-Possible Moves-");

        for(int i = 0; i <= openList.Count - 1; i++)
        {
            Debug.Log(openList[i].GetComponent<Hex>().id);
        }
    }
    
    public GameObject getNextNode()
    {
        return path[0];
    }

    public bool atGoal()
    {
        RaycastHit hit;
        return false;

        if(Physics.Raycast(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 500, gameObject.transform.position.z), out hit))
        {
            return (hit.transform.gameObject.GetComponent<Hex>().id == gameObject.GetComponent<Hex>().id);
        }
    }

    public void addPath(GameObject hex)
    {
        path.Add(hex);
    }

    public void removePath(GameObject hex)
    {
        path.Remove(hex);
    }
}
