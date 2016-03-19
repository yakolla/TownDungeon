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
    Button      m_buttonEquip;
    Item m_item;

	void Awake()
	{
		m_xpGuageBox = GetComponentInChildren<GuageBox>();
		m_itemIcon = transform.Find("ImageIcon").GetComponent<Image>();
		m_buttonUpgrade = transform.Find("ImageIcon/ButtonUpgrade").GetComponent<Button>();
        m_buttonEquip = transform.Find("ImageIcon/ButtonEquip").GetComponent<Button>();
        m_selectedImage = transform.Find("ImageSelected").GetComponent<Image>();
		m_initScale = m_itemIcon.transform.localScale;
		m_goalScale = m_initScale;
	}

	public void Init(InventoryPanel	inventoryPanel, Item item)
	{
		m_inventory = inventoryPanel;
		m_itemIcon.sprite = Sprite.Create(item.Icon, new Rect(0, 0, item.Icon.width, item.Icon.height), new Vector2(.5f,.5f));
		
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
        m_buttonEquip.gameObject.SetActive(m_selected);
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

        m_buttonEquip.gameObject.SetActive(m_selected);

    }

	void Update()
	{
		m_itemIcon.transform.localScale = Vector3.Lerp(m_itemIcon.transform.localScale, m_goalScale, Time.deltaTime);
        m_xpGuageBox.Amount(m_item.XP + "/" + m_item.XPToNextLevel, m_item.XP / (float)m_item.XPToNextLevel);
        if (m_item.XP >= m_item.XPToNextLevel)
		{
			m_buttonUpgrade.gameObject.SetActive(true);
            m_xpGuageBox.gameObject.SetActive(false);

        }
		else
		{
			m_buttonUpgrade.gameObject.SetActive(false);
            m_xpGuageBox.gameObject.SetActive(true);
        }
	}

    public void OnClickUpgrade()
    {
        m_item.LevelUp();
    }

    public void OnClickEquip()
    {
        m_inventory.EquipItem(m_item);
    }
}

