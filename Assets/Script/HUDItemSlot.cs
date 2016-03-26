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
    Button m_buttonUnEquip;
    Item m_item;
    bool m_isEquip = false;

	void Awake()
	{
		m_xpGuageBox = GetComponentInChildren<GuageBox>();
		m_itemIcon = transform.Find("ImageIcon").GetComponent<Image>();
		m_buttonUpgrade = transform.Find("ImageIcon/ButtonUpgrade").GetComponent<Button>();
        m_buttonEquip = transform.Find("ImageIcon/ButtonEquip").GetComponent<Button>();
        m_buttonUnEquip = transform.Find("ImageIcon/ButtonUnEquip").GetComponent<Button>();
        m_selectedImage = transform.Find("ImageSelected").GetComponent<Image>();
		m_initScale = m_itemIcon.transform.localScale;
		m_goalScale = m_initScale;
	}

	public void Init(InventoryPanel	inventoryPanel, Item item, bool equip)
	{
		m_inventory = inventoryPanel;
		m_itemIcon.sprite = Sprite.Create(item.Icon, new Rect(0, 0, item.Icon.width, item.Icon.height), new Vector2(.5f,.5f));
		
		m_item = item;
        m_isEquip = equip;
        
    }

    public Item Item
    {
        get { return m_item; }
    }

    void toggleEquipButton(bool active)
    {
        m_buttonEquip.gameObject.SetActive(!active);
        m_buttonUnEquip.gameObject.SetActive(active);
    }

	public void OnPointerDown(PointerEventData eventData)
	{
        if (m_inventory.SelectedItemSlot != null && m_inventory.SelectedItemSlot != this)
            m_inventory.SelectedItemSlot.OnUnfocus();

        if (!m_selected == true)
			OnPressed();
		else
			OnUnfocus();
		
		m_inventory.SelectedItemSlot = this;

	}

	public void OnPressed()
	{
		m_selected = true;
		m_goalScale = m_initScale*(1f-m_scaleDown);
        toggleEquipButton(m_isEquip);
        m_inventory.PreComputeItemStats(m_item, !m_isEquip);
    }

	public void OnUnfocus()
	{
		m_itemIcon.transform.localScale = m_goalScale = m_initScale;
		m_selectedImage.enabled = false;
		m_selected = false;
        m_buttonEquip.gameObject.SetActive(false);
        m_buttonUnEquip.gameObject.SetActive(false);
        m_inventory.PreComputeItemStats(null, !m_isEquip);
    }

	public void OnPointerUp(PointerEventData eventData)
	{

		m_selectedImage.enabled = m_selected;

        if (m_selected == true)
        {
            m_itemIcon.transform.localScale = m_initScale;
            m_goalScale = m_initScale * (1f);
        }
        else
        {
            OnUnfocus();
        }

    }

	void Update()
	{
		m_itemIcon.transform.localScale = Vector3.Lerp(m_itemIcon.transform.localScale, m_goalScale, Time.deltaTime);
        m_xpGuageBox.Amount(m_item.XP + "/" + m_item.XPToNextLevel, m_item.XP / (float)m_item.XPToNextLevel);

        if (m_item.XP >= m_item.XPToNextLevel)
        {
            if (m_selected == true)
                m_buttonUpgrade.gameObject.SetActive(true);
            else
                m_buttonUpgrade.gameObject.SetActive(false);

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
        m_inventory.LevelUpItem(m_item);
        m_inventory.PreComputeItemStats(m_item, !m_isEquip);
    }

    public void OnClickEquip()
    {
        if (m_inventory.EquipItem(m_item))
        {
            m_isEquip = true;
            toggleEquipButton(m_isEquip);
            m_inventory.PreComputeItemStats(null, !m_isEquip);
        }
    }

    public void OnClickUnEquip()
    {
        if (m_inventory.UnEquipItem(m_item))
        {
            m_isEquip = false;
            toggleEquipButton(m_isEquip);
            m_inventory.PreComputeItemStats(null, !m_isEquip);
        }
    }
}

