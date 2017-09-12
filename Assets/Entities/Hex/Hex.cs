using UnityEngine;
using System.Collections;

public class Hex : MonoBehaviour 
{
	public Player owner;
	public string title;
    public Type type = Type.NORMAL;
    public string id;

	public int units = 500;
	
	// Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        this.id = GameMaster.generateID();

        if (owner != null)
        {
            this.units = 1500;
            this.gameObject.GetComponent<Renderer>().material.color = owner.color;
            this.type = Type.CAPITAL;
        }

        updateUnitText();

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
        }
    }

    // Update is called once per frame
    void Update () 
	{

	}

    public void generateUnit()
    {
        this.addUnits(1);
        this.updateUnitText();
    }

    public void updateUnitText()
    {
        this.GetComponentInChildren<TextMesh>().text = this.units.ToString();
    }

	public void changeOwner(string id)
	{

		this.owner = GameObject.Find(id.ToString()).GetComponent<Player>();
        this.GetComponentInChildren<TextMesh>().text = units.ToString();
        Color32 x = new Color32((byte)(owner.color.r * 2), (byte)(owner.color.g * 2), (byte)(owner.color.b * 2), 1);
        this.GetComponentInChildren<TextMesh>().color = owner.color;
        Renderer ren = gameObject.GetComponent<Renderer>();
        ren.material.SetColor("_Color", owner.color);
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
