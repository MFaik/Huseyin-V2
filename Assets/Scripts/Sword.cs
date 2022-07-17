using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword : MonoBehaviour
{
    public IEnumerator Swing() {
        yield return transform.DORotate(new Vector3(0,-160,0),.3f,RotateMode.WorldAxisAdd).SetEase(Ease.OutBack).WaitForCompletion();
        yield return new WaitForSeconds(.3f);
        Destroy(gameObject);
    }
}
