using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    [SerializeField]
    private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int currentPlateCount=0;
    private int maxPlateCount = 4;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateGrabbed;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0;
            if(currentPlateCount < maxPlateCount) {
                currentPlateCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                //KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
            }            
        }
    }
    public override void Interact(Player player) {
        if(!player.HasKitchenObject()) {
            if(currentPlateCount > 0 ) {
                currentPlateCount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateGrabbed?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        throw new System.NotImplementedException();
    }
}
