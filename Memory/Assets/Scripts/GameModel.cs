using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameModel : MonoBehaviour {

	private Dictionary<int, int> boardCardGame = new Dictionary<int, int> ();
	private int pairIndex;
	private GridLayoutGroup gL;
	private Text playerActiveCanvasText;

	private int currentPlayer;


	private GameObject firstPickedCard;
	private GameObject secondPickedCard;
	private int pOneScore = 0;
	private int pTwoScore = 0;

	public GameObject card;
	public GameObject cardBoardCanvas;
	public GameObject playerActiveCanvas;
	public GameObject playerOnePseudo;
	public GameObject playerTwoPseudo;
	public GameObject playerOneScore;
	public GameObject playerTwoScore;


	void Awake(){

		/**
		 * Initialize the card value on the board
		 * 

		 * */
		for (int i = 0; i < GameSceneManager.getPairNumber () * 2; i++) {
			do {
				pairIndex = Random.Range (1, GameSceneManager.getPairNumber ()+1);
			} while (!checkBoardCardGamePair (pairIndex));
			boardCardGame.Add (i, pairIndex);
			Debug.Log ("Key : " + i + " Card : " + pairIndex);
			GameObject go = (GameObject)Instantiate (card);
			go.transform.SetParent (cardBoardCanvas.transform);
			go.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			go.GetComponent<CardData> ().setData (i, pairIndex);
			go.GetComponent<Button> ().onClick.AddListener (() => cardClick ());

		}

		adjustGridLayout ();

		playerOnePseudo.GetComponent<Text> ().text = GameSceneManager.getPlayerOnePseudo ();
		playerTwoPseudo.GetComponent<Text> ().text = GameSceneManager.getPlayerTwoPseudo ();
		playerActiveCanvasText = playerActiveCanvas.GetComponentInChildren<Text> ();

		currentPlayer = Random.Range (1, 3);
		giveActivePlayer ();
	}

	// Use this for initialization
	void Start () {
		

	}

	/// <summary>
	/// Checks if the card in parameter is already present two times on the board
	/// </summary>
	/// <returns><c>true</c>, if the card is already present two times, <c>false</c> otherwise.</returns>
	/// <param name="value">Value of the card.</param>
	private bool checkBoardCardGamePair(int value){
		int counter = 0;
		if (boardCardGame.Count > 0) {
			foreach (int v in boardCardGame.Values) {
				if (v == value && counter < 2) {
					counter++;
				}
				if (counter >= 2) {
					return false;
				}
			}
			return true;
		} else {
			return true;
		}
	}

	/// <summary>
	/// Updates the grid layout property.
	/// </summary>
	/// <param name="cellSize">Cell size.</param>
	/// <param name="bottomPadding">Bottom padding.</param>
	/// <param name="constraintCount">Constraint count.</param>
	private void updateGridLayoutProperty(Vector2 cellSize, int bottomPadding, int constraintCount){
		gL.constraintCount = constraintCount;
		Vector2 temp = gL.cellSize;
		temp = cellSize;
		gL.cellSize = temp;
		gL.padding.bottom = bottomPadding;
	}

	/// <summary>
	/// Adjusts the grid layout.
	/// </summary>
	private void adjustGridLayout(){
		
		int pN = GameSceneManager.getPairNumber ();
		gL = cardBoardCanvas.GetComponent<GridLayoutGroup> ();

		switch (pN) {

		case 4:
			updateGridLayoutProperty (new Vector2 (300, 300), 200, 4);
			break;
		case 5:
			updateGridLayoutProperty (new Vector2 (300, 300), 200, 5);
			break;
		case 6:
			updateGridLayoutProperty (new Vector2 (250, 250), 180, 4);
			break;
		case 7:
			updateGridLayoutProperty (new Vector2 (250, 250), 180, 5);
			break;
		case 8:
			updateGridLayoutProperty (new Vector2 (220, 220), 190, 6);
			break;
		case 9:
			updateGridLayoutProperty (new Vector2 (220, 220), 190, 6);
			break;
		case 10:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 5);
			break;
		case 11:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 6);
			break;
		case 12:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 6);
			break;
		case 13:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 7);
			break;
		case 14:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 7);
			break;
		case 15:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 8);
			break;
		case 16:
			updateGridLayoutProperty (new Vector2 (190, 190), 180, 8);
			break;
		}
	}

	private void cardClick(){
		if (firstPickedCard == null) {
			firstPickedCard = EventSystem.current.currentSelectedGameObject.gameObject;
			firstPickedCard.GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Easy/" + boardCardGame [firstPickedCard.GetComponent<CardData> ().getKey ()]);
			EventSystem.current.SetSelectedGameObject (null);
		} else if (secondPickedCard == null) {
			secondPickedCard = EventSystem.current.currentSelectedGameObject.gameObject;
			secondPickedCard.GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/Easy/" + boardCardGame [secondPickedCard.GetComponent<CardData> ().getKey ()]);
			EventSystem.current.SetSelectedGameObject (null);
			if (firstPickedCard.GetComponent<CardData> ().getValue () == secondPickedCard.GetComponent<CardData> ().getValue ()) {
				adjustScore ();
				firstPickedCard = null;
				secondPickedCard = null;
			} else {
				switchCurrentPlayer ();
				giveActivePlayer ();
				firstPickedCard.GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/cart_back");
				secondPickedCard.GetComponent<Image> ().sprite = Resources.Load <Sprite> ("Sprites/cart_back");
				firstPickedCard = null;
				secondPickedCard = null;
			}
		}

	}

	private void giveActivePlayer(){
		playerActiveCanvas.SetActive (true);
		if(currentPlayer == 1){
			playerActiveCanvasText.text = "C'est à " + GameSceneManager.getPlayerOnePseudo () + " de jouer !";
		} else {
			playerActiveCanvasText.text = "C'est à " + GameSceneManager.getPlayerTwoPseudo () + " de jouer !";
		}
	}

	private void adjustScore(){
		if (currentPlayer == 1) {
			pOneScore++;
			playerOneScore.GetComponent<Text> ().text = pOneScore.ToString () + " pts";
		} else {
			pTwoScore++;
			playerTwoScore.GetComponent<Text> ().text = pTwoScore.ToString () + " pts";
		}
	}

	private void switchCurrentPlayer(){
		if (currentPlayer == 1) {
			currentPlayer = 2;
		} else if (currentPlayer == 2) {
			currentPlayer = 1;
		}
	}
}


//			Color color = firstPickedCard.GetComponent<Image> ().color;
//			color.a = 0.0f;
//			firstPickedCard.GetComponent<Image> ().color = color;