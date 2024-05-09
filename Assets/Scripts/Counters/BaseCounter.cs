using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectHolder{

    public static event EventHandler OnAnyObjectPlacedHere;

    [SerializeField]
    protected Transform counterTop;

    protected KitchenObject kitchenObject;

    public abstract void Interact(Player player);

    public abstract void InteractAlternate(Player player);

    public Transform GetKitchenObjectFollowTransform() {
        return counterTop;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null ) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }
    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }
}
