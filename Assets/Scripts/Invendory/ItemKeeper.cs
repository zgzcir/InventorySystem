using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemKeeper : MonoBehaviour
{


    public Item Item { get; set; } = null;
    public int Amount;
    #region  ui
    private Image itemImage;
    private Text amountText;
    private Vector3 targetScale = Vector3.one;
    public Vector3 animationScale = new Vector3(1.3f, 1.3f, 1.3f);
    private float smoothing = 5f;
    public bool IsEmpty
    {
        get
        {
            return Item == null ? true : false;
        }
    }
    public Image ItemImage
    {
        get
        {
            if (itemImage == null)
            {
                itemImage = GetComponent<Image>();
            }
            return itemImage;
        }
    }
    protected Text AmountText
    {
        get
        {
            if (amountText == null)
            {
                amountText = GetComponentInChildren<Text>();

            }
            return amountText;
        }
    }
    #endregion
    #region  data
    public void SetItemKeeper(Item item, int amount = 1)
    {
        ChangeAnimation();
        Item = item;
        this.Amount = amount;
        ItemImage.sprite = Resources.Load<Sprite>(item.SpritePath);
        UpdateUI();
    }
    public void AddAmount(int amount = 1)
    {
        ChangeAnimation();
        this.Amount += amount;
        UpdateUI();
    }
    public virtual void ReduceAmount(int amount = 1)
    {
        this.Amount -= amount;
        if (Amount == 0)
        {
            Item = null;
        }
        UpdateUI();
    }
    public void SetAmount(int amount)
    {
        ChangeAnimation();
        this.Amount = amount;
        UpdateUI();
    }
    private void ChangeAnimation()
    {
        transform.localScale = animationScale;
    }
    protected virtual void UpdateUI()
    {   Character.Instance.UpdatePropertyText();
            Show();
        if (Amount == 0)
        {
            // Destroy(gameObject);
            itemImage.sprite=null;
            Hide();
            return;
        }
        Show();
        if (Item.Capacity > 1)
            AmountText.text = Amount.ToString();
        else
        {
            AmountText.text = "";
        }
             
    }
    public void Show()
    {   
    
        gameObject.SetActive(true);

    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPotion(Vector2 position)
    {
        transform.localPosition = position;
    }
    private void Update()
    {
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, smoothing * Time.deltaTime);
            if (Mathf.Abs(transform.localScale.x - targetScale.x) < 0.01f)
            {
                transform.localScale = targetScale;
            }
        }
    }

    public void ExChange(ItemKeeper targetItemKeeper)
    {
        Item tempItem = targetItemKeeper.Item;
        int tempAmount = targetItemKeeper.Amount;
        targetItemKeeper.SetItemKeeper(Item, Amount);
        SetItemKeeper(tempItem, tempAmount);
    }
    #endregion
}
