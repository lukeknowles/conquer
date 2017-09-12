using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public string name;
	public Color color;
    public string id;
    public bool isBot = false;

    public List<GameObject> hexes;

    public List<Player> allies;
    public List<Player> enemies;

	// Use this for initialization
    void Awake()
    {
        hexes = new List<GameObject>();
    }

	void Start () 
	{
        this.id = GameMaster.generateID();
    }
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    public void addEnemy(Player ply)
    {
        this.enemies.Add(ply);
    }

    public void removeEnemy(Player ply)
    {
        this.enemies.Remove(ply);
    }

    public void addAlly(Player ply)
    {
        this.allies.Add(ply);
    }

    public void removeAlly(Player ply)
    {
        this.allies.Remove(ply);
    }

    public void addHex(GameObject hex)
    {
        this.hexes.Add(hex);
    }

    public void removeHex(GameObject hex)
    {
        this.hexes.Remove(hex);
    }

	public void setName(string n)
	{
		this.name = n;
	}

	public void setColor(Color c)
	{
		this.color = c;
	}

	public void setId(string i)
	{
		this.id = i;
	}

	public string getName()
	{
		return this.name;
	}

	public Color getColor()
	{
		return this.color;
	}

	public string getId()
	{
		return this.id;
	}

    public void setBot()
    {
        this.isBot = true;
    }

    public Player(string n, Color c, string i)
    {
        this.setName(n);
        this.setColor(c);
        this.setId(i);
    }

    public void setupBot(string name, Color color, string id)
    {
        this.name = name;
        this.color = color;
        this.id = id;
        this.isBot = true;
    }

}
