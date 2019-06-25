using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS_PieceSpawner : MonoBehaviour {

    public PieceType type;
    private LS_Piece currentPiece;

    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.jump:
                amtObj = LS_LevelManager.instance.jumps.Count;
                break;
            case PieceType.longblock:
                amtObj = LS_LevelManager.instance.longblocks.Count;
                break;
            case PieceType.ramp:
                amtObj = LS_LevelManager.instance.ramps.Count;
                break;
        }

         currentPiece = LS_LevelManager.instance.GetPiece(type, Random.Range(0, amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

}
