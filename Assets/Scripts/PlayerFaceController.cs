using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceController : MonoBehaviour
{
    [SerializeField] Item _item;//HACK: wait for item manager
    public Item Item
    {
        get {return _item;}
        set {
            _meshRenderer.material = value.material;
            _item = value;
        }
    }

    MeshRenderer _meshRenderer;

    void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();    
    }
}
