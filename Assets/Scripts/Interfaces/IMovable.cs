using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable 
{
    public bool IsMoving {set;get;}

    public void Move(LineRenderer path);
}
