using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyManager : MonoBehaviour {
    [SerializeField]
    GameObject EnemyPrefab;

    List<Enemy> enemies = new List<Enemy>();

    public static EnemyManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start() {
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnEnemy(new Vector2(5, 5));
        }
    }

    public static void SpawnEnemy(Vector2 SpawnIndex) {
        if(MapManager.CheckValidPosition(SpawnIndex) && !MapManager.CheckEntity(SpawnIndex)) {
            Vector2 temp = MapManager.TilemapToWorldPoint((int)SpawnIndex.x, (int)SpawnIndex.y);

            Vector3 pos = new Vector3(temp.x, 1, temp.y);
            var enemy = Instantiate(Instance.EnemyPrefab, pos, Quaternion.identity).GetComponent<Enemy>();

            MapManager.SpawnEntity(enemy.gameObject, SpawnIndex);
            Instance.enemies.Add(enemy);
        } else {
            Debug.LogError("Entity Cannot Spawn At Position : " + SpawnIndex.x + ":" + SpawnIndex.y);
        }
    }

    public static IEnumerator NextMove() {
        Sequence enemyTweening = DOTween.Sequence();
        Debug.Log("NextMove");
        foreach (Enemy enemy in Instance.enemies) {
            var enemyTween = enemy.NextMove();
            if (enemyTween != null)
                enemyTweening.Join(enemyTween);
        }
        enemyTweening.Play();
        yield return enemyTweening.WaitForCompletion();
    }
    public static IEnumerator NextShoot() {
        Sequence enemyTweening = DOTween.Sequence();

        foreach (Enemy enemy in Instance.enemies) {
            var enemyTween = enemy.NextShoot();
            if (enemyTween != null)
                enemyTweening.Join(enemyTween);
        }
        enemyTweening.Play();
        yield return enemyTweening.WaitForCompletion();
    }
}
