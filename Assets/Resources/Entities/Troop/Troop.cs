using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    public int amount;
    public Player owner;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void init(int amt, Player own)
    {
        this.amount = amt;
        this.owner = own;
    }

    public int getAmount()
    {
        return this.amount;
    }

    public Player getOwner()
    {
        return this.owner;
    }
}
