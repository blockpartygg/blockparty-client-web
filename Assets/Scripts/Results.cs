using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class Results : ScriptableObject {
    public List<ResultsItem> Items;

    void Awake() {
        Items = new List<ResultsItem>();
    }
}