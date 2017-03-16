using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

	public override bool[,] PossibleMoves ()
	{
		bool[,] r = new bool[8, 8];

		Piece c;
		int i, j;

		i = _X;
		j = _Y;
		while (true) {
			i--;
			j++;
			if (i < 0 || j >= 8) 
			{
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c == null) {
				r [i, j] = true;
			} 
			else 
			{
				if (isWhite != c.isWhite) {
					r [i, j] = true;	
				}
				break;
			}	
		}

		i = _X;
		j = _Y;
		while (true) {
			i--;
			j--;
			if (i < 0 || j < 0) 
			{
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c == null) {
				r [i, j] = true;
			} 
			else 
			{
				if (isWhite != c.isWhite) {
					r [i, j] = true;	
				}
				break;
			}	
		}

		i = _X;
		j = _Y;
		while (true) {
			i++;
			j--;
			if (i >= 8 || j < 0) 
			{
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c == null) {
				r [i, j] = true;
			} 
			else 
			{
				if (isWhite != c.isWhite) {
					r [i, j] = true;	
				}
				break;
			}	
		}

		i = _X;
		j = _Y;
		while (true) {
			i++;
			j++;
			if (i >= 8 || j >= 8) 
			{
				break;
			}

			c = BoardManager.Instance.ChessPieces [i, j];
			if (c == null) {
				r [i, j] = true;
			} 
			else 
			{
				if (isWhite != c.isWhite) {
					r [i, j] = true;	
				}
				break;
			}	
		}

		return r;
	}
}
