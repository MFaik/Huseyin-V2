using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Slime : Enemy {
    [SerializeField] int SlimeIndex = 0;

    Vector2 _lastTargetPos;
    int _currentCommand = 0;
    string[] _commands = {"URDL"};
    public override Tween NextMove() {
        Vector2 currentPos = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);
        char command = _commands[SlimeIndex][_currentCommand];

        _currentCommand++;

        if (_currentCommand >= _commands[SlimeIndex].Length) {
            _currentCommand = 0;
        }
        Vector2 targetPosition = currentPos;

        switch (command) {
            case 'U':
                targetPosition.y++;
                break;
            case 'R':
                targetPosition.x++;
                break;
            case 'D':
                targetPosition.y--;
                break;
            case 'L':
                targetPosition.x--;
                break;

            default:
                Debug.LogError("Unknown Command : " + command);
                break;
        }
        _lastTargetPos = targetPosition;
        Tween t = Move(targetPosition);

        if (t == null) _currentCommand--;

        if (_currentCommand == -1) _currentCommand = _commands[SlimeIndex].Length - 1;

        return t;
    }

    public override Tween NextShoot() {

        Vector2 currentPos = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);

        if(_lastTargetPos == MapManager.PlayerPosition) {
            return Shoot(_lastTargetPos, new Vector2(0, 0));
        }

        return null;
    }
    public override Tween Move(Vector2 tar) {
        if (MapManager.MoveTo(MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z), tar)) {
            Vector2 temp = MapManager.TilemapToWorldPoint((int)tar.x, (int)tar.y);

            Vector3 pos = new Vector3(temp.x, 1, temp.y);
            return transform.DOMove(pos, 0.3f);
        } else {
            return null;
        }
    }

    public override Tween Shoot(Vector2 spawn, Vector2 vel) {
        Debug.Log("Slime Attack");
        return null;
    }
}
