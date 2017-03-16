using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardManager : MonoBehaviour {

	public static BoardManager Instance { set; get; }
	private bool[,] allowedMoves{ set; get; }

	public Piece[,] ChessPieces{ set; get; }
	private Piece selectedPiece;

	private const float fieldSize = 1.0f;
	private const float fieldOffset = 0.5f;

	private int selectionX = -1;
	private int selectionZ = -1;

	public List<GameObject> chessPiecePrefabs;
	private List<GameObject> activeChessPiece;

	private Quaternion white = Quaternion.Euler (-90.0f, -90.0f, 0.0f);
	private Quaternion black = Quaternion.Euler (-90.0f, 90.0f, 0.0f);

	private Material prevMat;
	public Material SelectedMat;

	public int[] EnPassatMove;

	public bool isWhiteTurn = true;

	private Camera cam;
	private Vector3 initCamPos{ set; get; }

	// Use this for initialization
	void Start () {
		Instance = this;
		cam = Camera.main;
		SpawnAllPieces ();
		initCamPos = cam.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateSelection ();
		//DrawChessBoard ();
		if (Input.GetMouseButtonDown(0)) {
			if (selectionX >= 0 && selectionZ >= 0) {
				if (selectedPiece == null) {
					//select
					SelectPiece(selectionX, selectionZ);
				} 
				else {
					//move
					MovePiece(selectionX, selectionZ);
				}
			}
		}
	}

	private void SelectPiece(int x, int y)
	{
		if (ChessPieces[x, y] == null) {
			return;
		}

		if (ChessPieces[x, y].isWhite != isWhiteTurn) {
			return;
		}

		allowedMoves = ChessPieces [x, y].PossibleMoves ();

		bool notLocked = false;
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (allowedMoves[i, j]) {
					notLocked = true;
					break;
				}
			}
		}

		if (!notLocked) {
			return;
		}

		selectedPiece = ChessPieces [x, y];

		prevMat = selectedPiece.GetComponent<MeshRenderer> ().material;
		SelectedMat.mainTexture = prevMat.mainTexture;
		selectedPiece.GetComponent<MeshRenderer> ().material = SelectedMat;

		Highlights.Instance.HighlightMoves (allowedMoves);
	}

	private void MovePiece(int x, int y)
	{
		if (allowedMoves[x, y]) {
			Piece c = ChessPieces [x, y];
			if (c != null && c.isWhite != isWhiteTurn) {
				//Capture piece

				//If it is the king
				if (c.GetType() == typeof(King)) {
					EndGame ();
					return;
				}

				activeChessPiece.Remove(c.gameObject);
				Destroy(c.gameObject);
			}

			if (x == EnPassatMove[0] && y == EnPassatMove[1]) {
				if (isWhiteTurn) {
					c = ChessPieces [x, y - 1];
				} 
				else 
				{
					c = ChessPieces [x, y + 1];
				}
				activeChessPiece.Remove(c.gameObject);
				Destroy(c.gameObject);
			}

			EnPassatMove [0] = -1;
			EnPassatMove [1] = -1;

			if (selectedPiece.GetType() == typeof(Pawn)) {
				if (y == 7) {
					activeChessPiece.Remove(selectedPiece.gameObject);
					Destroy(selectedPiece.gameObject);
					SpawnChessPiece (3, x, y, white);
					selectedPiece = ChessPieces [x, y];
				}
				else if (y == 0) {
					activeChessPiece.Remove(selectedPiece.gameObject);
					Destroy(selectedPiece.gameObject);
					SpawnChessPiece (9, x, y, black);
					selectedPiece = ChessPieces [x, y];
				}
				if (selectedPiece._Y == 1 && y == 3) {
					EnPassatMove [0] = x;
					EnPassatMove [1] = y - 1;
				} else if (selectedPiece._Y == 6 && y == 4) {
					EnPassatMove [0] = x;
					EnPassatMove [1] = y + 1;
				}
			}

			ChessPieces [selectedPiece._X, selectedPiece._Y] = null;
			selectedPiece.transform.position = fieldCenter (x, y);
			selectedPiece.SetPos (x, y);
			if (selectedPiece.tag == "Pawn") {
				selectedPiece.transform.position += new Vector3 (0.0f, 0.2f, 0.0f);
			}
			ChessPieces [x, y] = selectedPiece;
			isWhiteTurn = !isWhiteTurn;
		}

		selectedPiece.GetComponent<MeshRenderer> ().material = prevMat;
		RotateCam ();
		Highlights.Instance.HideHighlights ();
		selectedPiece = null;
	}

	private void SpawnAllPieces()
	{
		activeChessPiece = new List<GameObject> ();
		ChessPieces = new Piece[8, 8];
		EnPassatMove = new int[2]{-1, -1};
		//Bishops, knights and rooks
		for (int i = 0; i < 3; i++) {
			SpawnChessPiece (i, i, 0, white);
			SpawnChessPiece (i, 7-i, 0, white);
			SpawnChessPiece (i+6, i, 7, black);
			SpawnChessPiece (i+6, 7-i, 7, black);

		}

		//Queens
		SpawnChessPiece (3, 3, 0, white);
		SpawnChessPiece (9, 3, 7, black);

		//Kings
		SpawnChessPiece (4, 4, 0, Quaternion.Euler (-90.0f, 0.0f, 0.0f));
		SpawnChessPiece (10, 4, 7, Quaternion.Euler (-90.0f, 0.0f, 0.0f));

		//Pawns
		for (int i = 0; i < 8; i++) {
			SpawnChessPiece (5, i, 1, white);
			SpawnChessPiece (11, i, 6, black);
		}

		for (int i = 16; i < activeChessPiece.Count; i++) {
			activeChessPiece[i].transform.position += new Vector3 (0.0f, 0.2f, 0.0f);
		}
	}

	private void SpawnChessPiece(int i, int x, int y, Quaternion rotate)
	{
		GameObject go = Instantiate (chessPiecePrefabs [i], fieldCenter(x, y), rotate) as GameObject;
		go.transform.SetParent (transform);
		ChessPieces [x, y] = go.GetComponent<Piece> ();
		ChessPieces [x, y].SetPos (x, y);
		activeChessPiece.Add (go);
	}

	private Vector3 fieldCenter(int x, int y)
	{
		Vector3 tmp = Vector3.zero;
		tmp.x += (fieldSize * x) + fieldOffset;
		tmp.z += (fieldSize * y) + fieldOffset;
		return tmp;
	}

	private void DrawChessBoard ()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;
		for (int i = 0; i < 9; i++) 
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);
			for (int j = 0; j < 9; j++) 
			{
				start = Vector3.right * i;
				Debug.DrawLine (start, start + heightLine);	
			}
		}

		// Draw selections
		if (selectionX >= 0 && selectionZ >= 0) {
			Debug.DrawLine (
				Vector3.forward * selectionZ + Vector3.right * selectionX,
				Vector3.forward * (selectionZ + 1) + Vector3.right * (selectionX + 1));
			Debug.DrawLine (
				Vector3.forward * (selectionZ + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionZ + Vector3.right * (selectionX + 1));
		}
	}

	private void UpdateSelection()
	{
		if (!Camera.main) 
		{
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessBoard")))
		{
			selectionX = (int)hit.point.x;
			selectionZ = (int)hit.point.z;
		} 
		else 
		{
			selectionX = -1;
			selectionZ = -1;
		}
	}

	private void RotateCam()
	{
		Transform boardPos = GameObject.Find ("ChessBoard").GetComponent<Transform> ();
		float zdiff = boardPos.position.z - initCamPos.z;
		if (isWhiteTurn) {
			cam.transform.position = initCamPos;
			cam.transform.rotation = Quaternion.Euler (50.0f, 0.0f, 0.0f);
		} else {
			cam.transform.position = initCamPos + new Vector3 (0, 0, zdiff * 2);
			cam.transform.rotation = Quaternion.Euler (50.0f, 180.0f, 0.0f);
		}
	}

	private void EndGame()
	{
		if (isWhiteTurn) {
			Debug.Log ("White team won");
		} else {
			Debug.Log ("Black team won");
		}

		foreach (var item in activeChessPiece) {
			Destroy(item);
		}

		//Rotate camera

		isWhiteTurn = true;
		Highlights.Instance.HideHighlights ();
		SpawnAllPieces ();
	}
}
