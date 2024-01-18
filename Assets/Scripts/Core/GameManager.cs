using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Manages inventory, keeps several component references, and any other future control of the game itself you may need*/

public class GameManager : MonoBehaviour
{
    public enum ItemName
    {
        None,
        Copy,
        KeyCard,
        RedBalloon, BlueBalloon, Melee, Dynamite, LightBeer, Chair, Flamethrower
    };



    public int balloons
    {
        get
        {
            var r = System.Convert.ToInt32(GameManager.Instance.IsItemInInventory(ItemName.RedBalloon)) + System.Convert.ToInt32(GameManager.Instance.IsItemInInventory(ItemName.BlueBalloon));
            Debug.Log(r);
            return r;
        }
    }
    public class Item
    {
        public Sprite itemImage;
        public int slotNumber;
        public Item(Sprite sprite, int number)
        {
            itemImage = sprite;
            slotNumber = number;
        }
    }
    public AudioSource audioSource; //A primary audioSource a large portion of game sounds are passed through
    public DialogueBoxController dialogueBoxController;
    public HUD hud; //A reference to the HUD holding your health UI, coins, dialogue, etc.
    public Dictionary<ItemName, Item> inventory = new Dictionary<ItemName, Item>();
    public Dictionary<ItemName, Item>.KeyCollection keys { get; private set; }
    public bool[] isFull;
    private static GameManager instance;
    [SerializeField] public AudioTrigger gameMusic;
    [SerializeField] public AudioTrigger gameAmbience;

    [System.Serializable]
    public class EndingClass
    {
        public string key;
        public Ending val;
        public int bugs;
    }

    [SerializeField]
    private List<EndingClass> endingList = new List<EndingClass>();
    private Dictionary<string, Ending> endingDict = new Dictionary<string, Ending>();
    public Dictionary<string, int> gameCompletion = new Dictionary<string, int>();//0 nie zrobiono, 1 zrobiono
    public Dictionary<string, int> reward = new Dictionary<string, int>();//0 nie zrobiono, 1 zrobiono

    void Awake()
    {
        if (Instance != this) Destroy(gameObject);

        foreach (var kvp in endingList)
        {
            endingDict[kvp.key] = kvp.val;
            gameCompletion[kvp.key] = PlayerPrefs.GetInt(kvp.key, 0);
            reward[kvp.key] = kvp.bugs;
        }
    }
    // Singleton instantiation
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsItemInInventory(ItemName name)
    {
        return inventory.ContainsKey(name);
    }
    void AddItemToInventory(ItemName name, Item value)
    {
        inventory.Add(name, value);
        keys = inventory.Keys;

    }

    // Use this for initialization
    public void GetInventoryItem(ItemName name, Sprite image)
    {
        for (int i = 0; i < isFull.Length; i++)
        {
            if (!isFull[i])
            {
                Item item = new Item(image, i);
                AddItemToInventory(name, item);
                isFull[i] = true;
                if (image != null)
                {
                    hud.SetInventoryImage(inventory[name].itemImage, i);
                }
                break;
            }

        }

    }

    public void RemoveInventoryItem(ItemName name)
    {
        hud.SetInventoryImage(hud.blankUI, inventory[name].slotNumber);
        isFull[inventory[name].slotNumber] = false;
        inventory.Remove(name);
        keys = inventory.Keys;

    }

    public void ClearInventory()
    {
        inventory.Clear();
        hud.SetInventoryImage(hud.blankUI, 0);
    }

    public void EndGame(string ending)
    {
        if (!endingDict.ContainsKey(ending))
            Debug.LogError("Wrong ending name: " + ending);
        else
        {
            gameCompletion[ending] = 1;
            if (PlayerPrefs.GetInt(ending) == 0)
                NewPlayer.Instance.bugs += reward[ending];

            PlayerPrefs.SetInt(ending, 1);
            EndingPlayer.currentEnding = endingDict[ending];
            SceneManager.LoadScene("EndingScene");
        }

        PlayerPrefs.SetString("CurrendEnding", ending);
    }

    [ContextMenu("ResetEndings")]
    public void ResetEndings()
    {
        foreach (var kvp in endingList)
        {
            gameCompletion[kvp.key] = 0;
        }
    }

}
