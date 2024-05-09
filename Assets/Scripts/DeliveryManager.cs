using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public static DeliveryManager Instance { get; private set; }

    [SerializeField]
    private RecipeListSO recipeListSO;

    List<RecipeSO> waitingRecipeList;

    private float spawnRecipeTimer = 0;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulDeliveryCount = 0;

    public event EventHandler OnRecipeSpawned, OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess, OnRecipeFailed;

    private void Awake() {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(waitingRecipeList.Count  < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {        
        for(int i = 0; i < waitingRecipeList.Count; i++) {
            RecipeSO recipeSO = waitingRecipeList[i];
            if (recipeSO.kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOs().Count) {
                continue;
            }
            
            bool ingredientsFound = true;
            foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) {                
                if (!plateKitchenObject.GetKitchenObjectSOs().Contains(kitchenObjectSO)) {
                    ingredientsFound=false;
                    break;
                }
            }

            if(ingredientsFound) {
                waitingRecipeList.RemoveAt(i);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                successfulDeliveryCount++;
                return;
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        //no recipe in list
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeList;
    }

    public int GetSuccesfullDeliveryCount() {
        return successfulDeliveryCount;
    }
}
