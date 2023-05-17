using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] GameObject carHood;
    [SerializeField] CarHoodState carHoodState;
    [SerializeField][Range(-90, 0)] float carHoodOpenAngle;
    [SerializeField] float speedOpenClose;
    [SerializeField] Color carHoodColor;
    [SerializeField] Material carHoodMaterial;
    MeshRenderer meshRendererCarHood;

    enum CarHoodState
    {
        open,
        closed,
        deactivate,
        paint,
    }

    private void Start()
    {
        meshRendererCarHood = carHood.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        SetStateCarHood();
    }

    void SetStateCarHood()
    {
        switch (carHoodState)
        {
            case CarHoodState.open:
                OpenCloseCarHood(true);
                break;
            case CarHoodState.closed:
                OpenCloseCarHood(false);
                break;
            case CarHoodState.deactivate:
                ActivateCarHood(false);
                break;
            case CarHoodState.paint:
                PaintCarHood();
                break;
        }
    }

    void OpenCloseCarHood(bool open)
    {
        ActivateCarHood(true);
        float angle = 0;
        if (open) angle = carHoodOpenAngle;
        carHood.transform.rotation = Quaternion.Lerp(carHood.transform.rotation, Quaternion.Euler(angle, 0, 0), speedOpenClose * Time.deltaTime);
    }

    void ActivateCarHood(bool b)
    {
        carHood.SetActive(b);
    }

    void PaintCarHood()
    {
        ActivateCarHood(true);
        meshRendererCarHood.material = carHoodMaterial;
        meshRendererCarHood.material.color = carHoodColor;

    }
}
