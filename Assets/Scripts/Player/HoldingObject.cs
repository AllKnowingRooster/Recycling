using UnityEngine;
using UnityEngine.InputSystem;

public class HoldingObject : MonoBehaviour
{
    private LayerMask heldObjLayermask;
    private LayerMask defaultLayer;
    private Trash heldObject;
    [SerializeField] private GameObject heldPosition;
    [SerializeField] private GameObject player;
    private Trash selectedTrash;
    private Trashcan selectedTrashCan;
    private float pickUpRange=10.0f;
    private float recycleRange=5.0f;
    private string pickUpAction = "Pickup";
    private string dropAction = "Drop";
    private string RecycleAction = "Recycle";

    void Awake()
    {
        heldObjLayermask = LayerMask.NameToLayer("PickupLayer");
        defaultLayer = LayerMask.GetMask("Default");
    }
    void Update()
    {
        if (GameManager.instance.isActive && !GameManager.instance.isGameOver)
        {
            if (heldObject == null)
            {
                if (selectedTrashCan!=null)
                {
                    RemoveSelectedTrashCan(ref selectedTrashCan);
                }

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpRange, defaultLayer))
                {
                    LookAtTrash(hit, ref selectedTrash);
                }
                else
                {
                    RemoveSelectedTrash(ref selectedTrash);
                }
            }
            else
            {
                DropTrash(ref selectedTrash, ref heldObject);
            }


            if (heldObject != null)
            {
                MoveObject();
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, recycleRange, defaultLayer))
                {
                    LookAtTrashCan(hit, ref selectedTrashCan);
                }
                else
                {
                    RemoveSelectedTrashCan(ref selectedTrashCan);
                }
            }
        }
    }

    void LookAtTrash(RaycastHit hit,ref Trash trash)
    {
        Trash newTrash = hit.collider.GetComponent<Trash>();
        if (newTrash != null){
            if (trash != newTrash)
            {
                if (trash != null)
                {
                    trash.ToggleGlow();
                }
                newTrash.ToggleGlow();
                trash = newTrash;
                trash.AddAction(64, pickUpAction);
                TooltipSystem.instace.Show(hit.collider.name);
            }

            if (Keyboard.current.eKey.wasReleasedThisFrame)
            {
                PickupTrash(newTrash);
            }
        }
        else
        {
            RemoveSelectedTrash(ref trash);
        }
    }

    void LookAtTrashCan(RaycastHit hit,ref Trashcan trashcan)
    {
        Trashcan newTrashCan=hit.collider.GetComponent<Trashcan>();
        if (newTrashCan!=null)
        {
            if (trashcan!=newTrashCan)
            {
                if (trashcan!=null)
                {
                    trashcan.ToggleGlow();
                }
                newTrashCan.ToggleGlow();
                trashcan = newTrashCan;
                trashcan.AddAction(87, RecycleAction);
                TooltipSystem.instace.UpdateAction();
            }

            if (Keyboard.current.mKey.wasReleasedThisFrame)
            {
                trashcan.Recycle(heldObject);
                selectedTrash.gameObject.transform.parent = null;
                selectedTrash.gameObject.SetActive(false);
                selectedTrashCan.ToggleGlow();
                selectedTrash.RemoveAction(64);
                selectedTrashCan.RemoveAction(87);
                TooltipSystem.instace.Hide();
                selectedTrash.GetComponent<Rigidbody>().isKinematic = false;
                selectedTrash.gameObject.layer = 0;
                selectedTrash = null;
                selectedTrashCan = null;
                heldObject = null;

            }

        }
        else
        {
            RemoveSelectedTrashCan(ref trashcan);
        }
    }


    void RemoveSelectedTrash(ref Trash trash)
    {
        TooltipSystem.instace.Hide();
        if (trash != null)
        {
            trash.ToggleGlow();
            trash = null;
        }
    }

    void RemoveSelectedTrashCan(ref Trashcan trashCan)
    {
        if (trashCan != null)
        {
            trashCan.ToggleGlow();
            trashCan.RemoveAction(87);
            trashCan = null;
        }
        TooltipSystem.instace.UpdateAction();
    }

    void PickupTrash(Trash trash)
    {
        
        Rigidbody trashRigidbody = trash.GetComponent<Rigidbody>();
        if (trashRigidbody != null)
        {
            selectedTrash.ToggleGlow();
            trashRigidbody.isKinematic = true;
            heldObject = trash;
            heldObject.transform.parent = heldPosition.transform;
            heldObject.gameObject.layer = heldObjLayermask;
            heldObject.RemoveAction(64);
            heldObject.AddAction(64,dropAction);
            TooltipSystem.instace.UpdateAction();
            Physics.IgnoreCollision(trash.GetComponent<Collider>(), player.GetComponent<Collider>(),true);
        }

    }

    void DropTrash(ref Trash trash,ref Trash heldObject)
    {
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObject.RemoveAction(64);
            TooltipSystem.instace.Hide();
            heldObject.transform.parent = null;
            heldObject.gameObject.layer = 0;
            Rigidbody heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
            heldObjectRigidbody.isKinematic = false;
            trash = null;
            heldObject = null;
        }
        
    }

    void MoveObject()
    {
        heldObject.transform.position = heldPosition.transform.position;
    }

}