using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelector : MonoBehaviour
{ 
    Vector2 mousePosition;
    Camera cam;
    public Canvas thisCanvas;
    public Button StartGame;
    public Button ExitGame;
    float _ypostioin;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        thisCanvas.transform as RectTransform, Input.mousePosition,
        thisCanvas.worldCamera,
        out mousePosition);
       

    }

    // Update is called once per frame
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        thisCanvas.transform as RectTransform, Input.mousePosition,
        thisCanvas.worldCamera,
        out mousePosition);

        Vector2 StartGamePosition;
       
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        thisCanvas.transform as RectTransform, StartGame.transform.position,
        thisCanvas.worldCamera,
        out StartGamePosition);

        Vector2 ExitGamePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        thisCanvas.transform as RectTransform, ExitGame.transform.position,
        thisCanvas.worldCamera,
        out ExitGamePosition);

        if (thisCanvas.transform.TransformPoint(mousePosition).y >= thisCanvas.transform.TransformPoint(StartGamePosition).y +1 || thisCanvas.transform.TransformPoint(mousePosition).y <= thisCanvas.transform.TransformPoint(StartGamePosition).y - 1)
        {
            transform.position = thisCanvas.transform.TransformPoint(new Vector2(transform.position.x, StartGamePosition.y));
        }

        if (thisCanvas.transform.TransformPoint(mousePosition).y >= thisCanvas.transform.TransformPoint(ExitGamePosition).y + 1 || thisCanvas.transform.TransformPoint(mousePosition).y <= thisCanvas.transform.TransformPoint(ExitGamePosition).y - 1)
        {
            transform.position = thisCanvas.transform.TransformPoint(new Vector2(transform.position.x, ExitGamePosition.y));
        }

    }
}
