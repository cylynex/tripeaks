using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelector : MonoBehaviour {

    public Sprite currentBackground;

    private void Start() {

        this.GetComponent<Image>().sprite = currentBackground;

    }

    public void SelectBackground() {
        Debug.Log("clicked this one");
        GameManager.boardBackground = currentBackground;

    }

}