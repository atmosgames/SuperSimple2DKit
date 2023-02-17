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
        
        dialogue.Add("Seller2A", new string[] {
            "...",
            "Do you want to get another one?",
            "This one is actually for free"
        });

        dialogue.Add("Seller2AChoice1", new string[] {
            "",
            "",
            "Get balloon"
        });

        dialogue.Add("Seller2AChoice2", new string[] {
            "",
            "",
            "Ignore"
        });

        dialogue.Add("AccessA", new string[] {
            "Access denied"
        });

        dialogue.Add("Beer", new string[] {
            "Do you want to get a beer"
        });

        dialogue.Add("BeerChoice1", new string[] {
            "Yes"
        });

        dialogue.Add("BeerChoice2", new string[] {
            "No"
        });

        dialogue.Add("AccessB", new string[] {
            "Access granted"
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

        dialogue.Add("ElevatorBUp", new string[] {
            "Where do you wanna go?",
        });
        dialogue.Add("ElevatorBMiddle", new string[] {
            "Where do you wanna go?",
        });
        dialogue.Add("ElevatorBBottom", new string[] {
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

        dialogue.Add("ElevatorBUpChoice1", new string[] {
            "Street"
        });
        dialogue.Add("ElevatorBUpChoice2", new string[] {
            "Underground"
        });

        dialogue.Add("ElevatorBMiddleChoice1", new string[] {
            "Inside"
        });
        dialogue.Add("ElevatorBMiddleChoice2", new string[] {
            "Underground"
        });

        dialogue.Add("ElevatorBBottomChoice1", new string[] {
            "Inside"
        });
        dialogue.Add("ElevatorBBottomChoice2", new string[] {
            "Street"
        });

    }
}
