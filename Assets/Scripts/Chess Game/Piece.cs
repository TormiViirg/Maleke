using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;


[RequireComponent(typeof(IObjectTweener))]
[RequireComponent (typeof(MaterialSetter))]
public abstract class Piece : MonoBehaviour
{
    public MaterialSetter materialSetter;//if blows up look at this
    public Board board {  protected get; set; }
    public Vector2Int occupiedSquare { get; set; }
    public TeamColor team { get; set; }
    public bool hasMoved { get; private set; }
    public List<Vector2Int> availableMoves;

    private IObjectTweener tweener;
    public abstract List<Vector2Int> SelectAvailableSquares();
    private void Awake()
    {
        availableMoves = new List<Vector2Int>();
        tweener = GetComponent<IObjectTweener>();
        materialSetter = GetComponent<MaterialSetter>();
        hasMoved = false;
    }
    public void SetMaterial(Material material)//and this
    {
        /*if (material == null)
            material = GetComponent<Material>();
        materialSetter.SetSingleMaterial(material);*/
        if (materialSetter == null)
        {
            materialSetter = GetComponent<MaterialSetter>();
        }

        if (materialSetter != null)
        {
            materialSetter.SetSingleMaterial(material);
        }
        else
        {
            Debug.LogError("MaterialSetter component missing on Piece!", this);
        }
    }

    public bool IsFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords)
    {
        return availableMoves.Contains(coords);
    }
    public virtual void MovePiece(Vector2Int coords) 
    { 
    }
    protected void TryToAddMove(Vector2Int coords)
    {
        availableMoves.Add(coords);
    }
    public void SetData(Vector2Int coords, TeamColor team, Board board) 
    {
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        transform.position = board.CalculatePositionFromCoords(coords);
    }
}
