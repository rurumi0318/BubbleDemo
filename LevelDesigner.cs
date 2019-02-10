using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BitStrap;
using Hexagonal;

namespace BubbleGame
{
	public class LevelDesigner : MonoBehaviour
	{
		public event System.Action<string> OnChangeBrush;
		public event System.Action<string> OnSaveFailed;
		public event System.Action OnFinish;

		[SerializeField]
		GameSetting gameSetting;

		[SerializeField]
		GameManager gm;

		[SerializeField]
		BubbleCoordinate bubbleCoordinate;

		[SerializeField]
		Brush[] brushes;
		
		Brush currentBrush;

		LocalLevelDataProvider localLevel;

		public void EnableDesignerMode()
		{
			gm.ClearBubbles();

			LocalLevelDataProvider loadedLevel = LocalLevelDataProvider.Load(gameSetting.GetDefaultSavePath());

			if (loadedLevel == null)
			{
				localLevel = ScriptableObject.CreateInstance<LocalLevelDataProvider>();
				localLevel.map = MapData.Create();
			}
			else
			{
				localLevel = loadedLevel;
				gm.CreateBubblesByLevelData(localLevel);
			}
			
			ChangeBrush(brushes[0]);
		}

		public void NextBrush()
		{
			var currentIndex = System.Array.IndexOf<Brush>(brushes, currentBrush);
			var nextIndex = (currentIndex + 1) % brushes.Length;
			ChangeBrush(brushes[nextIndex]);
		}

		public void PreviousBrush()
		{
			var currentIndex = System.Array.IndexOf<Brush>(brushes, currentBrush);
			var previousIndex = (currentIndex + (brushes.Length - 1)) % brushes.Length;
			ChangeBrush(brushes[previousIndex]);
		}

		public void OnDetectInputPoint(Vector3 worldPosition)
		{
			Hex hex = bubbleCoordinate.WorldPositionToHex(worldPosition);

			// use oddr coordinate to check if the hexagonal is in the map
			var oddr = BubbleCoordinate.AxialToOddr(hex);

			if (BubbleCoordinate.ValidateOddrCoordinate(gameSetting, (int)oddr.x, (int)oddr.y))
			{
				currentBrush.PaintOn(localLevel.map, hex, gm);
			}
		}

		public void Save()
		{
			// TODO: validate map
			// OnSaveFailed

			localLevel.Save(gameSetting.GetDefaultSavePath());

			Destroy(localLevel);
			gm.ClearBubbles();

			if (OnFinish != null)
			{
				OnFinish();
			}
		}

		public void Cancel()
		{
			Destroy(localLevel);
			gm.ClearBubbles();

			if (OnFinish != null)
			{
				OnFinish();
			}
		}

		void ChangeBrush(Brush brush)
		{
			currentBrush = brush;

			if (OnChangeBrush != null)
			{
				OnChangeBrush(currentBrush.brushName);
			}
		}
	}
}
