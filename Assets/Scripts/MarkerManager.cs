using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerManager : MonoBehaviour
{

    public class Marker
    {
        public Vector3 position;
        public Quaternion rotation;

        public Marker(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    [SerializeField]
    public List<Marker> markerList = new List<Marker>();

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        UpdateMarkerList();
    }

    public void UpdateMarkerList()
    {
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

    public void ClearMarkerList()
    {
        markerList.Clear();
        markerList.Add(new Marker(transform.position, transform.rotation));
    }

    public Marker GetFirstMarkerAtDistance(Vector3 position, float distance)
    {
        int markersToDelete = 0;

        for (int markerIndex = 0; markerIndex < markerList.Count; markerIndex++)
        {
            if (Vector3.Distance(position, markerList[markerIndex].position) > distance)
            {
                markersToDelete++;
                continue;
            }
            else
            {
                Marker toReturn = new Marker(markerList[markerIndex].position, markerList[markerIndex].rotation);
                markerList.RemoveRange(0, markersToDelete);
                return toReturn;
            }
        }
        return null;
    }

}
