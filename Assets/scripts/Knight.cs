using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {

	public override bool[,] PossibleMoves ()
	{
		bool[,] r = new bool[8, 8];

		KnightMove(_X-1, _Y+2, ref r);
		KnightMove(_X+1, _Y+2, ref r);
		KnightMove(_X+2, _Y+1, ref r);
		KnightMove(_X+2, _Y-1, ref r);
		KnightMove(_X-2, _Y-1, ref r);
		KnightMove(_X-2, _Y+1, ref r);
		KnightMove(_X+1, _Y-2, ref r);
		KnightMove(_X-1, _Y-2, ref r);

		return r;
	}

	public void KnightMove (int x, int y, ref bool[,] r)
	{
		Piece c;
		if (x >= 0 && x < 8 && y >= 0 && y < 8) {
			c = BoardManager.Instance.ChessPieces [x, y];
			if (c == null) {
				r [x, y] = true;
			} else if (isWhite != c.isWhite) {
				r [x, y] = true;
			}
		}
	}
}
