using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingBarZones : MonoBehaviour
{
    public RectTransform LeftBoard, RightBoard;
    public RectTransform prefabLine;
    public List<RectTransform> inGame = new List<RectTransform>();

    public float SetLines(int count)
    {
        var stepValue = 1f / (count + 1f);
        if (inGame.Count > 0)
        {
            foreach (var rectTransform in inGame)
            {
                Destroy(rectTransform.gameObject);
            }
            inGame.Clear();
        }

        var fullDistance = Vector3.Distance(LeftBoard.transform.localPosition, RightBoard.transform.localPosition);
        var stepDistance = fullDistance * stepValue;
        var startPoint = LeftBoard.localPosition;

        for (int i = 1; i <= count; i++)
        {
            var xpos = startPoint.x + stepDistance * i;
            var pp = Instantiate(prefabLine.gameObject, transform);
            pp.transform.localPosition = new Vector3(xpos, startPoint.y, 0);
            inGame.Add((RectTransform)pp.transform);
            pp.gameObject.SetActive(true);
        }
        return stepValue;
    }
}
