using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour {

    public int minValue = 0;
    public int maxValue = 5;
    public int value = 5;

    public Texture backgroundTexture;
    public Texture unitTexture;
    public float textureScale = 45f;

    public float startAngle = 180f;
    public float arcAngle = 270f;
    public float radious = 100f;
    public Vector2 position;

    void OnGUI() {
        if (Event.current.type.Equals(EventType.Repaint)){
            // Draw
            Graphics.DrawTexture(new Rect(position.x - radious, position.y - radious, 2.3f * radious, 2.3f * radious), backgroundTexture);
            float angleBase = startAngle * Mathf.Deg2Rad;
            float angleStep = arcAngle / (maxValue-1) * Mathf.Deg2Rad;
            for (int i = minValue; i < value; i++)
            {
                float angle = angleBase + i * angleStep;
                float x = position.x + radious * Mathf.Cos(angle);
                float y = position.y + radious * Mathf.Sin(angle);
                Graphics.DrawTexture(new Rect(x, y, textureScale, textureScale), unitTexture);
            }
        }
    }

}
