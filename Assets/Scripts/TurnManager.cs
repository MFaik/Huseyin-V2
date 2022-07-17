using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TurnManager : MonoBehaviour 
{
    public static TurnManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    PlayerController _playerController;

    void Start() {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerController.EnableTurn();
    }

    public static void ContinueTurnAfterMove() {
        Instance.StartCoroutine(Instance.EnemyMove());
    }
    IEnumerator EnemyMove() {
        yield return EnemyManager.NextMove();
        Instance._playerController.EnableShoot();
    }
    public static void ContinueTurnAfterPlayer(){
        Instance.StartCoroutine(Instance.ContinueTurn());
    }

    IEnumerator ContinueTurn(){
        yield return EnemyManager.NextShoot();
        yield return BulletManager.NextStep();
        _playerController.EnableTurn();
    }
}