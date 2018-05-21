using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA_script : MonoBehaviour {

	public string DNA;

	private string[] cellType2Name = { "BlueCell", "RedCell", "GreenCell" }; 

	private readonly int geneLength = 3;

	// Use this for initialization
	void Start () {
	}
	
	private void parseDNA(string DNA) {
		int ctype;
		int cposX;
		int cposY;

		for (int i = 0; i < DNA.Length -2; i = i + geneLength) {
			string gene = DNA.Substring (i, geneLength);
			ctype = char2Int(gene [0]);
			cposX = char2Int(gene [1]);
			cposY = char2Int(gene [2]);
			createCell (ctype, cposX, cposY);
		}
			
	}

	public void getDNA() {
	}
	public void buildOrganism() {
		parseDNA (DNA);
	}

	private int char2Int(char c) {
		return (int)char.GetNumericValue (c);
	}

	private void createCell(int type, int x, int y) {
		Object cell = Resources.Load ("cells/" + cellType2Name [type] + "_prefab");
		Vector2 pos = new Vector2 (this.transform.position.x + x, this.transform.position.y + y);
		Instantiate (cell, pos, Quaternion.identity);
	}

	private void changeAt(string str, char change, int at) {
		str [at] = change;
		return str;
	}
}
