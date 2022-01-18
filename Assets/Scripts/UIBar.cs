using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour {

    public int minValue = 0;
    public int maxValue = 5;
    public int value = 5;

    public Texture backgroundTexture;
    public Texture unitTexture;
    public float textureScale = 35f;

    public Vector2 startPoint = new Vector2(-1, 0);
    public Vector2 endPoint = new Vector2(0, -1);
    public float radious = 100f;
    public Vector2 position;

    void Start() {
        startPoint = startPoint.normalized;
        endPoint = endPoint.normalized;
    }
    void OnGUI() {
        // Draw
        if (Event.current.type.Equals(EventType.Repaint)){
            float angleBase = Vector2.Angle(Vector2.right, startPoint) * Mathf.Deg2Rad;
            float angleStep = Vector2.SignedAngle(startPoint, endPoint) / (maxValue-1) * Mathf.Deg2Rad;
            for (int i = minValue; i < value; i++)
            {
                float angle = angleBase + i * angleStep;
                float x = position.x + radious * Mathf.Cos(angle);
                float y = position.y + radious * Mathf.Sin(angle);
                // print("x: " + x + ", y: " + y);
                Graphics.DrawTexture(new Rect(x, y, textureScale, textureScale), unitTexture);
            }
        }
    }

}
