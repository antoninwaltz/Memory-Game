using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour {

	private static Dictionary<string, object> gameParameters;
	public GameObject difficultyDropdown;
	public GameObject pairNumberDropdown;
	public GameObject playerOnePseudoInputField;
	public GameObject playerTwoPseudoInputField;
	public string scene;

	// Use this for initialization
	void Start () {

		gameParameters = new Dictionary<string, object> ();

	}

	public void loadNewGameScene(){

		/**
		 * Difficulty
		 **/
		try
		{
			gameParameters.Add("difficulty",difficultyDropdown.GetComponent<Dropdown>().value);
		}
		catch(ArgumentException)
		{
			gameParameters ["difficulty"] = difficultyDropdown.GetComponent<Dropdown> ().value;
		}

		/**
		 * Pair Number
		 **/
		try
		{
			gameParameters.Add("pairNumber",pairNumberDropdown.GetComponent<Dropdown>().value + 4);
		}
		catch(ArgumentException)
		{
			gameParameters ["pairNumber"] = pairNumberDropdown.GetComponent<Dropdown> ().value + 4;
		}

		/**
		 * Player One Pseudo
		 **/
		try
		{
			gameParameters.Add("playerOnePseudo",playerOnePseudoInputField.GetComponent<InputField>().text);
		}
		catch(ArgumentException)
		{
			gameParameters ["playerOnePseudo"] = playerOnePseudoInputField.GetComponent<InputField>().text;
		
		}

		/**
		 * Player Two Pseudo
		 **/
		try
		{
			gameParameters.Add("playerTwoPseudo",playerTwoPseudoInputField.GetComponent<InputField>().text);
		}
		catch(ArgumentException)
		{
			gameParameters ["playerTwoPseudo"] = playerTwoPseudoInputField.GetComponent<InputField>().text;

		}
			
		SceneManager.LoadSceneAsync (scene);
			
	}

	public static int getDifficulty(){
		if (gameParameters.ContainsKey("difficulty")) {
			return (int)gameParameters ["difficulty"];
		} else {
			return -1;
		}
	}

	public static int getPairNumber(){
		if (gameParameters.ContainsKey ("pairNumber")) {
			return (int)gameParameters ["pairNumber"];
		} else {
			return -1;
		}
	}

	public static string getPlayerOnePseudo(){
		if (gameParameters.ContainsKey ("playerOnePseudo")) {
			if ((string)gameParameters ["playerOnePseudo"] != "") {
				return (string)gameParameters ["playerOnePseudo"];
			} else {
				return "Joueur 1";
			}
		} else {
			return "Joueur 1";
		}
	}


	public static string getPlayerTwoPseudo(){
		if (gameParameters.ContainsKey ("playerTwoPseudo")) {
			if ((string)gameParameters ["playerTwoPseudo"] != "") {
				return (string)gameParameters ["playerTwoPseudo"];
			} else {
				return "Joueur 2";
			}
		} else {
			return "Joueur 2";
		}
	}
}

