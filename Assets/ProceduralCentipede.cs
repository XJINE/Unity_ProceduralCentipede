using System.Collections.Generic;
using UnityEngine;

public partial class ProceduralCentipede : MonoBehaviour
{
    #region Field

    public List<Part> parts;
    public int        partsCount = 20;
    public float      partsScale = 0.2f;
    public float      speed      = 1f;
    public float      legSpeed   = 3f;
    public Color      color;

    public Vector2 targetInScreen;
    public Mesh    mesh;

    #endregion Field

    #region Method

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        UpdatePart(this.parts[0]);

        if (this.parts[0].position == targetInScreen)
        {
            UpdateTarget();
        }

        for (int i = this.parts.Count - 1; i >= 1; i--)
        {
            Part partA = this.parts[i];
            Part partB = this.parts[i - 1];

            UpdatePart(partA, partB);
        }
    }

    void OnDrawGizmos()
    {
        foreach (var part in this.parts)
        {
            part.Draw(this.mesh);
        }
    }

    public void Initialize()
    {
        this.parts = new List<Part>();

        for (int i = 0; i < this.partsCount; i++)
        {
            this.parts.Add(new Part()
            {
                scale    = this.partsScale,
                rotation = Random.rotation,
                even     = i % 2 == 0,
                color    = this.color,
            });
        }

        UpdateTarget();
    }

    public void UpdateTarget()
    {
        var screenPoint     = Camera.main.ViewportToScreenPoint(new Vector3(Random.value, Random.value, 0));
        this.targetInScreen = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void UpdatePart(Part partA, Part partB = null)
    {
        Vector2 targetPosition = partB == null ? this.targetInScreen : partB.position;

        if (partB == null || !partA.Collide(partB))
        {
            Vector2 nextPosition = Vector2.MoveTowards(partA.position, targetPosition, 0.1f);
            Vector2 direction    = (targetPosition - partA.position).normalized;

            partA.moves    += Vector3.Distance(partA.position, nextPosition);
            partA.position  = nextPosition;
            partA.rotation  = Quaternion.LookRotation(direction, Vector3.forward);
            partA.rotation *= Quaternion.AngleAxis(Mathf.Sin(((partA.even ? 1 : -1) + partA.moves) * this.legSpeed) * 45f, Vector3.up);
        }
    }

    #endregion Method
}