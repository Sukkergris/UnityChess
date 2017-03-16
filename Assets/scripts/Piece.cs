using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

	public int _X{set; get;}
	public int _Y{set; get;}
	public bool isWhite;

	public void SetPos(int x, int y)
	{
		_X = x;
		_Y = y;
	}

	public virtual bool[,] PossibleMoves()
	{
		return new bool[8, 8];
	}

}
