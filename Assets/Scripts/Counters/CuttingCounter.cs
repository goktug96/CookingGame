using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    [SerializeField]
    private CuttingRecipeSO[] cuttingRecipeSOArr;

    private int cuttingProgress;

    public static EventHandler OnCut;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (IsCuttable(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    cuttingProgress = 0;
                }                
            }
        } else {
            if (player.HasKitchenObject()) {
                PlateKitchenObject plateKitchenObject = (PlateKitchenObject)player.GetKitchenObject();
                if (plateKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.DestroySelf();
                }
            } else {
                kitchenObject.SetKitchenObjectHolder(player);
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progressNormalized = 1f
                });
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && IsCuttable(kitchenObject.GetKitchenObjectSO())) {

            cuttingProgress++;   
            OnCut?.Invoke(this, EventArgs.Empty);
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOForInput(kitchenObject.GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.requiredCutCount
            });

            if (cuttingProgress >= cuttingRecipeSO.requiredCutCount) {
                KitchenObjectSO kitchenObjectSO = cuttingRecipeSO.output;
                kitchenObject.DestroySelf();
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }            
        }
    }

    private bool IsCuttable(KitchenObjectSO kitchenObjectSO) {
        return GetCuttingRecipeSOForInput(kitchenObjectSO);
    }

    private CuttingRecipeSO GetCuttingRecipeSOForInput(KitchenObjectSO kitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArr) {
            if (cuttingRecipeSO.input == kitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    new public static void ResetStaticData() {
        OnCut = null; 
    }
}  
