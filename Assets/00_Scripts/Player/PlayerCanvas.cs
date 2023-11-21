using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCanvas : MonoBehaviour
{

    private PlayerHP playerHP; // Reference to the script with the variable you want to display
    private TextMeshProUGUI hpField;
    // Start is called before the first frame update
    void Start()
    {
       hpField = GetComponent<TextMeshProUGUI>();
       playerHP = GetComponentInParent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        hpField.text = "HP: " + playerHP.health.ToString();
    }
}
