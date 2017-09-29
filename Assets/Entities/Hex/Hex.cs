using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Hex : MonoBehaviour 
{
	public Player owner;
	public string title;
    public Type type = Type.NORMAL;
    public string id;

	public int units = 1;
	
	// Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        this.id = GameMaster.generateID();

        if (owner != null)
        {
            this.units = 50;
            this.gameObject.GetComponent<Renderer>().material.color = owner.color;
            this.type = Type.CAPITAL;
        }

        if(gameObject.transform.Find("UnitCount").gameObject.active) { updateUnitText(); }

        float regenRate = 3.0f;
        float initRate = Random.Range(0.1f, 2.0f);

        if (this.type == Type.CAPITAL) { regenRate = 1.5f; }
        if (this.type == Type.CAPITAL) { initRate = 1.5f; }


        InvokeRepeating("generateUnit", initRate, regenRate);
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && GameObject.Find("GameMaster").GetComponent<GameMaster>().guiOpen == false)
        {
            if(GameObject.Find("LightColumn") != null) { GameObject.Destroy(GameObject.Find("LightColumn")); }
            if(GameObject.Find("Canvas") != null) { GameObject.Destroy(GameObject.Find("Canvas")); }

            GameObject light = (GameObject)Instantiate(Resources.Load(Prefabs.LIGHT_COLUMN_PREFAB));
            GameObject gui = (GameObject)Instantiate(Resources.Load(Prefabs.GUI_HEXCLICK_PREFAB));

            GameObject.Find("GameMaster").GetComponent<GameMaster>().guiOpen = true;
            gui.name = "Canvas";
            light.name = "LightColumn";
            light.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);

            GameObject.Find("GameMaster").GetComponent<GameMaster>().currentClick = gameObject;

            getNeighbors();
        }
    }

    // Update is called once per frame
    void Update () 
	{

	}

    public void generateUnit()
    {
        this.addUnits(1);
        if(gameObject.transform.Find("UnitCount").gameObject.active) { this.updateUnitText(); }
    }

    public void updateUnitText()
    {
        this.GetComponentInChildren<TextMesh>().text = this.units.ToString();
    }

    public void initOwner(string id)
    {

    }

	public void changeOwner(string id)
	{
		this.owner = GameObject.Find(id.ToString()).GetComponent<Player>();
        //this.GetComponentInChildren<TextMesh>().text = units.ToString();
        Color32 x = new Color32((byte)(owner.color.r * 2), (byte)(owner.color.g * 2), (byte)(owner.color.b * 2), 1);
        //this.GetComponentInChildren<TextMesh>().color = owner.color;
        Renderer ren = gameObject.GetComponent<Renderer>();
        ren.material.SetColor("_Color", owner.color);
        GameObject.Find(id.ToString()).GetComponent<Player>().hexes.Add(gameObject);
    }

    public void disableUnitText()
    {
        gameObject.transform.Find("UnitCount").gameObject.SetActive(false);
    }

    public void enableUnitText()
    {
        gameObject.transform.Find("UnitCount").gameObject.SetActive(true);
        updateUnitText();
    }

    public GameObject[] getNeighbors()
    {
        List<GameObject> neighbors = new List<GameObject>();
        List<RaycastHit> hits = new List<RaycastHit>();

        RaycastHit[] hitA = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 1500, transform.position.y, transform.position.z));
        for(int i = 0; i <= hitA.Length - 1; i++) { if(hitA[i].distance <= 1 && hitA[i].transform.gameObject.name == "Hex") { neighbors.Add(hitA[i].transform.gameObject); } }

        RaycastHit[] hitB = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 1500, transform.position.y, transform.position.z));
        for (int i = 0; i <= hitB.Length - 1; i++) { if (hitB[i].distance <= 1 && hitB[i].transform.gameObject.name == "Hex") { neighbors.Add(hitB[i].transform.gameObject); } }

        RaycastHit[] hitC = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 750, transform.position.y, transform.position.z + 1250));
        for (int i = 0; i <= hitC.Length - 1; i++) { if (hitC[i].distance <= 1 && hitC[i].transform.gameObject.name == "Hex") { neighbors.Add(hitC[i].transform.gameObject); } }

        RaycastHit[] hitD = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 750, transform.position.y, transform.position.z - 1250));
        for (int i = 0; i <= hitD.Length - 1; i++) { if (hitD[i].distance <= 1 && hitD[i].transform.gameObject.name == "Hex") { neighbors.Add(hitD[i].transform.gameObject); } }

        RaycastHit[] hitE = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x + 700, transform.position.y, transform.position.z - 1000));
        for (int i = 0; i <= hitE.Length - 1; i++) { if (hitE[i].distance <= 1 && hitE[i].transform.gameObject.name == "Hex") { neighbors.Add(hitE[i].transform.gameObject); } }

        RaycastHit[] hitF = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(transform.position.x - 700, transform.position.y, transform.position.z + 1000));
        for (int i = 0; i <= hitF.Length - 1; i++) { if (hitF[i].distance <= 1 && hitF[i].transform.gameObject.name == "Hex") { neighbors.Add(hitF[i].transform.gameObject); } }

        return neighbors.ToArray();
    }

    public GameObject[] getNeighbors(GameObject hex) // Get neighbors next to specific hex
    {
        List<GameObject> neighbors = new List<GameObject>();
        List<RaycastHit> hits = new List<RaycastHit>();

        RaycastHit[] hitA = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x + 1500, hex.transform.position.y, hex.transform.position.z));
        for (int i = 0; i <= hitA.Length - 1; i++) { if (hitA[i].distance <= 1 && hitA[i].transform.gameObject.name == "Hex") { neighbors.Add(hitA[i].transform.gameObject); } }

        RaycastHit[] hitB = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x - 1500, hex.transform.position.y, hex.transform.position.z));
        for (int i = 0; i <= hitB.Length - 1; i++) { if (hitB[i].distance <= 1 && hitB[i].transform.gameObject.name == "Hex") { neighbors.Add(hitB[i].transform.gameObject); } }

        RaycastHit[] hitC = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x + 750, hex.transform.position.y, hex.transform.position.z + 1250));
        for (int i = 0; i <= hitC.Length - 1; i++) { if (hitC[i].distance <= 1 && hitC[i].transform.gameObject.name == "Hex") { neighbors.Add(hitC[i].transform.gameObject); } }

        RaycastHit[] hitD = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x - 750, hex.transform.position.y, hex.transform.position.z - 1250));
        for (int i = 0; i <= hitD.Length - 1; i++) { if (hitD[i].distance <= 1 && hitD[i].transform.gameObject.name == "Hex") { neighbors.Add(hitD[i].transform.gameObject); } }

        RaycastHit[] hitE = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x + 700, hex.transform.position.y, hex.transform.position.z - 1000));
        for (int i = 0; i <= hitE.Length - 1; i++) { if (hitE[i].distance <= 1 && hitE[i].transform.gameObject.name == "Hex") { neighbors.Add(hitE[i].transform.gameObject); } }

        RaycastHit[] hitF = Physics.RaycastAll(new Vector3(hex.transform.position.x, hex.transform.position.y, hex.transform.position.z), new Vector3(hex.transform.position.x - 700, hex.transform.position.y, hex.transform.position.z + 1000));
        for (int i = 0; i <= hitF.Length - 1; i++) { if (hitF[i].distance <= 1 && hitF[i].transform.gameObject.name == "Hex") { neighbors.Add(hitF[i].transform.gameObject); } }

        Debug.Log("POS - (" + hex.transform.position.x + ", " + hex.transform.position.y + ", " + hex.transform.position.z + ")");
        Debug.Log("HITS - " + hits.Count);

        return neighbors.ToArray();
    }


    public static void initFogOfWar()
    {
        GameObject[] hex = GameObject.FindGameObjectsWithTag("Hex");

        for(int i = 0; i <= hex.Length - 1; i++)
        {
            hex[i].transform.Find("UnitCount").gameObject.SetActive(false);
            hex[i].transform.Find("Fog").gameObject.SetActive(true);
        }
    }

    public static void updateFogOfWar()
    {
        GameObject[] hexes = GameObject.FindGameObjectsWithTag("Hex");
        List<GameObject> plyHex = GameObject.Find("Player").GetComponent<Player>().hexes;

        for(int i = 0; i <= plyHex.Count - 1; i++)
        {
            GameObject[] neighbor = plyHex[i].GetComponent<Hex>().getNeighbors();

            for(int v = 0; v <= neighbor.Length - 1; v++)
            {
                neighbor[v].transform.Find("Fog").gameObject.SetActive(false);
                neighbor[v].transform.Find("UnitCount").gameObject.SetActive(true);
                neighbor[v].GetComponent<Hex>().updateUnitText();
            }
        }


        /*for (int i = 0; i <= hexes.Length - 1; i++)
        {
            if(neighbors.Contains(hexes[i]))
            {
                hexes[i].transform.Find("Fog").gameObject.SetActive(false);
                hexes[i].transform.Find("UnitCount").gameObject.SetActive(true);
                hexes[i].GetComponent<Hex>().updateUnitText();
            }
            else
            {
                hexes[i].transform.Find("Fog").gameObject.SetActive(true);
                hexes[i].transform.Find("UnitCount").gameObject.SetActive(false);
            }
        }*/
    }

    public void addUnits(int n)
    {
        this.units += n;
    }

    public void removeUnits(int n)
    {
        this.units -= n;
    }

    public enum Type
    {
        NORMAL,
        CAPITAL
    }
}
