using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameBootStrap : MonoBehaviour
{
    private Field _field;

    [Inject]
    public void Constructor(Field field)
    {
        _field = field;
    }
}
