using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    GameObject _mapObject;

    public static Vector2 PlayerPosition;

    GameObject[][] _tileset;
    static Vector2 mapSize;

    public static MapManager Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
        MapInit();
    }

    void MapInit() {
        _mapObject = GameObject.FindGameObjectWithTag("Map");

        mapSize = new Vector2(_mapObject.transform.localScale.x, _mapObject.transform.localScale.z);

        _tileset = new GameObject[(int)mapSize.x][];

        for(int i = 0; i < mapSize.x; i++) {
            _tileset[i] = new GameObject[(int)mapSize.y];
        }
    }

    public static bool MoveTo(Vector2 from, Vector2 to) {
        if(CheckEntity(from) && CheckValidPosition(to) && !CheckEntity(to)) {

            if (from.x == PlayerPosition.x && from.y == PlayerPosition.y) PlayerPosition = to;

            Instance._tileset[(int)to.x][(int)to.y] = Instance._tileset[(int)from.x][(int)from.y];
            Instance._tileset[(int)from.x][(int)from.y] = null;
            return true;
        }

        return false;
    }

    public static void SpawnEntity(GameObject objectRef, Vector2 pos) {
        if (!CheckEntity(pos)) {
            Instance._tileset[(int)pos.x][(int)pos.y] = objectRef;

            if (objectRef.CompareTag("Player")) PlayerPosition = pos;
        } else {
            Debug.LogError("Entity Cannot Spawn At Position : " + pos.x + ":" + pos.y);
        }
    }

    public static bool CheckEntity(Vector2 pos) {
        return CheckValidPosition(pos) && Instance._tileset[(int)pos.x][(int)pos.y] != null;
    }

    public static bool CheckValidPosition(Vector2 pos) {
        return pos.x >= 0 && pos.y >= 0 && pos.x < mapSize.x && pos.y < mapSize.y;
    }

    static public Vector2 TilemapToWorldPoint(int x, int y) {
        return new Vector2(mapSize.x * -1 / 2f + 0.5f + x,
            mapSize.y * -1 / 2f + 0.5f + y);
    }

    static public Vector2 WorldToTilemapPoint(float x, float y) {
        return new Vector2(x - mapSize.x * -1 / 2f - 0.5f,
            y - mapSize.y * -1 / 2f - 0.5f);
    }
}
