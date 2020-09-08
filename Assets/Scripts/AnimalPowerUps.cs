using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalPowerUps : MonoBehaviour
{
    SpriteRenderer spriteRend;
    MainPlayerMovement movementScript;
    public GameObject mask;
    public Image image;

    public Sprite maskCur;
    public bool sneak = false;

    private float movespeedOG;
    // Start is called before the first frame update
    void Start()
    {
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        movespeedOG = 5f;
        movementScript = gameObject.GetComponent<MainPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //currentMask = SpriteRen;
        maskCur = movementScript.currentMask;
        image.sprite = maskCur;

        switch (maskCur.name)
        {
            case "CatMask":
                if (Input.GetButtonDown("Cat Sneak"))
                {
                    sneak = !sneak;
                }
                CatMask();
                break;
            default:
                sneak = false;
                CatMask();
                break;
        }
    }

    void CatMask()
    {
        if (sneak)
        {
         movementScript.moveSpeed = 2f;
         setAlpha(0.5f);
        }
        else
        {
            movementScript.moveSpeed = movespeedOG;
            setAlpha(1f);
        }
    }

    public void setAlpha(float alpha)
    {
        SpriteRenderer[] children = GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        foreach (SpriteRenderer child in children)
        {
            if (child.name != "Body" && child.name != "Ruffles")
            {
                newColor = child.color;
                newColor.a = alpha;
                child.color = newColor;
            }
        }
    }
}
