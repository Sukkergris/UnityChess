using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

	public override bool [,] PossibleMoves()
	{
		bool[,] r = new bool[8, 8];
		Piece c, c2;

		int[] enPass = BoardManager.Instance.EnPassatMove;

		if (isWhite) 
		{
			if (_X != 0 && _Y != 7) 
			{
				if (enPass[0] == _X - 1 && enPass[1] == _Y + 1) {
					r [_X - 1, _Y + 1] = true;
				}
				c = BoardManager.Instance.ChessPieces [_X - 1, _Y + 1];
				if (c != null && !c.isWhite) {
					r [_X - 1, _Y + 1] = true;
				}
			}

			if (_X != 7 && _Y != 7) 
			{
				if (enPass[0] == _X + 1 && enPass[1] == _Y + 1) {
					r [_X + 1, _Y + 1] = true;
				}
				c = BoardManager.Instance.ChessPieces [_X + 1, _Y + 1];
				if (c != null && !c.isWhite) {
					r [_X + 1, _Y + 1] = true;
				}
			}

			if (_Y != 7) 
			{
				c = BoardManager.Instance.ChessPieces [_X, _Y + 1];
				if (c == null) {
					r [_X, _Y + 1] = true;
				}
			}
				
			if (_Y == 1) 
			{
				c = BoardManager.Instance.ChessPieces [_X, _Y + 1];
				c2 = BoardManager.Instance.ChessPieces [_X, _Y + 2];
				if (c2 == null && c == null) {
					r [_X, _Y + 1] = true;
					r [_X, _Y + 2] = true;
				}
			}
		} 
		else 
		{
			if (_X != 0 && _Y != 0) 
			{
				if (enPass[0] == _X - 1 && enPass[1] == _Y - 1) {
					r [_X - 1, _Y - 1] = true;
				}
				c = BoardManager.Instance.ChessPieces [_X - 1, _Y - 1];
				if (c != null && c.isWhite) {
					r [_X - 1, _Y - 1] = true;
				}
			}
			//diag right
			if (_X != 7 && _Y != 0) 
			{
				if (enPass[0] == _X + 1 && enPass[1] == _Y - 1) {
					r [_X + 1, _Y - 1] = true;
				}
				c = BoardManager.Instance.ChessPieces [_X + 1, _Y - 1];
				if (c != null && c.isWhite) {
					r [_X + 1, _Y - 1] = true;
				}
			}

			if (_Y != 0) 
			{
				c = BoardManager.Instance.ChessPieces [_X, _Y - 1];
				if (c == null) {
					r [_X, _Y - 1] = true;	
				}
			}
				
			if (_Y == 6) {
				c = BoardManager.Instance.ChessPieces [_X, _Y - 1];
				c2 = BoardManager.Instance.ChessPieces [_X, _Y - 2];
				if (c2 == null && c == null) {
					r [_X, _Y - 1] = true;
					r [_X, _Y - 2] = true;
				}
			}
		}
		return r;
	}
}
