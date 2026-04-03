using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    private TMPro.TextMeshProUGUI ammoCountUI;

    public PlayerShoot2D PS;

    private void Start()
    {
        ammoCountUI = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        ammoCountUI.text = PS.ammoCount.ToString();
    }
}
