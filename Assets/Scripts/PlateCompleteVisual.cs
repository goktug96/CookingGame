using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class PlateCompleteVisual : MonoBehaviour {
    [SerializeField]
    private PlateKitchenObject plateKitchenObject;
    [SerializeField]
    private SerializedDictionary<KitchenObjectSO, GameObject> kitchenObjectSO_gameObjectMap;    

    private void Start () {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e) {       
        kitchenObjectSO_gameObjectMap.TryGetValue(e.kitchenObjectSO, out GameObject gameObject);
        if (gameObject != null) {
            gameObject.SetActive(true);
        }        
    }
}
