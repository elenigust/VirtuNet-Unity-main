using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using CesiumForUnity;
public class Map: MonoBehaviour
{
    public TMP_InputField inputField;
    public CesiumGeoreference cesiumGeoreference;
    public CesiumCameraController cesiumCameraController;
    public double heightFromGround = 400.0f;
    private string apiKey = "AIzaSyAYo_T-G2GIsPvRZDgWrOwRPIIVPUL6syo";
    private string urlLocation = "";
    private string urlElevation = "";
    private double lat;
    private double lon;
    private double elevation;
    private string latLon;
    private string address = "Metatopia+thessaloniki";



    // Start is called before the first frame update
    void Start()
    {
        cesiumGeoreference.originAuthority = CesiumGeoreferenceOriginAuthority.LongitudeLatitudeHeight;
        inputField.text = "Metatopia Thessaloniki";
        StartCoroutine(GetGoogleMapLocation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onEndString()
    {
        address = inputField.text;
        address = address.Replace(" ", "+");
        StartCoroutine(GetGoogleMapLocation());
    }


        IEnumerator GetGoogleMapLocation()
    {
        urlLocation = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address + "&key=" + apiKey;
        UnityWebRequest webRequest = UnityWebRequest.Get(urlLocation);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string requestText = webRequest.downloadHandler.text;
            int locationIndex = requestText.IndexOf("\"location\"");
            if (locationIndex != -1 && requestText.IndexOf("\"lat\"") > -1 && requestText.IndexOf("\"lng\"") > -1)
            {
                int indexOfLat = requestText.IndexOf("\"lat\"");
                int startIndexLat = requestText.IndexOf(':', indexOfLat) + 1;
                int endIndexLat = requestText.IndexOf(',', startIndexLat);
                string latString = requestText.Substring(startIndexLat, endIndexLat - startIndexLat);
                if (double.TryParse(latString, out lat)) { }

                int index0fLng = requestText.IndexOf("\"lng\"");
                int startIndexLng = requestText.IndexOf(':', index0fLng) + 1;
                int endIndexLng = requestText.IndexOf('}', startIndexLng);
                string lngString = requestText.Substring(startIndexLng, endIndexLng - startIndexLng);
                if (double.TryParse(lngString, out lon)) { }
                latLon = latString +"," + lngString;
                StartCoroutine(GetGoogleMapElevation());
            }
            else
            {
                Debug.Log("WWW ERROR: " + webRequest.error);
            }

        }
    }

    IEnumerator GetGoogleMapElevation()
    {
        urlElevation = "https://maps.googleapis.com/maps/api/elevation/json?locations=" + latLon + "&key=" + apiKey;
        UnityWebRequest webRequest = UnityWebRequest.Get(urlElevation);
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("WWW ERROR: " + webRequest.error);
        }
        else
        {
            string requestText = webRequest.downloadHandler.text;

            if (requestText.IndexOf("\"elevation\"") > -1)
            {
                int elevationIndex = requestText.IndexOf("\"elevation\"");
                int colonIndex = requestText.IndexOf(":", elevationIndex);
                int commaIndex = requestText.IndexOf(",", colonIndex); // Updated to search for comma

                if (colonIndex >= 0 && commaIndex >= 0) // Check if indexes are valid
                {
                    string elevationSubstring = requestText.Substring(colonIndex + 1, commaIndex - colonIndex - 1);
                    elevationSubstring = elevationSubstring.Trim();
                    if (double.TryParse(elevationSubstring, out elevation)) { }
                }
                else
                {
                    Debug.Log("Colon or comma not found in response text.");
                }
            }
            cesiumGeoreference.SetOriginLongitudeLatitudeHeight(lon, lat, elevation + heightFromGround);
            cesiumCameraController.transform.position = Vector3.zero;
            cesiumCameraController.transform.rotation = Quaternion.Euler(90.0f, 0, 0);
        }
    }
}

