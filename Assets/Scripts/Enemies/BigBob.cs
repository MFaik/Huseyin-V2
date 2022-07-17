using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigBob : Enemy {
    int _shootCounter = 0;

    int _shootDelay = 2;
    public override Tween NextMove() {
        Vector2 currentPos = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);

        if (!(currentPos.x == MapManager.PlayerPosition.x || currentPos.y == MapManager.PlayerPosition.y)) {
            Vector2 positionDifference = new Vector2(Mathf.Abs(currentPos.x - MapManager.PlayerPosition.x),
                                      Mathf.Abs(currentPos.y - MapManager.PlayerPosition.y));
            Vector2 targetPosition;
            if (positionDifference.x < positionDifference.y) {
                //Move In X
                targetPosition = new Vector2((MapManager.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                return Move(currentPos + targetPosition);
            } else {
                //Move In Y
                targetPosition = new Vector2(0, (MapManager.PlayerPosition.y < currentPos.y ? -1 : 1));
                return Move(currentPos + targetPosition);
            }

        }

        return null;
    }

    public override Tween NextShoot() {
        if (_shootCounter > 0) _shootCounter--;

        Vector2 currentPos = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);

        if (currentPos.x == MapManager.PlayerPosition.x) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2(0, (MapManager.PlayerPosition.y < currentPos.y ? -1 : 1));
                return Shoot(currentPos + angle, angle);
            }
        } else if (currentPos.y == MapManager.PlayerPosition.y) {
            if (_shootCounter == 0) {
                Vector2 angle = new Vector2((MapManager.PlayerPosition.x < currentPos.x ? -1 : 1), 0);
                return Shoot(currentPos + angle, angle);
            }
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
        _shootCounter = _shootDelay;
        return BulletManager.SpawnBullet(transform.position, spawn, vel);
    }
}
