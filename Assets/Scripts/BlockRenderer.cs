using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class BlockRenderer: MonoBehaviour {
    public Game Game;
    public Block Block;
    public BlockGarbage Garbage;
    public BlockSlider Slider;
    public BlockFaller Faller;
    public BlockMatcher Matcher;
    public BlockClearer Clearer;
    public SpriteRenderer SpriteRenderer;
    public List<Sprite> Sprites;
    public List<Sprite> MatchedSprites;
    public List<Sprite> ClearingSprites;
    public List<Sprite> HoppingSprites0;
    public List<Sprite> HoppingSprites1;
    public List<Sprite> HoppingSprites2;
    public List<Sprite> HoppingSprites3;
    public List<Sprite> HoppingSprites4;
    public FloatReference RaiseDuration;
    public FloatReference SlideDuration;
    public FloatReference FallDuration;
    public FloatReference ClearDuration;
    
    List<List<Sprite>> hoppingSprites;
    BlockManager blockManager;
    ParticleManager particleManager;
    BoardRaiser boardRaiser;
    Vector3 garbageTranslation = Vector3.zero;
    Vector3 blockTranslation;

    void Awake() {
        GameObject board = GameObject.Find("Board");
        if(board != null) {
            blockManager = board.GetComponent<BlockManager>();
            boardRaiser = board.GetComponent<BoardRaiser>();
        }

        GameObject particles = GameObject.Find("Particles");
        if(particles != null) {
            particleManager = particles.GetComponent<ParticleManager>();
        }        

        hoppingSprites = new List<List<Sprite>>();
        hoppingSprites.Add(HoppingSprites0);
        hoppingSprites.Add(HoppingSprites1);
        hoppingSprites.Add(HoppingSprites2);
        hoppingSprites.Add(HoppingSprites3);
        hoppingSprites.Add(HoppingSprites4);
    }

    void Start() {
        blockTranslation = new Vector3(Block.Column, Block.Row);

        UpdateSpriteState();
        UpdateSpriteType();
        UpdatePosition();
        Block.StateChanged += HandleStateChanged;
        Block.TypeChanged += HandleTypeChanged;
    }

    void HandleStateChanged(object sender, EventArgs args) {
        UpdateSpriteState();
        UpdatePosition();
    }

    void HandleTypeChanged(object sender, EventArgs args) {
        UpdateSpriteType();
        UpdatePosition();
    }

    void UpdatePosition() {
        Vector3 raiseTranslation = Vector3.zero;
        if(boardRaiser != null) {
            raiseTranslation = new Vector3(0, boardRaiser.Elapsed / RaiseDuration.Value);
        }
        Vector3 parentPosition = Vector3.zero;
        if(transform.parent != null) {
            parentPosition = transform.parent.position;
        }
        transform.position = parentPosition + blockTranslation + raiseTranslation + garbageTranslation;
    }

    void UpdateSpriteState() {
        switch(Block.State) {    
            case BlockState.Matched:
                if(Block.Type != -1) {
                    SpriteRenderer.sprite = MatchedSprites[Block.Type];
                }
                break;
            case BlockState.WaitingToClear:
                if(Block.Type != -1) {
                    SpriteRenderer.sprite = ClearingSprites[Block.Type];
                }
                break;
            case BlockState.Clearing:
                if(particleManager != null) {
                    particleManager.Particles[Block.Column, Block.Row].GetComponent<ParticleSystem>().Play();
                }
                break;
            case BlockState.WaitingToEmpty:
            case BlockState.Empty:
                SpriteRenderer.sprite = null;
                break;
            default:
                SpriteRenderer.transform.localScale = Vector3.one;
                SpriteRenderer.color = Color.white;
                break;
        }
    }

    void UpdateSpriteType() {
        if(Block.Type == -1 || (Block.Type == 5 && Block.Garbage.IsNeighbor)) {
            SpriteRenderer.sprite = null;
        }
        else {
            SpriteRenderer.sprite = Sprites[Block.Type];
        }

        if(Block.Type == 5) {
            SpriteRenderer.size = new Vector3((float)Garbage.Width, (float)Garbage.Height);
            garbageTranslation = new Vector3((Garbage.Width - 1) * 0.5f, (Garbage.Height - 1) * 0.5f);
        }
        else {
            SpriteRenderer.size = Vector2.one;
            garbageTranslation = Vector2.zero;
        }
    }

    void FixedUpdate() {
        Vector3 raiseTranslation = Vector3.zero;
        if(boardRaiser != null) {
            raiseTranslation = new Vector3(0, boardRaiser.Elapsed / RaiseDuration.Value);
        }
        Vector3 parentPosition = Vector3.zero;
        if(transform.parent != null) {
            parentPosition = transform.parent.position;
        }
        float timePercentage;

        switch(Block.State) {
            case BlockState.Idle:
                transform.position = parentPosition + blockTranslation + raiseTranslation + garbageTranslation;
                if(Game.Mode == GameMode.Survival) {
                    if(blockManager != null) {
                        if(blockManager.Blocks[Block.Column, BlockManager.Rows - 2].State != BlockState.Empty || 
                            blockManager.Blocks[Block.Column, BlockManager.Rows - 3].State != BlockState.Empty) {
                            SpriteRenderer.sprite = hoppingSprites[Block.Type][(int)(Time.time * 10) % 4];
                        }
                        else {
                            SpriteRenderer.sprite = Sprites[Block.Type];
                        }
                    }
                }
                break;
            case BlockState.Sliding:
                float direction = Slider.Direction == SlideDirection.Left ? -1 : 1;
                timePercentage = Slider.Elapsed / SlideDuration.Value;
                Vector3 slideTranslation = new Vector3(direction * timePercentage, 0);
                transform.position = parentPosition + blockTranslation + raiseTranslation + slideTranslation;
                break;
            case BlockState.Falling:
                timePercentage = Faller.Elapsed / FallDuration.Value;
                Vector3 fallTranslation = new Vector3(0, -1 * timePercentage);
                transform.position = parentPosition + blockTranslation + garbageTranslation + raiseTranslation + fallTranslation;
                break;
            case BlockState.Matched:
                SpriteRenderer.sprite = Matcher.Elapsed % 0.1f < 0.05f ? MatchedSprites[Block.Type] : Sprites[Block.Type];
                break;
            case BlockState.Clearing:
                timePercentage = Clearer.Elapsed / ClearDuration.Value;
                SpriteRenderer.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timePercentage);
                break;
        }

        if(Block.Faller.JustLanded) {
            transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            transform.DOScale(1f, 0.5f);
            
            Block.Faller.JustLanded = false;
        }
    }
}