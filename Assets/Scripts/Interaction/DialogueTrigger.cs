using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Triggers a dialogue conversation, passing unique commands and information to the dialogue box and inventory system for fetch quests, etc.*/

public class DialogueTrigger : MonoBehaviour
{

    [Header ("References")]
    [SerializeField] private GameObject finishTalkingActivateObject; //After completing a conversation, an object can activate. 
    [SerializeField] private Animator iconAnimator; //The E icon animator

    [Header("Trigger")]
    [SerializeField] private bool autoHit; //Does the player need to press the interact button, or will it simply fire automatically?
    public bool completed;
    [SerializeField] private bool repeat; //Set to true if the player should be able to talk again and again to the NPC. 
    [SerializeField] private bool sleeping;

    [Header ("Dialogue")]
    [SerializeField] private string characterName; //The character's name shown in the dialogue UI
    [SerializeField] private string dialogueStringA; //The dialogue string that occurs before the fetch quest
    [SerializeField] private string dialogueStringB; //The dialogue string that occurs after fetch quest
    [SerializeField] private AudioClip[] audioLinesA; //The audio lines that occurs before the fetch quest
    [SerializeField] private AudioClip[] audioLinesB; //The audio lines that occur after the fetch quest
    [SerializeField] private AudioClip[] audioChoices; //The audio lines that occur when selecting an audio choice

    [Header ("Fetch Quest")]
    [SerializeField] private GameObject deleteGameObject; //If an NPC is holding the object, and gives it to you, this object will destroy
    [SerializeField] private string getWhichItem1; //The inventory item given if items is fetched
    [SerializeField] private string getWhichItem2;
    [SerializeField] private int getBugsAmount; //Or the amount of coins given if item is fetched
    [SerializeField] private string finishTalkingAnimatorBool; //After completing a conversation, an animation can be fired
    [SerializeField] private string finishTalkingActivateObjectString; //After completing a conversation, an object's name can be searched for and activated.
    [SerializeField] private Sprite getItemSprite1; //The sprite of the inventory item given, shown in HUD
    [SerializeField] private Sprite getItemSprite2;
    [SerializeField] private AudioClip getSound; //When the player is given an object, this sound will play
    [SerializeField] private bool instantGet; //Player can be immediately given an item the moment the conversation begins
    [SerializeField] private string requiredItem1; //The required fetch quest item
    [SerializeField] private string requiredItem2;
    [SerializeField] private int requiredBugs; //Or the required coins (cannot require both an item and coins)
    public Animator useItemAnimator; //If the player uses an item, like a key, an animator can be fired (ie to open a door)
    [SerializeField] private string useItemAnimatorBool; //An animator bool can be set to true once an item is used, like ae key.

    void OnTriggerStay2D(Collider2D col)
    {
        if (instantGet)
        {
            InstantGet();
        }

        if (col.gameObject == NewPlayer.Instance.gameObject && !sleeping && !completed && NewPlayer.Instance.grounded)
        {
            iconAnimator.SetBool("active", true);
            if (autoHit || (Input.GetAxis("Submit") > 0))
            {
                iconAnimator.SetBool("active", false);
                if ((requiredItem1 == "" && requiredBugs == 0 || requiredItem2 == "" && requiredBugs == 0) ||
                    (!GameManager.Instance.inventory.ContainsKey(requiredItem1) && requiredBugs == 0 || 
                    !GameManager.Instance.inventory.ContainsKey(requiredItem2) && requiredBugs == 0) || 
                    (requiredBugs != 0 && NewPlayer.Instance.bugs < requiredBugs))
                {
                    GameManager.Instance.dialogueBoxController.Appear(dialogueStringA, characterName, this, false, audioLinesA, audioChoices, finishTalkingAnimatorBool, finishTalkingActivateObject, finishTalkingActivateObjectString, repeat);
                }
                else if ((requiredBugs == 0 && GameManager.Instance.inventory.ContainsKey(requiredItem1) || requiredBugs == 0 && GameManager.Instance.inventory.ContainsKey(requiredItem2)) || (requiredBugs != 0 && NewPlayer.Instance.bugs >= requiredBugs))
                {
                    if (dialogueStringB != "")
                    {
                        GameManager.Instance.dialogueBoxController.Appear(dialogueStringB, characterName, this, true, audioLinesB, audioChoices, "", null, "", repeat);
                    }
                    else
                    {
                        UseItem();
                    }
                }
                sleeping = true;
            }
        }
        else
        {
            iconAnimator.SetBool("active", false);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            iconAnimator.SetBool("active", false);
            sleeping = completed;
        }
    }

    public void UseItem()
    {
        if (!completed)
        {
            if (useItemAnimatorBool != "")
            {
                useItemAnimator.SetBool(useItemAnimatorBool, true);
            }

            if (deleteGameObject)
            {
                Destroy(deleteGameObject);
            }

            Collect();

            if (GameManager.Instance.inventory.ContainsKey(requiredItem1))
            {
                GameManager.Instance.RemoveInventoryItem(requiredItem1);
            }
            else if(GameManager.Instance.inventory.ContainsKey(requiredItem2))
            {
                GameManager.Instance.RemoveInventoryItem(requiredItem2);
            }
            else
            {
                NewPlayer.Instance.bugs -= requiredBugs;
            }

            repeat = false;
        }
    }

    public void Collect()
    {
        if (!completed)
        {
            if (getWhichItem1 != "")
            {
                GameManager.Instance.GetInventoryItem(getWhichItem1, getItemSprite1);
            }
            else if (getWhichItem2 != "")
            {
                GameManager.Instance.GetInventoryItem(getWhichItem2, getItemSprite2);
            }

            if (getBugsAmount != 0)
            {
                NewPlayer.Instance.bugs += getBugsAmount;
            }

            if (getSound != null)
            {
                GameManager.Instance.audioSource.PlayOneShot(getSound);
            }

            completed = true;
        }
    }

    public void InstantGet()
    {
        if (getWhichItem1 != "")
        {
            GameManager.Instance.GetInventoryItem(getWhichItem1, getItemSprite1);
        }
        else if (getWhichItem2 != "")
        {
            GameManager.Instance.GetInventoryItem(getWhichItem2, getItemSprite2);
        }
        instantGet = false;
    }
}