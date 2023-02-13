using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Manages inventory, keeps several component references, and any other future control of the game itself you may need*/

public class GameManager : MonoBehaviour
{
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
    public Dictionary<string, Item> inventory = new Dictionary<string, Item>();
    public bool[] isFull;
    private static GameManager instance;
    [SerializeField] public AudioTrigger gameMusic;
    [SerializeField] public AudioTrigger gameAmbience;

    // Singleton instantiation
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    public void GetInventoryItem(string name, Sprite image)
    {
        for (int i = 0; i < isFull.Length; i++)
        {
            if (!isFull[i])
            {
                Item item = new Item(image, i);
                inventory.Add(name, item);
                isFull[i] = true;
                if (image != null)
                {
                    hud.SetInventoryImage(inventory[name].itemImage, i);
                }
                break;
            }
            
        }

    }

    public void RemoveInventoryItem(string name)
    {
        hud.SetInventoryImage(hud.blankUI, inventory[name].slotNumber);
        inventory.Remove(name);
        
    }

    public void ClearInventory()
    {   
        inventory.Clear();
        hud.SetInventoryImage(hud.blankUI, 0);
    }

}
