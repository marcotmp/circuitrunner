using UnityEngine;

public enum Direction { RIGHT=0, DOWN=270, LEFT=180, UP=90 };

public interface CameraTargetable {

    Direction GetDirection();
    Vector3 GetPosition();
    float GetVelocity();
}
