using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayCardManager : MonoBehaviour {

    public Card topCard;
    public int above;
    public int below;

    public void SetCard() {
        this.GetComponent<SpriteRenderer>().sprite = topCard.cardImage;

        // Handle the Aces and the Kings
        if (topCard.cardValue == 1) {
            above = 2;
            below = 13;
        } else if (topCard.cardValue == 13) {
            above = 1;
            below = 12;

        // The rest of the cards standard setup
        } else {
            above = topCard.cardValue + 1;
            below = topCard.cardValue - 1;
        }

    }
}