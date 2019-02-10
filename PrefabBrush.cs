using System.Collections;
using System.Collections.Generic;
using Hexagonal;
using UnityEngine;

namespace BubbleGame
{
	[CreateAssetMenu(fileName = "PrefabBrush", menuName = "BubbleGame/PrefabBrush")]
	public class PrefabBrush : Brush
	{
		public BubbleType bubbleType;

		private void OnEnable()
		{
			if (bubbleType != null)
			{
				brushName = bubbleType.name;
			}
		}

		public override void PaintOn(MapData map, Hex hex, GameManager gm)
		{
			map.Set(hex, brushName);
			gm.CreateBubbleOn(hex, brushName);
		}
	}
}