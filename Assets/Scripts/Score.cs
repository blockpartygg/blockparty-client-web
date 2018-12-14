using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class Score : ScriptableObject {
    [SerializeField]
    int points;
    public event EventHandler PointsChanged;
    public int Points {
        get { return points; }
        set {
            if(points != value) {
                points = value;
                if(PointsChanged != null) {
                    PointsChanged(this, null);
                }
            }
        }
    }

    [SerializeField]
    int blocksMatched;
    public event EventHandler BlocksMatchedChanged;
    public int BlocksMatched {
        get { return blocksMatched; }
        set {
            if(blocksMatched != value) {
                blocksMatched = value;
                if(BlocksMatchedChanged != null) {
                    BlocksMatchedChanged(this, null);
                }
            }
        }
    }

    [SerializeField]
    Dictionary<int, int> comboCounts = new Dictionary<int, int>();
    public event EventHandler ComboCountsChanged;
    public Dictionary<int, int> ComboCounts {
        get { return comboCounts; }
    }

    [SerializeField]
    Dictionary<int, int> chainLengths = new Dictionary<int, int>();
    public event EventHandler ChainLengthsChanged;
    public Dictionary<int, int> ChainLengths {
        get { return chainLengths; }
    }

    [SerializeField]
    int linesRaised;
    public event EventHandler LinesRaisedChanged;
    public int LinesRaised {
        get { return linesRaised; }
        set {
            if(linesRaised != value) {
                linesRaised = value;
                if(LinesRaisedChanged != null) {
                    LinesRaisedChanged(this, null);
                }
            }
        }
    }

    public int MatchValue = 10;
    public int[] ComboValues = new int[] { 0, 0, 0, 20, 30, 50, 60, 70, 80, 100, 140, 170, 210, 250, 290, 340, 390, 440, 490, 550, 610, 680, 750, 820, 900, 980, 1060, 1150, 1240, 1330 };
    public int[] ChainValues = new int[] { 0, 50, 80, 150, 300, 400, 500, 700, 900, 1100, 1300, 1500, 1800 };
    public int RaiseValue = 1;

    void OnEnable() {
        Reset();
    }

    public void Reset() {
        Points = 0;
        BlocksMatched = 0;
        
        ComboCounts.Clear();
        for(int count = 0; count < BlockManager.Columns * BlockManager.Rows; count++) {
            ComboCounts[count] = 0;
        }

        ChainLengths.Clear();
        for(int length = 0; length < 256; length++) {
            ChainLengths[length] = 0;
        }

        LinesRaised = 0;
    }

    public void ScoreMatch() {
        int points = MatchValue;
        Points += points;
        BlocksMatched++;
    }

    public void ScoreCombo(int matchedBlockCount) {
        int points = ComboValues[Math.Min(matchedBlockCount - 1, ComboValues.Length - 1)];
        Points += points;
        ComboCounts[matchedBlockCount]++;
        if(ComboCountsChanged != null) {
            ComboCountsChanged(this, null);
        }
    }

    public void ScoreChain(int chainLength) {
        int points = ChainValues[Math.Min(chainLength - 1, ChainValues.Length - 1)];
        Points += points;
        chainLengths[chainLength]++;
        if(ChainLengthsChanged != null) {
            ChainLengthsChanged(this, null);
        }
    }

    public void ScoreRaise() {
        int points = RaiseValue;
        Points += points;
        LinesRaised++;
    }
}