using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Teams.TeamName teamName;

    [SerializeField] private bool card1_Availability = true;
    [SerializeField] private bool card2_Availability = true;
    [SerializeField] private bool card3_Availability = false;
    [SerializeField] private bool card4_Availability = false;

    [SerializeField] private GameObject card1;
    [SerializeField] private GameObject card2;
    [SerializeField] private GameObject card3;
    [SerializeField] private GameObject card4;

    public Player(Teams.TeamName teamName)
    {
        this.teamName = teamName;
    }

    public void Update()
    {
        Move();
    }


    public void Move()
    {

    }


    public void UseCard(Teams.TeamName name, GameController.Action action)
    {
        if (teamName == name && action == GameController.Action.Card_DoubleJump)
        {
            card1_Availability = false;
        }
        if (teamName == name && action == GameController.Action.Card_Parachute)
        {
            card2_Availability = false;
        }
        if (teamName == name && action == GameController.Action.Card_DoubleBuild)
        {
            card3_Availability = false;
        }
        if (teamName == name && action == GameController.Action.Card_RemoveBridge)
        {
            card4_Availability = false;
        }
    }

    public void SetCardsOnPanel()
    {
        if(GameController.currentTeam == teamName)
        {
            card1.SetActive(card1_Availability);
            card2.SetActive(card2_Availability);
            card3.SetActive(card3_Availability);
            card4.SetActive(card4_Availability);
            Debug.Log("Cards updated!...");
        }
    }
}
