using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameMaster : MonoBehaviour
{
    public const int BOTS = 5;
    public bool guiOpen = false;
    public GameObject currentClick;

    public static GameObject player;
    public static List<KeyValuePair<string, GameObject>> botPlayers = new List<KeyValuePair<string, GameObject>>();

    void Awake()
    {
        GameObject ply = (GameObject)Instantiate(Resources.Load(Prefabs.PLAYER_PREFAB));
        ply.name = "Player";

        List<string> names = new List<string>();
        List<Color> colors = new List<Color>();

        // Loops through for every bot that should be generated
        for (int i = 0; i < BOTS; i++)
        {
            string name = PlayerName.getRandomName();
            Color color = PlayerColor.getRandomColor();
            string id = generateID();

            // Generate random name and color for every bot
            for (int j = 0; j <= 10; j++)
            {
                if (colors.Contains(color))
                {
                    color = PlayerColor.getRandomColor();
                }

                if (names.Contains(name))
                {
                    name = PlayerName.getRandomName();
                }
            }

            names.Add(name);
            colors.Add(color);

            GameObject bot = (GameObject)Instantiate(Resources.Load(Prefabs.BOT_PLAYER_PREFAB));
            bot.transform.position = new Vector3(bot.transform.position.x, bot.transform.position.y + i * 3 + 25, bot.transform.position.z);
            bot.name = id;
            bot.tag = "Player";

            Destroy(bot.GetComponent("BotPlayer"));
            bot.AddComponent<Player>();
            bot.GetComponent<Player>().setupBot(name, color, id);
            
            Renderer ren = bot.GetComponent<Renderer>();
            ren.material.SetColor("_Color", color);

            botPlayers.Add(new KeyValuePair<string, GameObject>(bot.GetComponent<Player>().getId(), bot));
        }

        GameObject[] hex = GameObject.FindGameObjectsWithTag("Hex");

        // Loop through every bot
        for (int p = 0; p < BOTS; p++)
        {
            bool taken = false;

            // Assign a random hex for every bot
            for (int i = 0; i <= 50; i++)
            {
                int x = Random.Range(0, hex.Length);

                if (!taken && hex[x].GetComponent<Hex>().owner == null)
                {
                    taken = true;
                    hex[x].GetComponent<Hex>().changeOwner(botPlayers[p].Value.GetComponent<Player>().getId());
                    botPlayers[p].Value.GetComponent<Player>().addHex(hex[x]);
                }
            }
        }

        player = GameObject.Find("Player");
    }

    void Start()
    {
        List<GameObject> plyHex = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hex"));

        for (int i = 0; i <= plyHex.Count - 1; i++)
        {
            if (plyHex[i].GetComponent<Hex>().owner != null)
            {
                plyHex.RemoveAt(i);
            }
        }

        int giveRandom = Random.Range(0, plyHex.Count - 1);

        plyHex[giveRandom].GetComponent<Hex>().changeOwner("Player");
        player.GetComponent<Player>().addHex(plyHex[giveRandom]);
        player.GetComponent<Player>().hexes[0].GetComponent<Hex>().units = 50;
        player.GetComponent<Player>().hexes[0].GetComponent<Hex>().updateUnitText();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown(1) && GameObject.Find("LightColumn") != null && GameObject.Find("Canvas") != null)
        {
            GameObject.Destroy(GameObject.Find("LightColumn"));
            GameObject.Destroy(GameObject.Find("Canvas"));
            this.guiOpen = false;
            this.currentClick = null;
        }
    }

    public static string generateID()
    {
        string chars = "abcdefghijklmnopqrstuvwxyz123456789890";
        string gen = null;

        for(int i = 0; i <= 5; i++)
        {
            gen += chars[Random.Range(0, chars.Length)];
        }

        return gen;
    }

    public enum STATE
    {
        INIT,
        IDLE,
        REPLAY,
        CALULATE
    }
}
