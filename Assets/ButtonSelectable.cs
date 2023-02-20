using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectable : Selectable
{
    public SpriteRenderer playerImage,ready,unready;

    public bool useThis;
    void Update()
    {
        if (true)
        {

        }

        //Check if the GameObject is being highlighted
        if (IsHighlighted() ||IsPressed())
        {
            
            //Output that the GameObject was highlighted, or do something else
            playerImage.sprite = ready.sprite;
        }
        else
        {
            playerImage.sprite = unready.sprite;
        }
    }
}
