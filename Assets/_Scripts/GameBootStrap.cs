using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameBootStrap : MonoBehaviour
{
    private Field _field;
    private ContentMover _contentMover;

    [Inject]
    public void Constructor(Field field, ContentMover contentMover)
    {
        _field = field;
        _contentMover = contentMover;
    }

    public void Start()
    {
        _contentMover.OnMove += _field.CreateNewContentInRandomCell;
    }
}
