using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlights : MonoBehaviour {

	public static Highlights Instance { set; get; }

	public GameObject highlightPre;
	private List<GameObject> highlights;

	private void Start()
	{
		Instance = this;
		highlights = new List<GameObject> ();
	}

	private GameObject GetHiglightObject()
	{
		GameObject go = highlights.Find (g => !g.activeSelf);
		if (go == null) 
		{
			go = Instantiate (highlightPre);
			highlights.Add (go);
		}

		return go;
	}

	public void HighlightMoves(bool[,] moves)
	{
		for (int i = 0; i < 8; i++) 
		{
			for (int j = 0; j < 8; j++) 
			{
				if (moves[i, j]) {
					GameObject go = GetHiglightObject ();
					go.SetActive (true);
					go.transform.position = new Vector3 (i + 0.5f, 0, j + 0.5f);
				}
			}
		}
	}

	public void HideHighlights()
	{
		foreach (GameObject item in highlights) 
		{
			item.SetActive (false);
		}
	}

}
