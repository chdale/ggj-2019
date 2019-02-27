using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelRequirement
{
    public Vector2 playerPosition;
    public Vector3 defaultCameraPosition;
    public Level level;
    public bool dynamicCameraHorizontal;
    public float cameraLeftThreshold;
    public float cameraRightThreshold;
    public float cameraSize;

	public LevelRequirement(Vector2 playerPosition, Vector3 defaultCameraPosition, Level level, float cameraSize)
    {
        this.playerPosition = playerPosition;
        this.defaultCameraPosition = defaultCameraPosition;
        this.level = level;
        this.dynamicCameraHorizontal = false;
        this.cameraSize = cameraSize;
    }

    public LevelRequirement(Vector2 playerPosition, Vector3 defaultCameraPosition, Level level, bool dynamicCameraHorizontal, float cameraLeft, float cameraRight, float cameraSize)
    {
        this.playerPosition = playerPosition;
        this.defaultCameraPosition = defaultCameraPosition;
        this.dynamicCameraHorizontal = dynamicCameraHorizontal;
        this.cameraLeftThreshold = cameraLeft;
        this.cameraRightThreshold = cameraRight;
        this.cameraSize = cameraSize;
    }
}
