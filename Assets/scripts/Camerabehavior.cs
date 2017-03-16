using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Camerabehavior : MonoBehaviour {

	public Transform target;
	private Vector3 curPos;
	private Vector3 rotW;
	private Vector3 rotB;
	float zdiff;
	float speed;

	private void Start()
	{
		speed = 15f;
		curPos = this.transform.position;
		rotW = curPos;
		zdiff = target.position.z - curPos.z;
		rotB = new Vector3 (rotW.x, rotW.y, rotW.z + (zdiff * 2));
	}

	private void Update() {
		curPos = this.transform.position;
		transform.LookAt(target);
		if (BoardManager.Instance.isWhiteTurn) {
			if (Vector3.Distance(curPos, rotW) < 0.05f) 
			{
				curPos = rotW;
				this.transform.position = curPos;
			} 
			else 
			{
				Rotate (CalcSpeed(rotW));	
			}
		}
		else {
			if (Vector3.Distance(curPos, rotB) < 0.05f) 
			{
				curPos = rotB;
				this.transform.position = curPos;
			} 
			else 
			{
				Rotate (CalcSpeed(rotB));
			}
		}
	}

	private void Rotate(float speed)
	{
		transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
	}

	private float CalcSpeed(Vector3 dest)
	{
		float maxSpeed = 150f;
		float minSpeed = 20f;

		if (BoardManager.Instance.isWhiteTurn) 
		{
			//Black to White
			if (curPos.z >= dest.z + zdiff) 
			{
				//accelerate
				speed = speed * 1.05f;
			} 
			else 
			{
				//decelerate
				speed = speed * 0.975f;
			}
		} 
		else 
		{
			//White to Black
			if (curPos.z <= dest.z - zdiff) 
			{
				//accelerate
				speed = speed * 1.05f;
			} 
			else 
			{
				//decelerate	
				speed = speed * 0.975f;
			}
		}

		if (speed > maxSpeed) {
			speed = maxSpeed;
			return speed;
		}

		if (speed < minSpeed) {
			speed = minSpeed;
			return speed;
		}
			
		return speed;
	}
}
