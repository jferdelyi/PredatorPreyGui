using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    // Get dimension in px
    public static Vector2 GetDimensionInPx(GameObject gameObject) {
        Vector2 tmpDimension;

        // Get size
        tmpDimension.x = gameObject.transform.localScale.x * gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;  // this is gonna be our width
        tmpDimension.y = gameObject.transform.localScale.y * gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;  // this is gonna be our height

        return tmpDimension;
    }

    // Get dimension in px
    public static Vector2 GetOriginLocationInContainer(GameObject container, GameObject gameObject) {
        // Get size
        Vector2 tmpLocation = Tools.GetDimensionInPx(container);
        Vector2 tmpDimensionGameObject = Tools.GetDimensionInPx(gameObject);

        // Substract to origin the previous size
        tmpLocation.x = (container.transform.position.x - tmpLocation.x / 2);
        tmpLocation.y = (container.transform.position.y - tmpLocation.y / 2);
        
        // Substract to origin the previous size
        tmpLocation.x = tmpLocation.x + tmpDimensionGameObject.x / 2;
        tmpLocation.y = tmpLocation.y + tmpDimensionGameObject.y / 2;

        return tmpLocation;
    }
}
