using UnityEngine;
using System.Collections;

public class PlayerName : MonoBehaviour
{
    public static string[] prefix = { "American", "Russian", "Mexican", "Syrian", "German", "British", "Australian", "French", "Canadian", "Ukrainian", "Polish", "Austrian", "Armenian", "Kurdish", "Cuban", "Finnish", "Georgian", "Indian", "Iranian", "Italian", "Iraqi", "North Korean", "South Korean", "Chinese", "Swedish" };
    public static string[] suffix = { "Federation", "Republic", "Union", "Empire", "Collective", "Confederacy", "Autocracy", "Monarchy", "Oligarchy", "Commonwealth" };

    public static string getRandomName()
    {
        return ("The " + prefix[Random.Range(0, prefix.Length)] + " " + suffix[Random.Range(0, suffix.Length)]);
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
