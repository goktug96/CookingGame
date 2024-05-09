using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject{

    [SerializeField]
    private List<KitchenObjectSO> plateableIngredients;

    private List<KitchenObjectSO> kitchenObjectSOList;

    public event EventHandler<OnIngridientAddedEventArgs> OnIngredientAdded;
    public class OnIngridientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {    
        if (DoesRecipeAlreadyContain(kitchenObjectSO) || !IsKitchenObjectPlateable(kitchenObjectSO)) {
            return false;
        }
        kitchenObjectSOList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngridientAddedEventArgs {
            kitchenObjectSO = kitchenObjectSO
        });
        return true;
    }

    private bool IsKitchenObjectPlateable(KitchenObjectSO kitchenObjectSO) {
        return plateableIngredients.Contains(kitchenObjectSO);
    }

    private bool DoesRecipeAlreadyContain(KitchenObjectSO kitchenObjectSO) {
        return kitchenObjectSOList.Contains(kitchenObjectSO);
    }

    public List<KitchenObjectSO> GetKitchenObjectSOs() { 
        return kitchenObjectSOList;
    }
}
