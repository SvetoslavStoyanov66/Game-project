    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.PlayerLoop;

    [CreateAssetMenu(menuName = "Items/Item")]
    public class ItemData : ScriptableObject
    {
        public string description;
        public Sprite thumbnail;
        public GameObject gameModel;
        public int quantity;

        public bool achievementUnlock;

        public string achievementDiscription;
    private void OnEnable()
    {
        quantity = 1;
    }
}
