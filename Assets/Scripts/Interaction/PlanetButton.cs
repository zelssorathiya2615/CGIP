// using UnityEngine;

// public class PlanetButton : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }




// using UnityEngine;

// public class PlanetButton : MonoBehaviour
// {
//     [Header("References")]
//     public PlanetSelectionManager selectionManager;
//     public Transform body;

//     [Header("Planet Information")]
//     public string displayName;
//     public string category;

//     [TextArea(3, 6)]
//     public string description;

//     public void Select()
//     {
//         if (selectionManager == null || body == null)
//         {
//             Debug.LogError("PlanetButton references are missing.", this);
//             return;
//         }

//         selectionManager.SelectBody(
//             body,
//             displayName,
//             category,
//             description
//         );
//     }
// }




using UnityEngine;

public class PlanetButton : MonoBehaviour
{
    [Header("References")]
    public PlanetSelectionManager selectionManager;
    public Transform body;

    [Header("Scientific Information")]
    public CelestialBodyInfo bodyInfo;

    public void Select()
    {
        if (selectionManager == null ||
            body == null ||
            bodyInfo == null)
        {
            Debug.LogError(
                "PlanetButton: Missing references or body information.",
                this
            );
            return;
        }

        selectionManager.SelectBody(body, bodyInfo);
    }
}
