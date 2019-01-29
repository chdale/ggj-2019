using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRequirement : MonoBehaviour {

    public Vector2 playerPosition { get; set; }
    public Vector3 defaultCameraPosition { get; set; }
    public Level level { get; set; }
    public bool dynamicCameraHorizontal { get; set; }
    public float cameraLeftThreshold { get; set; }
    public float cameraRightThreshold { get; set; }
    public float cameraSize { get; set; }

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
