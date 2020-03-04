using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour {

    // Cards Management
    public List<Card> cards = new List<Card>();
    public Card[] deckOfCards = new Card[52];
    public GameObject[] cardHolders;
    public GameObject drawDeck;

    // Scoring Stuff
    public List<int> scoreIncrements = new List<int>();
    public int scoreSpot;
    public int scoreThisGame;
    public static int overallScore;
    public int currentStreak;

    // Turn rotation stuff
    public bool isFirstTurn;
    public int cardsLeftInPlay;

    // UI Stuff
    public Text numCardsLeft;
    public Text scoreThisGameLabel;
    public Text bonusTextLabel;
    public Text overallScoreLabel;
    public Image currentBoardBackground;
    public Text currentStreakLabel;

    public static Sprite boardBackground;
    public Sprite defaultBoardBackground;


    void Start() {

        // Setup the background
        SetupEnvironment();

        // Setup the game itself
        SetupTurnsAndScores();

        // initialize deck
        InitializeDeck();

        // Deal the cards and setup the board
        DealCards();

        // Create play deck
        CreatePlayDeck();                  

    }


    void SetupEnvironment() {
        if (boardBackground) {
            currentBoardBackground.GetComponent<Image>().sprite = boardBackground;
        } else {
            currentBoardBackground.GetComponent<Image>().sprite = defaultBoardBackground;
        }
    }


    void SetupTurnsAndScores() {

        if (overallScore == null) { overallScore = 0; }
        UpdateOverallScore();

        scoreSpot = 0;
        scoreThisGame = 0;
        scoreThisGameLabel.text = scoreThisGame.ToString();
        isFirstTurn = true;
        currentStreak = 0;
        UpdateCurrentStreak();
    }


    // Setup the deck and put all the cards into the list from the master array.
    void InitializeDeck() {
        
        for (int i = 0; i < deckOfCards.Length; i++) {
            Card newCard = new Card();
            newCard = deckOfCards[i];
            cards.Add(deckOfCards[i]);
        }        
    }


    // Deal the cards to the gameplay board randomly.
    void DealCards() {

        for (int i = 0; i < 28; i++) {
            int cardSpot = Random.Range(0, (cards.Count - 1));
            cardHolders[i].GetComponent<CardManager>().thisCard = cards[cardSpot];
            cardHolders[i].GetComponent<SpriteRenderer>().sprite = cards[cardSpot].cardImage;
            cards.RemoveAt(cardSpot);
        }

        cardsLeftInPlay = 28;

    }


    // Randomly place the cards into the draw deck
    void CreatePlayDeck() {
        drawDeck = GameObject.FindGameObjectWithTag("Drawdeck");
        int maxCards = cards.Count;
        for (int i = 0; i < maxCards; i++) {
            int cardPicked = Random.Range(0, cards.Count - 1);
            drawDeck.GetComponent<Drawdeck>().drawCards.Add(cards[cardPicked]);
            cards.RemoveAt(cardPicked);
        }

        numCardsLeft.text = drawDeck.GetComponent<Drawdeck>().drawCards.Count.ToString();

    }


    public void CheckForWin() {
        if (cardsLeftInPlay == 0) {
            // Game over, they won.  Award 50 point bonus and end the game
            scoreThisGame = scoreThisGame + 50;
            SceneManager.LoadScene("Win");
            CloseOutOverallScore();
        }
    }


    // UI Stuff
    public void UpdateCardsLeft(int cardsLeft) {
        numCardsLeft.text = cardsLeft.ToString();
    }


    public void UpdateScore() {
        scoreThisGameLabel.text = scoreThisGame.ToString();
        UpdateOverallScore();
    }

    public void AddScore() {
        scoreThisGame = scoreThisGame + scoreIncrements[scoreSpot];
        scoreSpot++;
    }

    public void AddConquerScore(int conquerPoints) {
        scoreThisGame = scoreThisGame + conquerPoints;
        scoreSpot++;
    }

    public void ShowBonusText(string bonusText) {
        bonusTextLabel.text = bonusText;
    }

    public void ClearBonusText() {
        bonusTextLabel.text = "";
    }
    
    void UpdateOverallScore() {
        overallScoreLabel.text = overallScore.ToString();
    }

    public void CloseOutOverallScore() {
        overallScore = overallScore + scoreThisGame;
    }

    public void UpdateCurrentStreak() {
        currentStreakLabel.text = currentStreak.ToString();
    }

    public void ResetStreak() {
        currentStreak = 0;
        UpdateCurrentStreak();
    }

}
