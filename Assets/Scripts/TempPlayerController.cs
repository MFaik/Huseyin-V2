using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    /*[SerializeField] float MoveTime = 1f;

    MapController _mapController;
    float t = 0f;
    void Start()
    {
        _mapController = MapController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = MapController.WorldToTilemapPoint(transform.position.x, transform.position.z);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 tar = pos;

        if (x > 0) tar.x++;
        else if (x < 0) tar.x--;
        else if (y > 0) tar.y++;
        else if (y < 0) tar.y--;

        t += Time.deltaTime;

        if (pos != tar && t < MoveTime) return;
        t = 0;

        

        if (_mapController.MoveTo(pos, tar)) {
            Vector2 targetLocation = MapController.TilemapToWorldPoint((int)tar.x, (int)tar.y);
            transform.position = new Vector3(targetLocation.x, 1, targetLocation.y);
        }
    }*/
}
