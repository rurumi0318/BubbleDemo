using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagonal;

namespace BubbleGame
{
	public abstract class Brush : ScriptableObject
	{
		public string brushName;

		public abstract void PaintOn(MapData map, Hex hex, GameManager gm);
	}
}