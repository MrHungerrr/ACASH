using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseGeometry
{
    public static Quaternion GetQuaternionTo(Transform from ,Vector3 to)
    {
        Vector3 direct = to - from.position;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        targetRotation.z = from.rotation.z;
        targetRotation.x = from.rotation.x;
        return targetRotation.normalized;
    }

    public static Quaternion GetQuaternionTo(Vector3 from, Vector3 to)
    {
        Vector3 direct = to - from;
        Quaternion targetRotation = Quaternion.LookRotation(direct);
        return targetRotation.normalized;
    }

    public static float LookingAngle(Transform who, Vector3 lookingTo)
    {
        lookingTo.y = who.position.y;
        lookingTo = lookingTo - who.position;
        return Vector3.Angle(lookingTo, who.forward);
    }

}
