using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform RendererChild;

    [SerializeField] PlayerFaceController[] Faces;


    enum Direction {
        ZP,
        ZM,
        XP,
        XM
    }

    void Start() {
        MapManager.SpawnEntity(gameObject, new Vector2(0,0));
        Vector2 startPos = MapManager.TilemapToWorldPoint(0,0);
        transform.position = new Vector3(startPos.x, 0.5f, startPos.y);
    }

    bool CanTurn = false;
    bool CanShoot = false;

    void Update() {
        if(CanTurn){
            Vector2 tilemapPosition = MapManager.WorldToTilemapPoint(transform.position.x, transform.position.z);
            
            if(Input.GetAxisRaw("Vertical") > 0.1f){
                if(MapManager.MoveTo(tilemapPosition, tilemapPosition + new Vector2(0, 1)))
                    StartCoroutine(Turn(Direction.ZP));
            }  
            else if(Input.GetAxisRaw("Vertical") < -0.1f){
                if(MapManager.MoveTo(tilemapPosition, tilemapPosition + new Vector2(0,-1)))
                    StartCoroutine(Turn(Direction.ZM));
            } 
            else if(Input.GetAxisRaw("Horizontal") < -0.1f){
                if(MapManager.MoveTo(tilemapPosition, tilemapPosition + new Vector2(-1,0)))
                    StartCoroutine(Turn(Direction.XM));
            } 
            else if(Input.GetAxisRaw("Horizontal") > 0.1f){
                if(MapManager.MoveTo(tilemapPosition, tilemapPosition + new Vector2( 1,0)))
                    StartCoroutine(Turn(Direction.XP));
            }else if (Input.GetKeyDown(KeyCode.Backspace)) {
                CanTurn = false;
                TurnManager.ContinueTurnAfterMove();
            }
        } else if(CanShoot) {
            if(Input.GetAxisRaw("Vertical") > 0.1f){
                StartCoroutine(Shoot(new Vector2(0, 1)));
            }  
            else if(Input.GetAxisRaw("Vertical") < -0.1f){
                StartCoroutine(Shoot(new Vector2(0,-1)));
            } 
            else if(Input.GetAxisRaw("Horizontal") < -0.1f){
                StartCoroutine(Shoot(new Vector2(-1,0)));
            } 
            else if(Input.GetAxisRaw("Horizontal") > 0.1f){
                StartCoroutine(Shoot(new Vector2( 1,0)));
            }   
        }
    }

    public void EnableTurn(){
        CanTurn = true;
    }

    public void EnableShoot() {
        CanShoot = true;
    }

    Vector3 ZP = new Vector3(0,0, 0.5f);
    Vector3 ZM = new Vector3(0,0,-0.5f);
    Vector3 XP = new Vector3( 0.5f,0,0);
    Vector3 XM = new Vector3(-0.5f,0,0);

    IEnumerator Turn(Direction direction) {
        CanTurn = false;

        switch (direction){
            case Direction.ZP:
                transform.position += ZP;
                RendererChild.position -= ZP;
                yield return transform.DORotate(new Vector3(90,0,0),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += ZP;
                RendererChild.position -= ZP;
            break;
            case Direction.XP:
                transform.position += XP;
                RendererChild.position -= XP;
                yield return transform.DORotate(new Vector3(0,0,-90),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += XP;
                RendererChild.position -= XP;
            break;
            case Direction.ZM:
                transform.position += ZM;
                RendererChild.position -= ZM;
                yield return transform.DORotate(new Vector3(-90,0,0),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += ZM;
                RendererChild.position -= ZM;
            break;
            case Direction.XM:
                transform.position += XM;
                RendererChild.position -= XM;
                yield return transform.DORotate(new Vector3(0,0,90),.2f,RotateMode.WorldAxisAdd).WaitForCompletion();
                transform.position += XM;
                RendererChild.position -= XM;
            break;
        }
        TurnManager.ContinueTurnAfterMove();
    }

    IEnumerator Shoot(Vector2 direction){
        CanShoot = false;
        yield return GetActiveItem().Shoot(transform.position, direction);
        TurnManager.ContinueTurnAfterPlayer();
    }

    Item GetActiveItem(){
        foreach(var face in Faces){
            if(Mathf.Abs(face.transform.rotation.eulerAngles.x-90) < 1f && Mathf.Abs(face.transform.rotation.eulerAngles.z) < 1f)
                return face.Item; 
        }
        return null;
    }
}
