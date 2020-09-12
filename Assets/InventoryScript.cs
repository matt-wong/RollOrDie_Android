using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{

    playerScript myPlayer;
    TextMeshProUGUI myAdditionalLivesText;
    Image myLivesIcon;
    Image myWeightIcon;
    Image myWrapIcon;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        this.myPlayer = GameObject.FindObjectOfType<playerScript>();
        myPlayer.GotUpgradeAction += (value) => {CheckForPlayerUpgrades();};

        this.myAdditionalLivesText = transform.Find("livesText").GetComponent<TextMeshProUGUI>();
        this.myLivesIcon = transform.Find("livesIcon").GetComponent<Image>();
        this.myWeightIcon = transform.Find("weightIcon").GetComponent<Image>();
        this.myWrapIcon = transform.Find("wrapIcon").GetComponent<Image>();
        gm = gameManager.Instance;
    }

    void CheckForPlayerUpgrades()
    {
        //Got Weight Upgrade
        if (myPlayer.HasExtraWeight)
        {
            myWeightIcon.color = Color.white; //Show (permanent)

            Text maybeText = GameObject.Find("rollText").GetComponent<Text>();
            if (maybeText)
            {
                maybeText.text = "QUICK ROLL!";
            }
        }

        //Got Wrap Upgrade
        if (myPlayer.CanWrap)
        {
            myWrapIcon.color = Color.white; //Show (permanent)
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Extra Lives
        if (myPlayer.ExtraLives == 0){
            myLivesIcon.color = new Color(0f, 0f, 0f, 0.33f); //Show only shadow (hint)
            myAdditionalLivesText.text = "";
        }else{
            myLivesIcon.color = Color.white; //Show
            myAdditionalLivesText.text = "X" + myPlayer.ExtraLives.ToString();
        }

    }
}
