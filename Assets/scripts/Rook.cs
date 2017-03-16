using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece {

	public override bool[,] PossibleMoves ()
	{
		bool[,] r = new bool[8, 8];
		Piece c;

		//right
		int i = _X;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, _Y];
			if (c == null) {
				r [i, _Y] = true;
			} 
			else 
			{
				if (c.isWhite != isWhite) {
					r [i, _Y] = true;
				}

				break;
			}
		}

		//left
		i = _X;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, _Y];
			if (c == null) {
				r [i, _Y] = true;
			} 
			else 
			{
				if (c.isWhite != isWhite) {
					r [i, _Y] = true;
				}

				break;
			}
		}

		//up
		i = _Y;
		while (true) {
			i++;
			if (i >= 8) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [_X, i];
			if (c == null) {
				r [_X, i] = true;
			} 
			else 
			{
				if (c.isWhite != isWhite) {
					r [_X, i] = true;
				}

				break;
			}
		}

		//up
		i = _Y;
		while (true) {
			i--;
			if (i < 0) {
				break;
			}

			c = BoardManager.Instance.ChessPieces [_X, i];
			if (c == null) {
				r [_X, i] = true;
			} 
			else 
			{
				if (c.isWhite != isWhite) {
					r [_X, i] = true;
				}

				break;
			}
		}

		return r;
	}
}
