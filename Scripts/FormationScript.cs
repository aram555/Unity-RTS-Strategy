using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TRavljen.UnitFormation.Formations;
using TRavljen.UnitFormation;
using UnityEngine.AI;
using UnityEngine.UI;

public class FormationScript : MonoBehaviour
{

    #region Public Properties

    /// <summary>
    /// List of units in the scene
    /// </summary>
    public List<GameObject> units = new List<GameObject>();
    public int rectangleColumn;

    /// <summary>
    /// Specifies the layer mask used for mouse point raycasts in order to
    /// find the drag positions in world/scene.
    /// </summary>
    public LayerMask groundLayerMask;

    /// <summary>
    /// Specifies the line renderer used for rendering the mouse drag line
    /// that indicates the unit facing direction.
    /// </summary>
    public LineRenderer LineRenderer;

    /// <summary>
    /// Specifies the unit count that will be generated for the scene.
    /// May be adjusted in realtime.
    /// </summary>
    public int UnitCount;

    /// <summary>
    /// Specifies the unit spacing that will be used to generate formation
    /// positions.
    /// </summary>
    public int UnitSpacing;

    /// <summary>
    /// Specifies the <see cref="Text"/> used to represent the unit count
    /// selected by <see cref="UnitCountSlider"/>.
    /// </summary>

    /// <summary>
    /// Specifies the <see cref="Text"/> used to represent the unit spacing
    /// selected by <see cref="UnitSpacingSlider"/>.
    /// </summary>

    public GameObject UnitPrefab = null;

    #endregion

    #region Private Properties

    private IFormation currentFormation;

    private bool isDragging = false;

    private int unitCount; //=> UnitCount;
    private int unitSpacing => UnitSpacing;

    #endregion

    private void Start()
    {
        LineRenderer.enabled = false;
        SetUnitFormation(new RectangleFormation(rectangleColumn, unitSpacing));

        // Initial UI update
        UpdateUnitCountText();
        UpdateUnitSpacing();
    }

    void Update()
    {
        units = UnitSelections.instance.unitSelected;
        unitCount = units.Count;
        if (units.Count < unitCount)
        {
            for (int index = units.Count; index < unitCount; index++)
            {
                var gameObject = Instantiate(
                    UnitPrefab, transform.position, Quaternion.identity);
                units.Insert(index, gameObject);
            }

            ApplyCurrentUnitFormation();
        }
        else if (units.Count > unitCount)
        {
            for (int index = units.Count - 1; index >= unitCount; index--)
            {
                var gameObject = units[index];
                Destroy(gameObject);
                units.RemoveAt(index);
            }

            ApplyCurrentUnitFormation();
        }

        if (units.Count > 0)
        {
            HandleMouseDrag();
        }
    }

    private void HandleMouseDrag()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
            {
                LineRenderer.enabled = true;
                isDragging = true;

                LineRenderer.SetPosition(0, hit.point);
                LineRenderer.SetPosition(1, hit.point);
            }
        }
        else if (Input.GetKey(KeyCode.Mouse1) & isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
            {
                LineRenderer.SetPosition(1, hit.point);

            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && isDragging)
        {
            isDragging = false;
            LineRenderer.enabled = false;
            ApplyCurrentUnitFormation();
        }
    }

    private void ApplyCurrentUnitFormation()
    {
        var direction = LineRenderer.GetPosition(1) - LineRenderer.GetPosition(0);

        UnitsFormationPositions formationPos;

        // Check if mouse drag was NOT minor, then we can calculate angle
        // (direction) for the mouse drag.
        if (direction.magnitude > 0.8f)
        {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var newPositions = FormationPositioner.GetAlignedPositions(
                units.Count, currentFormation, LineRenderer.GetPosition(0), angle);

            formationPos = new UnitsFormationPositions(newPositions, angle);
        }
        else
        {
            var currentPositions = units.ConvertAll(obj => obj.transform.position);
            formationPos = FormationPositioner.GetPositions(
                currentPositions, currentFormation, LineRenderer.GetPosition(0));
        }

        for (int index = 0; index < units.Count; index++)
        {
            if (units[index].TryGetComponent(out NavMeshAgent agent))
                agent.destination = formationPos.UnitPositions[index];
        }
    }

    private void SetUnitFormation(IFormation formation)
    {
        currentFormation = formation;
        ApplyCurrentUnitFormation();
    }

    #region User Interactions

    public void LineFormationSelected() =>
        SetUnitFormation(new LineFormation(unitSpacing));

    public void CircleFormationSelected() =>
        SetUnitFormation(new CircleFormation(unitSpacing));

    public void TriangleFormationSelected() =>
        SetUnitFormation(new TriangleFormation(unitSpacing));

    public void RectangleFormationFirstConfigSelected() =>
        SetUnitFormation(new RectangleFormation(rectangleColumn, unitSpacing));

    public void RectangleFormationSecondConfigSelected() =>
        SetUnitFormation(new RectangleFormation(2, unitSpacing));

    public void UpdateUnitCountText()
    {
        //UnitCountText.text = "Unit Count: " + unitCount;
    }

    public void UpdateUnitSpacing()
    {
        //UnitSpacingText.text = "Unit Spacing: " + unitSpacing;

        if (currentFormation is LineFormation)
        {
            currentFormation = new LineFormation(unitSpacing);
        }
        else if (currentFormation is RectangleFormation rectangleFormation)
        {
            currentFormation = new RectangleFormation(
                rectangleFormation.ColumnCount, unitSpacing);
        }
        else if (currentFormation is CircleFormation)
        {
            currentFormation = new CircleFormation(unitSpacing);
        }
        else if (currentFormation is TriangleFormation)
        {
            currentFormation = new TriangleFormation(unitSpacing);
        }

        ApplyCurrentUnitFormation();
    }

    #endregion

}
