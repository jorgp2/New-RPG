﻿using UnityEngine;
using System.Collections;

public class Interact : MonoBehaviour
{
	public bool Draw;
	public PlayerController PC;
	public virtual void MouseAction(PlayerController play){}
	public virtual void MouseAction(PlayerController play, Vector2 MousePos){}
}
