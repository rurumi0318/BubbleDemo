using System.Collections;
using System.Collections.Generic;
using Hexagonal;
using UnityEngine;

namespace BubbleGame
{
	[CreateAssetMenu(fileName = "EraserBrush", menuName = "BubbleGame/EraserBrush")]
	public class EraserBrush : Brush
	{
		public override void PaintOn(MapData map, Hex hex, GameManager gm)
		{
			map.Remove(hex);
			gm.ClearBubble(hex);
		}
	}
}