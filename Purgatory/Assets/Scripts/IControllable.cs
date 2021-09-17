using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void EnableControl(Player player);
    void RevokeControl(Player player);
}
