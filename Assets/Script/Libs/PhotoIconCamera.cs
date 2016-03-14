using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhotoIconCamera : MonoBehaviour {

	Camera	m_camera;
	[SerializeField]
	int	m_screenShotWidth = (int)(160*1.5f);
	[SerializeField]
	int m_screenShotHeight = (int)(100*1.5f);

	public void Awake()
	{
		m_camera = GetComponent<Camera>();
	}

	public Sprite Photo(GameObject target)
	{
		Vector3 backupPos = m_camera.gameObject.transform.position;
		Quaternion backupRotation = m_camera.gameObject.transform.rotation;
		m_camera.gameObject.transform.parent = target.transform;
		m_camera.gameObject.transform.localPosition = backupPos;
		m_camera.gameObject.transform.localRotation = backupRotation;
		
		int width = m_screenShotWidth;//Screen.width;
		int height = m_screenShotHeight;//Screen.height;
		RenderTexture rt = new RenderTexture(width, height, 0); 
		m_camera.targetTexture = rt; 
		m_camera.Render(); 
		
		RenderTexture.active = rt; 
		Texture2D icon = new Texture2D(width, height, TextureFormat.RGB24, false); 
		icon.ReadPixels(new Rect(0, 0, width, height), 0, 0); 
		icon.Apply();
		
		m_camera.targetTexture = null; 
		RenderTexture.active = null; // JC: added to avoid errors 
		Destroy(rt); 
		
		m_camera.gameObject.transform.parent = null;
		m_camera.gameObject.transform.position = backupPos;
		m_camera.gameObject.transform.rotation = backupRotation;
		
		int pw = (int)(width * m_camera.rect.width);
		int ph = (int)(height * m_camera.rect.height);
		return Sprite.Create(icon, new Rect(0, 0, pw, ph), new Vector2(0.0f, 0.0f));
	}

}
