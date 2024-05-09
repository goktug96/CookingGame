using UnityEngine;

public class ClearCounter : BaseCounter {
    public override void Interact(Player player) {
       if(!HasKitchenObject()) {
            if(player.HasKitchenObject() ) { 
                player.GetKitchenObject().SetKitchenObjectHolder(this);
                //player.ClearKitchenObject();
            }
        }else {
            if(player.HasKitchenObject()) {
               if(player.GetKitchenObject() is PlateKitchenObject) {
                    PlateKitchenObject plateKitchenObject = (PlateKitchenObject) player.GetKitchenObject();
                    if (plateKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO())) {
                        kitchenObject.DestroySelf();
                    }                    
                } else {
                    if(kitchenObject is PlateKitchenObject) {
                        PlateKitchenObject plateKitchenObject = (PlateKitchenObject)kitchenObject;
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }                        
                    }
                }
            } else {
                kitchenObject.SetKitchenObjectHolder(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        throw new System.NotImplementedException();
    }
}
