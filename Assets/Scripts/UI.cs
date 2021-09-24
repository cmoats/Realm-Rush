using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    TextMeshProUGUI label;
    Bank gold;

    private void Start()
    {
        gold = FindObjectOfType<Bank>();
        label = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        label.text = "Gold: " + gold;
    }
}
