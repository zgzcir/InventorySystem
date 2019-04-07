using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int BasicStrength { get; set; } = 10;
    public int BasicIntellect { get; set; } = 9;
    public int BasicAgility { get; set; } = 4;
    public int BasicStamina { get; set; } = 1;
    public int BasicDamage { get; set; } = 1;
    private int coinAmount = 200;
    public int CoinAmount
    {
        get
        {
            return coinAmount;
        }
        set
        {
            coinAmount = value;
            UpdateUI();
        }
    }
    private Text coinText;
    void Start()
    {
        coinText = GameObject.Find("coinText").GetComponentInChildren<Text>();
        UpdateUI();
    }
    public bool Cosume(int amount)
    {
        if (CoinAmount >= amount)
        {
            CoinAmount -= amount;
            UpdateUI();

            return true;
        }
        else
        {
            return false;
        }
    }
    public void Earn(int amount)
    {
        CoinAmount += amount;
        UpdateUI();
    }
    private void UpdateUI()
    {
        coinText.text = CoinAmount.ToString();

    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.S))
        {
            int id = Random.Range(14, 18);
            Knapsack.Instance.StoreItem(id);
        }
    }
    public void SavePlayer()
    {
        PlayerPrefs.SetInt("coinAmount", coinAmount);
    }
    public void LoadPlayer()
    {
        if (!PlayerPrefs.HasKey("coinAmount")) ;
      CoinAmount=PlayerPrefs.GetInt("coinAmount");
    }

}
