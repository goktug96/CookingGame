using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField]
    private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectHolder kitchenObjectHolder;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectHolder(IKitchenObjectHolder kitchenObjectHolder) {
        if(this.kitchenObjectHolder != null) {
            this.kitchenObjectHolder.ClearKitchenObject();
        }
        this.kitchenObjectHolder = kitchenObjectHolder;
        if(kitchenObjectHolder.HasKitchenObject()) {
            Debug.LogError("Kitchen already has object");
        }
        kitchenObjectHolder.SetKitchenObject(this);

        transform.parent = kitchenObjectHolder.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectHolder GetKitchenObjectHolder() {
        return kitchenObjectHolder;
    }

    public void DestroySelf() {
        kitchenObjectHolder.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectHolder kitchenObjectHolder) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectHolder(kitchenObjectHolder);

        return kitchenObject;
    }
}
