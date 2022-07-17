using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public void Update() {
        OnUpdate();
    }
    private void Start() {
        Init();
    }

    public virtual void Init() { }
    public virtual void OnUpdate() { }

    public virtual Tween NextMove() { return null; }
    public virtual Tween NextShoot() { return null; }

    public virtual Tween Move(Vector2 tar) { return null; }

    public virtual Tween Shoot(Vector2 spawn, Vector2 tar) { return null; }

    public virtual void Die() { }
}
