using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    GameObject gameMaster;
    public static bool movingTroops = false;

    // Use this for initialization
    void Start ()
    {
		gameMaster = GameObject.Find("GameMaster");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (GameObject.Find("Selector") != null)
            {
                GameObject[] obj = GameObject.FindGameObjectsWithTag("Selector");

                for(int i = 0; i <= obj.Length - 1; i++)
                {
                    GameObject.Destroy(obj[i]);
                }

                movingTroops = false;
            }
        }
	}

    public void sendTroops()
    {

        if (GameObject.Find("GameMaster").GetComponent<GameMaster>().currentClick.GetComponent<Hex>().owner == GameObject.Find("Player").GetComponent<Player>() && !movingTroops)
        {
            movingTroops = true;

            GameObject cur = GameObject.Find("GameMaster").GetComponent<GameMaster>().currentClick;
            GameObject[] neighbors = cur.GetComponent<Hex>().getNeighbors();

            for (int i = 0; i <= neighbors.Length - 1; i++)
            {
                GameObject selector = (GameObject)Instantiate(Resources.Load(Prefabs.SELECTOR_PREFAB));
                selector.transform.position = new Vector3(neighbors[i].transform.position.x, neighbors[i].transform.position.y + 2.5f, neighbors[i].transform.position.z);
            }
        }
    }
}
