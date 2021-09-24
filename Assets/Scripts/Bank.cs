using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int StartingBalance = 150;
    [SerializeField] int CurrentBalance;
    [SerializeField] TextMeshProUGUI label;

    public int currentBalance { get { return CurrentBalance; } }

    private void Awake()
    {
        CurrentBalance = StartingBalance;
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        CurrentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        CurrentBalance -= Mathf.Abs(amount);
    }

    private void UpdateDisplay()
    {
        label.text = "Gold: " + CurrentBalance;
    
    }






}
