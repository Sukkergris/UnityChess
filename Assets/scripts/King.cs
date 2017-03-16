using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

	private bool castle = true;

	public void SetCastleFalse()
	{
		castle = false;
	}

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

		if (_X == 4 && _Y == 0 && isWhite && castle) {
			bool[] tmp = new bool[]{false, false, false};
			c = BoardManager.Instance.ChessPieces [0, _Y];
			if (c.GetType () == typeof(Rook) && c.isWhite) 
			{
				for (int k = 1; k < 4; k++) 
				{
					c = BoardManager.Instance.ChessPieces [k, _Y];
					if (c == null) {
						tmp [k - 1] = true;
					}
				}
				if (tmp[0] == true && tmp[1] == true && tmp[2] == true) {
					r [_X - 2, _Y] = true;					
				}
			}	
		}

		if (_X == 4 && _Y == 0 && isWhite && castle) {
			bool[] tmp = new bool[]{false, false};
			c = BoardManager.Instance.ChessPieces [7, _Y];
			if (c.GetType () == typeof(Rook) && c.isWhite) 
			{
				for (int k = 1; k < 3; k++) 
				{
					c = BoardManager.Instance.ChessPieces [_X + k, _Y];
					if (c == null) {
						tmp [k - 1] = true;
					}
				}
				if (tmp[0] == true && tmp[1] == true) {
					r [_X + 2, _Y] = true;	
				}
			}	
		}

		if (_X == 4 && _Y == 7 && !isWhite && castle) {
			bool[] tmp = new bool[]{false, false, false};
			c = BoardManager.Instance.ChessPieces [0, _Y];
			if (c.GetType () == typeof(Rook) && !c.isWhite) 
			{
				for (int k = 1; k < 4; k++) 
				{
					c = BoardManager.Instance.ChessPieces [k, _Y];
					if (c == null) {
						tmp [k - 1] = true;
					}
				}
				if (tmp[0] == true && tmp[1] == true && tmp[2] == true) {
					r [_X - 2, _Y] = true;		
				}
			}	
		}

		if (_X == 4 && _Y == 7 && !isWhite && castle) {
			bool[] tmp = new bool[]{false, false};
			c = BoardManager.Instance.ChessPieces [7, _Y];
			if (c.GetType () == typeof(Rook) && !c.isWhite) 
			{
				for (int k = 1; k < 3; k++) 
				{
					c = BoardManager.Instance.ChessPieces [_X + k, _Y];
					if (c == null) {
						tmp [k - 1] = true;
					}
				}
				if (tmp[0] == true && tmp[1] == true) {
					r [_X + 2, _Y] = true;		
				}
			}	
		}

		return r;
	}
}
