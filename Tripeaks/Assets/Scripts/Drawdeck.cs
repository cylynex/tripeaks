using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Drawdeck : MonoBehaviour {

    public GameObject gm;
    PlayCardManager playCardManager;

    public GameObject pcm;
    GameManager gameManager;

    public List<Card> drawCards = new List<Card>();
    

    private void Start() {

        // Fetch and Condense variables a bit
        FetchVariables();        

    }


    void FetchVariables() {

        gm = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gm.GetComponent<GameManager>();

        pcm = GameObject.FindGameObjectWithTag("PlayCardManager");
        playCardManager = pcm.GetComponent<PlayCardManager>();

    }


    private void OnMouseDown() {

        if (drawCards.Count > 0) {

            gameManager.ClearBonusText();

            // Setup the card on the draw deck so the game knows what its comparing to for matches.
            playCardManager.topCard = drawCards[0];
            playCardManager.SetCard();
            drawCards.RemoveAt(0);
            gameManager.UpdateCardsLeft(drawCards.Count);

            // Score stuff.  Don't charge for the draw card on the first turn.
            gameManager.scoreSpot = 0;
            if (gameManager.isFirstTurn == true) {
                gameManager.isFirstTurn = false;
                gameManager.ShowBonusText("The Game Begins!");
            } else {
                gameManager.scoreThisGame = gameManager.scoreThisGame - 5;
            }

            gameManager.UpdateScore();
            gameManager.ResetStreak();

        } else {
            SceneManager.LoadScene("Lose");
            gameManager.CloseOutOverallScore();
        }

    }

}