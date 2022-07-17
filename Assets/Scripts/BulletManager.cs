using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    GameObject BulletPrefab;

    List<Bullet> bullets = new List<Bullet>();

    public static BulletManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    public static Tween SpawnBullet(Vector3 SpawnPosition, Vector2 SpawnIndex, Vector2 velocity) {
        var bullet = Instantiate(Instance.BulletPrefab, SpawnPosition, Quaternion.identity).GetComponent<Bullet>();

        Instance.bullets.Add(bullet);

        return bullet.Init(SpawnIndex, velocity);
    }

    public static IEnumerator NextStep() {
        Sequence bulletSequence = DOTween.Sequence();
        foreach (Bullet bullet in Instance.bullets) {
            var bulletTween = bullet.NextStep();
            if(bulletTween != null)
                bulletSequence.Join(bulletTween);
        }
        bulletSequence.Play();
        yield return bulletSequence.WaitForCompletion();
    }

    public static void DestroyBullet(Bullet refObject) {
        Instance.bullets.Remove(refObject);
    }
}
