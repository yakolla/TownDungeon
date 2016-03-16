using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDItemSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

	Image	m_selectedImage;
	Image	m_itemIcon;
	bool	m_selected = false;
	Vector3 m_initScale;
	Vector3	m_goalScale;
	float 	m_scaleDown = 0.2f;
	InventoryPanel	m_inventory;
	GuageBox	m_xpGuageBox;
	Button		m_buttonUpgrade;
	Item m_item;

	void Awake()
	{
		m_xpGuageBox = GetComponentInChildren<GuageBox>();
		m_itemIcon = transform.Find("ImageIcon").GetComponent<Image>();
		m_buttonUpgrade = transform.Find("ImageIcon/ButtonUpgrade").GetComponent<Button>();
		m_selectedImage = transform.Find("ImageSelected").GetComponent<Image>();
		m_initScale = m_itemIcon.transform.localScale;
		m_goalScale = m_initScale;
	}

	public void Init(InventoryPanel	inventoryPanel, Item item)
	{
		m_inventory = inventoryPanel;
		m_itemIcon.sprite = Sprite.Create(item.Icon, new Rect(0, 0, item.Icon.width, item.Icon.height), new Vector2(.5f,.5f));
		m_xpGuageBox.Amount(item.StatsProp.GetValue(StatsPropType.XP) + "/" + item.StatsProp.MaxXP, item.StatsProp.GetValue(StatsPropType.XP)/item.StatsProp.MaxXP);
		m_item = item;

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!m_selected == true)
			OnPressed();
		else
			OnUnfocus();

		if (m_inventory.SelectedItemSlot != null && m_inventory.SelectedItemSlot != this)
			m_inventory.SelectedItemSlot.OnUnfocus();
		
		m_inventory.SelectedItemSlot = this;

	}

	public void OnPressed()
	{
		m_selected = true;
		m_goalScale = m_initScale*(1f-m_scaleDown);
	}

	public void OnUnfocus()
	{
		m_itemIcon.transform.localScale = m_goalScale = m_initScale;
		m_selectedImage.enabled = false;
		m_selected = false;
	}

	public void OnPointerUp(PointerEventData eventData)
	{

		m_selectedImage.enabled = m_selected;

		if (m_selected == true)
		{
			m_itemIcon.transform.localScale = m_initScale;
			m_goalScale = m_initScale*(1f);
		}
		else
			OnUnfocus();

	}

	void Update()
	{
		m_itemIcon.transform.localScale = Vector3.Lerp(m_itemIcon.transform.localScale, m_goalScale, Time.deltaTime);

		if (m_item.StatsProp.GetValue(StatsPropType.XP) >= m_item.StatsProp.MaxXP)
		{
			m_buttonUpgrade.enabled = true;
			m_xpGuageBox.enabled = false;
		}
		else
		{
			m_buttonUpgrade.enabled = false;
			m_xpGuageBox.enabled = true;
		}
	}

}

