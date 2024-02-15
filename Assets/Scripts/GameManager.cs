using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int itemCollected;

    public Text itemText;

    private void Update()
    {
        itemText.text = "" + itemCollected;
    }

    public void CollectItem()
    {
        itemCollected++;
    }
}
