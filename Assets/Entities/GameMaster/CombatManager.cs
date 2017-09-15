﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    GameObject gameMaster;
    public static bool movingTroops = false;
    public static bool selectingHexToMove = false;
    public static bool verifyHexToMove = false;

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

        if(selectingHexToMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = GameObject.Find("Player").GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    verifyMove(hit.transform.gameObject);
                }

                selectingHexToMove = false;
                verifyHexToMove = true;
            }
        }
	}

    public void verifyMove(GameObject obj)
    {
        GameObject cur = GameObject.Find("GameMaster").GetComponent<GameMaster>().currentClick;
        GameObject[] neighbors = cur.GetComponent<Hex>().getNeighbors();

        bool got = false;

        for(int i = 0; i <= neighbors.Length - 1; i++)
        {
            if (neighbors[i] == obj)
            {
                got = true;
            }
        }

        if (got)
        {
            int amt = int.Parse(GameObject.Find("Canvas").GetComponentInChildren<InputField>().text);
            cur.GetComponent<Hex>().removeUnits(amt);
            cur.GetComponent<Hex>().updateUnitText();

            obj.GetComponent<Hex>().removeUnits(amt);
            obj.GetComponent<Hex>().updateUnitText();
        }

        if (GameObject.Find("LightColumn") != null) { GameObject.Destroy(GameObject.Find("LightColumn")); }
        if (GameObject.Find("Canvas") != null) { GameObject.Destroy(GameObject.Find("Canvas")); }

        GameObject[] selector = GameObject.FindGameObjectsWithTag("Selector");

        for (int v = 0; v <= selector.Length - 1; v++)
        {
            GameObject.Destroy(selector[v]);
        }

        if(obj.GetComponent<Hex>().units <= 0)
        {
            obj.GetComponent<Hex>().changeOwner("Player");
            obj.GetComponent<Hex>().units = Mathf.Abs(obj.GetComponent<Hex>().units);
            obj.GetComponent<Hex>().updateUnitText();
        }

        GameObject.Find("GameMaster").GetComponent<GameMaster>().guiOpen = false;


        verifyHexToMove = false;
    }

    public void sendTroops()
    {
        if (GameObject.Find("Canvas").GetComponentInChildren<InputField>().text.Length > 0)
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

                movingTroops = false;
                selectingHexToMove = true;
            }
        }
    }
}
