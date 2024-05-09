using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField]
    private FryingRecipeSO[] fryingRecipeSOArr;

    private FryingRecipeSO fryingRecipeSO;
    private float fryingProgress;
    private State currentState;

    private void Start() {
        currentState = State.Idle;
    }

    public event EventHandler<OnStoveStateChangedEventArgs> OnStoveStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStoveStateChangedEventArgs : EventArgs {
        public State state;
    }
    

    private void Update() {
        if (HasKitchenObject()) {
            switch (currentState) {
                case State.Idle:                    
                    break;
                case State.Frying:
                    fryingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingProgress / fryingRecipeSO.fryingTimer
                    });
                    if (fryingProgress > fryingRecipeSO.fryingTimer) {
                        kitchenObject.DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        currentState = State.Fried;
                        fryingRecipeSO = GetFryingRecipeSOForInput(kitchenObject.GetKitchenObjectSO());
                        fryingProgress = 0;
                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs {
                            state = State.Fried
                        });
                    }
                    break;
                case State.Fried:
                    fryingProgress += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = fryingProgress / fryingRecipeSO.fryingTimer
                    });
                    if (fryingProgress > fryingRecipeSO.fryingTimer) {
                        kitchenObject.DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        currentState = State.Burned;
                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs {
                            state = State.Burned
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 1f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }                            
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                if (IsFryable(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectHolder(this);
                    fryingRecipeSO = GetFryingRecipeSOForInput(kitchenObject.GetKitchenObjectSO());
                    fryingProgress = 0;                    
                    currentState = State.Frying;
                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs {
                        state = currentState
                    });
                }
            }
        } else {
            if (player.HasKitchenObject()) {
                PlateKitchenObject plateKitchenObject = (PlateKitchenObject)player.GetKitchenObject();
                if (plateKitchenObject.TryAddIngredient(kitchenObject.GetKitchenObjectSO())) {
                    kitchenObject.DestroySelf();
                    if (currentState != State.Idle) {
                        OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs {
                            state = State.Idle
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                            progressNormalized = 1f
                        });
                    }
                    currentState = State.Idle;
                }
            } else {
                kitchenObject.SetKitchenObjectHolder(player);
                if (currentState != State.Idle) {
                    currentState = State.Idle;
                    OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs {
                        state = State.Idle
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = 1f
                    });
                }                            
            }
        }
    }

    public override void InteractAlternate(Player player) {
        throw new System.NotImplementedException();
    }

    private bool IsFryable(KitchenObjectSO kitchenObjectSO) {
        return GetFryingRecipeSOForInput(kitchenObjectSO);
    }

    private FryingRecipeSO GetFryingRecipeSOForInput(KitchenObjectSO kitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArr) {
            if (fryingRecipeSO.input == kitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    public bool IsBurning() {
        return currentState == State.Fried;
    }
}
