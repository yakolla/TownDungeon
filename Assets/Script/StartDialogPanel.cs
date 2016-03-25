using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartDialogPanel : MonoBehaviour{

	public void OnClickStart()
    {
        gameObject.SetActive(false);
    }
    
}
