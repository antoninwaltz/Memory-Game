using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store the Card data.
/// </summary>
public class CardData : MonoBehaviour {

	public int key;
	public int value;

	// Use this for initialization
	void Start () {
		
	}

	public void setKey(int pKey){
		this.key = pKey;
	}

	public void setValue(int pValue){
		this.value = pValue;
	}

	public void setData(int pKey, int pValue){
		this.key = pKey;
		this.value = pValue;
	}

	public int getKey(){
		return this.key;
	}

	public int getValue(){
		return this.value;
	}

}
