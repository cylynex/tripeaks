using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager : MonoBehaviour {
    
    public Card thisCard;
    public GameObject[] blockerCards;
    public bool isTopCard;

    GameObject pcm;
    PlayCardManager playCardManager;

    GameObject gm;
    GameManager gameManager;


    private void Start() {
        pcm = GameObject.FindGameObjectWithTag("PlayCardManager");
        playCardManager = pcm.GetComponent<PlayCardManager>();

        gm = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();
    }


    private void OnMouseDown() {

        int blockersLeft = 0;

        // Establish cards in the way of the card
        for(int i = 0;i < blockerCards.Length;i++) {
            if (blockerCards[i] != null) {
                blockersLeft++;
            } 
        }

        if (blockersLeft == 0) {

            // Card matches the above condition - playable
            if (thisCard.cardValue == playCardManager.above) {
                PlayThisCard();

            // Card matches the below condition - playable
            } else if (thisCard.cardValue == playCardManager.below) {
                PlayThisCard();
            } else {
                gameManager.ShowBonusText("Invalid Card");
            }
        } 
    }


    void PlayThisCard() {

        // Move this card to the top of the play deck
        playCardManager.topCard = thisCard;
        playCardManager.SetCard();
        gameManager.cardsLeftInPlay = gameManager.cardsLeftInPlay - 1;

        gameManager.ClearBonusText();

        // Scoring Stuff
        if (isTopCard) {
            gameManager.AddConquerScore(50);
            gameManager.ShowBonusText("You have Conquered a Peak for 50 bonus points");
        } else {
            gameManager.AddScore();
        }

        gameManager.UpdateScore();

        gameManager.currentStreak++;
        gameManager.UpdateCurrentStreak();

        Object.Destroy(this.gameObject);

        gameManager.CheckForWin();
    }


}
