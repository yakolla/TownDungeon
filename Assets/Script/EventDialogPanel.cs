using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventDialogPanel : MonoBehaviour{

	public void OnClickClose()
    {
        gameObject.SetActive(false);
    }
    
}
