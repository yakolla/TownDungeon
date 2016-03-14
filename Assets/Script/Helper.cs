using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Helper {

	public const int XPBlock = 5;
	public static Rect MapArea = new Rect(-25, -25, 25, 25);
	public static int LayerMaskHero = LayerMask.NameToLayer("Hero");
	public static int LayerMaskMob = LayerMask.NameToLayer("Mob");
	public static int LayerMaskBuilding = LayerMask.NameToLayer("Building");
	public static int LayerMaskItem = LayerMask.NameToLayer("ItemBox");
	public const int ItemCols = 4;

	public static Sprite Photo(GameObject target)
	{
		return GameObject.Find("PhotoIconCamera").GetComponent<PhotoIconCamera>().Photo(target);
	}

	public static ItemBoxs ItemBoxs
	{
		get {return GameObject.Find("ItemBoxs").GetComponent<ItemBoxs>();}
	}
}
