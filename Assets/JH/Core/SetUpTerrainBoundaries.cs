using JH.INTERACTABLES;
using UnityEditor;
using UnityEngine;

public class SetUpTerrainBoundaries : MonoBehaviour
{
    #region //Cached Components
    private PolygonCollider2D cameraBoundary = null;

    [Header("Player Colliders")]
    [SerializeField] private BoxCollider2D leftPlayerBoundary = null;
    [SerializeField] private BoxCollider2D rightPlayerBoundary = null;
    [SerializeField] private BoxCollider2D topPlayerBoundary = null;
    #endregion

    #region //Transition Boundaries
    [Header("Transition Boundary Variables")]
    [SerializeField] private float distanceFromCameraBoundary = 0.5f;
    [SerializeField] private float transitionBoundaryWidth = 0.25f;
    [SerializeField] private float transitionBoundaryHeight = 3f;
    #endregion
    

    #region //Position player bounds around camera bounds
    public void SetPlayerBounds()
    {
        cameraBoundary = GetComponent<PolygonCollider2D>();
        SetHorizontalBoundary(leftPlayerBoundary, true);
        SetHorizontalBoundary(rightPlayerBoundary, false);
        SetVerticalBoundary(topPlayerBoundary);
    }

    private void SetHorizontalBoundary(BoxCollider2D _collider, bool _isLeft)
    {
        int firstPoint = _isLeft ? 1 : 0;
        int secondPoint = _isLeft ? 2 : 3;

        Vector2 newSize = Vector2.zero;
        newSize.x = 1;
        newSize.y = cameraBoundary.points[firstPoint].y - cameraBoundary.points[secondPoint].y;
        _collider.size = newSize;

        Vector2 newPosition = Vector2.zero;
        float halfSize = newSize.x/2;
        newPosition.x = cameraBoundary.points[firstPoint].x + halfSize * (_isLeft ? -1 : 1);
        newPosition.y = (cameraBoundary.points[firstPoint].y + cameraBoundary.points[secondPoint].y)/2;
        _collider.transform.localPosition = newPosition;
    }

    private void SetVerticalBoundary(BoxCollider2D _collider)
    {
        Vector2 newSize = Vector2.zero;
        newSize.x = cameraBoundary.points[0].x - cameraBoundary.points[1].x;
        newSize.y = 1;
        _collider.size = newSize;

        Vector2 newPosition = Vector2.zero;
        float halfSize = newSize.y/2;
        newPosition.x = (cameraBoundary.points[0].x + cameraBoundary.points[1].x)/2;
        newPosition.y = cameraBoundary.points[0].y + halfSize;
        _collider.transform.localPosition = newPosition;
    }
    #endregion

    #region //Position transition bounds at fixed distance from camera bounds
    public void SetUpTransitionBoundaries()
    {
        cameraBoundary = GetComponent<PolygonCollider2D>();
        foreach(var transition in GetComponentsInChildren<SubLevelTransitioner>())
        {
            ResizeTransition(transition);
            RepositionTransition(transition);
        }
    }

    private void ResizeTransition(SubLevelTransitioner _transition)
    {
        var collider = _transition.GetComponent<BoxCollider2D>();
        if(_transition.IsHorizontalBoundary())
        {
            collider.offset = new Vector2(0, transitionBoundaryHeight/2);
            collider.size = new Vector2(transitionBoundaryWidth, transitionBoundaryHeight);
        }
        else
            collider.size = new Vector2(transitionBoundaryHeight, transitionBoundaryWidth);
    }

    private void RepositionTransition(SubLevelTransitioner _transition)
    {
        // Determine reference point on camera boundary
        int pointNumber = (_transition.IsLowerBoundary() ? 2 : 0); //bottom-left or top-right point
        Vector2 polyVector = cameraBoundary.points[pointNumber];

        //Determine distance from poly collider
        Vector2 distanceFromPolyVector = Vector2.one * distanceFromCameraBoundary;
        if(_transition.IsLowerBoundary())
            distanceFromPolyVector *= -1;

        // Determine New Position
        Vector2 newPosition = polyVector + distanceFromPolyVector;
        if(_transition.IsHorizontalBoundary()) newPosition *= Vector2.right;
        else newPosition *= Vector2.up;

        //Integrate Old Position
        Vector2 oldPosition = _transition.transform.localPosition;
        if(_transition.IsHorizontalBoundary()) newPosition.y = oldPosition.y;
        else newPosition.x = oldPosition.x;

        // Set new Collider position
        _transition.transform.localPosition = newPosition;
    }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(SetUpTerrainBoundaries))]
public class SetUpSceneBoundsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ButtonsToEditThisTerrain();
        ButtonsToEditAllTerrains();
    }

    #region //GUI Methods
    private void ButtonsToEditThisTerrain()
    {
        EditorGUILayout.LabelField(" ");
        EditorGUILayout.LabelField("Edit This Terrain Only", EditorStyles.boldLabel);
        if (GUILayout.Button("Position Player Bounds"))
        {
            var instance = (SetUpTerrainBoundaries)target;
            EditorUtility.SetDirty(target);
            instance.SetPlayerBounds();
            EditorUtility.ClearDirty(target);
        }
        if (GUILayout.Button("Position Transition Bounds"))
        {
            var instance = (SetUpTerrainBoundaries)target;
            EditorUtility.SetDirty(target);
            instance.SetUpTransitionBoundaries();
            EditorUtility.ClearDirty(target);
        }
        if (GUILayout.Button("Position All Bounds"))
        {
            var instance = (SetUpTerrainBoundaries)target;
            EditorUtility.SetDirty(target);
            instance.SetPlayerBounds();
            instance.SetUpTransitionBoundaries();
            EditorUtility.ClearDirty(target);
        }
    }

    private void ButtonsToEditAllTerrains()
    {
        EditorGUILayout.LabelField(" ");
        EditorGUILayout.LabelField("Edit All Terrains In Hierarchy", EditorStyles.boldLabel);
        if (GUILayout.Button("Position Player Bounds"))
        {
            foreach(var setup in FindObjectsOfType<SetUpTerrainBoundaries>())
            {
                EditorUtility.SetDirty(setup);
                setup.SetPlayerBounds();
                EditorUtility.ClearDirty(target);
            }
        }
        if (GUILayout.Button("Position Transition Bounds"))
        {
            foreach(var setup in FindObjectsOfType<SetUpTerrainBoundaries>())
            {
                EditorUtility.SetDirty(setup);
                setup.SetUpTransitionBoundaries();
                EditorUtility.ClearDirty(target);
            }
        }
        if (GUILayout.Button("Position All Bounds"))
        {
            foreach(var setup in FindObjectsOfType<SetUpTerrainBoundaries>())
            {
                EditorUtility.SetDirty(setup);
                setup.SetPlayerBounds();
                setup.SetUpTransitionBoundaries();
                EditorUtility.ClearDirty(target);
            }
        }
    }
    #endregion
}
#endif
