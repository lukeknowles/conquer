using UnityEngine;
using System.Collections;

public class PlayerColor : MonoBehaviour
{
    public static Color[] colors = { new Color32(255, 0, 0, 255), new Color32(0, 255, 0, 255), new Color32(0, 0, 255, 255), new Color32(255, 255, 0, 255), new Color32(255, 188, 0, 255), new Color32(239, 0, 255, 255), new Color32(154, 0, 255, 255), new Color32(0, 239, 255, 255), new Color32(78, 49, 5, 255) };


    public enum Colors
    {
        RED = 0,
        GREEN = 1,
        BLUE = 2,
        YELLOW = 3,
        ORANGE = 4,
        PINK = 5,
        PURPLE = 6,
        CYAN = 7,
        BROWN = 8
    }

    public static Color getRandomColor()
    {
        return(colors[Random.Range(0, System.Enum.GetValues(typeof(Colors)).Length)]);
    }

    public static Color getColor(Colors color)
    {
        return(colors[(int)color]);
    }

    public static Colors getColorFromId(int id)
    {
        return ((Colors)id);
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
