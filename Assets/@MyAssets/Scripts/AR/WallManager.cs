using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class WallManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    private List<ARPlane> walls = new List<ARPlane>();
    public List<ARPlane> Walls { get => walls; set => walls = value; }


    // Start is called before the first frame update

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (ARPlane plane in args.added)
        {
            if (plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.Vertical)
            {
                walls.Add(plane);
                Debug.Log("Plano");
            }
        }
    }
}
