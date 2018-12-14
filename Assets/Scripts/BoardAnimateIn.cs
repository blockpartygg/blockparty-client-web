using UnityEngine;

public class BoardAnimateIn : MonoBehaviour {
    public BlockManager BlockManager;
    public Game Game;

    void Start() {
        SetupAnimation();
    }

    public void HandleTimeExpired() {
        if(Game.State == GameState.PreMinigame) {
            SetupAnimation();
        }
    }

    void SetupAnimation() {
        for(int column = 0; column < BlockManager.Columns; column++) {
            for(int row = 0; row < BlockManager.Rows; row++) {
                BlockManager.Blocks[column, row].AnimateIn.Play();
            }
        }
    }
}