using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

	public override bool[,] PossibleMoves ()
	{
		bool[,] r = new bool[8, 8];
		Piece c;
		int i, j;

		i = _X - 1;
		j = _Y + 1;
		if (_Y != 7) {
			for (int k = 0; k < 3; k++) {
				if (j >= 0 || i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					if (c == null) {
						r [i, j] = true;
					} else if (isWhite != c.isWhite) {
						r [i, j] = true;
					}
				}
				i++;
			}
		}

		i = _X - 1;
		j = _Y - 1;
		if (_Y != 0) {
			for (int k = 0; k < 3; k++) {
				if (j >= 0 || i < 8) {
					c = BoardManager.Instance.ChessPieces [i, j];
					if (c == null) {
						r [i, j] = true;
					} else if (isWhite != c.isWhite) {
						r [i, j] = true;
					}
				}
				i++;
			}
		}

		if (_X != 0) {
			c = BoardManager.Instance.ChessPieces [_X - 1, _Y];
			if (c == null) {
				r [_X - 1, _Y] = true;
			} else if (isWhite != c.isWhite) {
				r [_X - 1, _Y] = true;
			}
		}

		if (_X != 7) {
			c = BoardManager.Instance.ChessPieces [_X + 1, _Y];
			if (c == null) {
				r [_X + 1, _Y] = true;
			} else if (isWhite != c.isWhite) {
				r [_X + 1, _Y] = true;
			}
		}

		return r;
	}
}
