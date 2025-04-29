using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PieceCreator))]
public class ChessGameController : MonoBehaviour
{
    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;

    private PieceCreator pieceCreator;

    private void Awake()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
    }

    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        CreatePiecesFromLayout(startingBoardLayout);
    }

    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount() ; i++)
        {
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColor team = layout.GetSquareTeamColorAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndIntialize(squareCoords, team, type);
        }
    }

    private void CreatePieceAndIntialize(Vector2Int squareCoords, TeamColor team, Type type)
    {
        Piece newPiece = pieceCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = pieceCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);
    }
}
