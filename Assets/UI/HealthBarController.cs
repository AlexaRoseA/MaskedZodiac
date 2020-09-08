using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider healthbar;
    public int currentHP = 100;
    void Awake()
    {
        healthbar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = currentHP;
    }

    public void changeHP(int dHP)
    {
        currentHP += dHP;
    }

    public void removeHP(int dHP)
    {
        currentHP -= dHP;
    }
}
