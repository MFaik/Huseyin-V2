using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public enum ItemEnum {
        Pistol,
        Shotgun,
        Sword,
    }
    
    public Material material;

    public ItemEnum item;

    GameObject SwordPrefab;

    public IEnumerator Shoot(Vector3 position, Vector2 direction){
        switch(item){
            case ItemEnum.Pistol:
                yield return BulletManager.SpawnBullet(position,
                                            MapManager.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction).WaitForCompletion();
            break;
            case ItemEnum.Shotgun:
                Sequence shotgunSequence = DOTween.Sequence();

                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapManager.WorldToTilemapPoint(position.x + direction.x, position.z + direction.y),direction));

                Vector2 leftDirection = direction;
                Vector2 rightDirection = direction;
                if(direction.x == 0){
                    leftDirection.x = -1;
                    rightDirection.x = 1;
                } else {
                    leftDirection.y = -1;
                    rightDirection.y = 1;
                }
                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapManager.WorldToTilemapPoint(position.x + leftDirection.x, position.z + leftDirection.y),leftDirection));
                shotgunSequence.Join(BulletManager.SpawnBullet(position,
                                            MapManager.WorldToTilemapPoint(position.x + rightDirection.x, position.z + rightDirection.y),rightDirection));

                yield return shotgunSequence.WaitForCompletion();
            break;
            case ItemEnum.Sword:
                if(!SwordPrefab)
                    SwordPrefab = Resources.Load("Items/Sword") as GameObject;
                Quaternion swordRotation = Quaternion.identity;
                if(direction.y == 1){
                    swordRotation = Quaternion.Euler(0,90,0);
                } else if(direction.y == -1){
                    swordRotation = Quaternion.Euler(0,270,0);
                } else if(direction.x == -1){
                    swordRotation = Quaternion.Euler(0,0,0);
                } else
                    swordRotation = Quaternion.Euler(0,180,0);
                yield return Instantiate(SwordPrefab,position,swordRotation).GetComponent<Sword>().Swing();
            break;
        }
    }
}
