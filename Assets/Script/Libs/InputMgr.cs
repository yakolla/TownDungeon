using UnityEngine;
using System.Collections;


public class InputMgr {
	
	public static void UpdateTouch(System.Action<int, Vector3, TouchPhase> callback)
	{
		TouchPhase phase = TouchPhase.Began;
		int touchedCount = 0;
		Vector3 touchPos = Vector3.zero;
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) == true)
		{
			phase = TouchPhase.Began;
			touchedCount = 1;
		}
		else if (Input.GetMouseButton(0) == true)
		{
			phase = TouchPhase.Moved;
			touchedCount = 1;
		}
		else if (Input.GetMouseButtonUp(0) == true)
		{
			phase = TouchPhase.Ended;
			touchedCount = 1;
		}
		
		if (touchedCount > 0)
			touchPos = Input.mousePosition;
		
		#else
		touchedCount = Input.touchCount;
		if (touchedCount > 0)
		{
			phase = Input.GetTouch (0).phase;
			touchPos = Input.GetTouch(0).position;
		}
		#endif
		
		callback(touchedCount, touchPos, phase);
	}

	public static GameObject PickUpObject(Vector3 screenPos, int layerMask)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, float.PositiveInfinity, layerMask)){
			return hit.transform.gameObject;
		}

		return null;
	}
}
