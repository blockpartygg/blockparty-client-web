using UnityEngine;
using System;

public class BlockSelectedRenderer : MonoBehaviour {
    public Block Block;
    public SpriteRenderer SelectedSprite;
    public Color SelectedColor = new Color(1f, 1f, 1f, .25f);
    public Color DeselectedColor = new Color(1f, 1f, 1f, 0);
    BoardController controller;

    void Awake() {
        GameObject board = GameObject.Find("Board");
        if(board != null) {
            controller = board.GetComponent<BoardController>();
        }
    }

    void Start() {
        if(controller != null) {
            controller.SelectedBlockChanged += UpdateSelectedSprite;
        }
    }

    void UpdateSelectedSprite(object sender, EventArgs args) {
        if(controller != null) {
            if(Block == controller.SelectedBlock) {
               SelectedSprite.color = SelectedColor;
            }
            else {
                SelectedSprite.color = DeselectedColor;
            }
        }
    }
}