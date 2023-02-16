using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script stores every dialogue conversation in a public Dictionary.*/

public class Dialogue : MonoBehaviour
{

    public Dictionary<string, string[]> dialogue = new Dictionary<string, string[]>();

    void Start()
    {
        //Door
        dialogue.Add("LockedDoorA", new string[] {
            "A large door...",
            "Looks like it has a key hole!"
        });


        dialogue.Add("LockedDoorB", new string[] {
            "Key used!"
        });

        //NPC
        dialogue.Add("SellerA", new string[] {
            "Hi there!",
            "Wanna buy some balloons?",
            "What!?",
            "You don't have any money?",
            "If you get me something to drink I'll give you one"
        });

        dialogue.Add("SellerB", new string[] {
            "Hi there!",
            "Wanna buy some balloons?",
            "What!?",
            "You don't have any money?",
            "If you get me something to drink I'll give you one"
        });

        dialogue.Add("SellerBChoice1", new string[] {
            "",
            "",
            "",
            "",
            "Give beer"
        });

        dialogue.Add("SellerBChoice2", new string[] {
            "",
            "",
            "",
            "",
            "Ignore"
        });


        dialogue.Add("ElevatorAUp", new string[] {
            "Where do you wanna go?",
        });
        dialogue.Add("ElevatorAMiddle", new string[] {
            "Where do you wanna go?",
        });
        dialogue.Add("ElevatorABottom", new string[] {
            "Where do you wanna go?",
        }); 
       
        dialogue.Add("ElevatorAUpChoice1", new string[] {
            "Inside"
        });
        dialogue.Add("ElevatorAUpChoice2", new string[] {
            "Street"
        });

        dialogue.Add("ElevatorAMiddleChoice1", new string[] {
            "Roof"
        });
        dialogue.Add("ElevatorAMiddleChoice2", new string[] {
            "Street"
        });

        dialogue.Add("ElevatorABottomChoice1", new string[] {
            "Roof"
        });
        dialogue.Add("ElevatorABottomChoice2", new string[] {
            "Inside"
        });

    }
}
