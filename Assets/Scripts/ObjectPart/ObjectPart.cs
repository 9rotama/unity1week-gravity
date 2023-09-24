using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

abstract public class ObjectPart : MonoBehaviour
{
    public virtual void OnCollisionWithPlayer(Player player) {}
}
