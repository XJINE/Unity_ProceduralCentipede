using UnityEngine;

public class CentipedeGenerator : MonoBehaviour
{
    #region Field

    public int        count      = 20;
    public Vector2Int partsCount = new Vector2Int(8, 30);
    public Vector2    partsScale = new Vector2(0.5f, 3f);
    public Vector2    speed      = new Vector2(2, 5);
    public Color[]    colors;

    public Mesh bodyMesh;

    #endregion Field

    void Start()
    {
        for (int i = 0; i < this.count; i++)
        {
            ProceduralCentipede centipede = this.gameObject.AddComponent<ProceduralCentipede>();
            centipede.partsCount = Random.Range(this.partsCount.x, this.partsCount.y);
            centipede.partsScale = Random.Range(this.partsScale.x, this.partsScale.y);
            centipede.speed      = Random.Range(this.speed.x,      this.speed.y);
            centipede.mesh       = bodyMesh;
            centipede.color      = this.colors[Random.Range(0, this.colors.Length)];
            centipede.Initialize();
        }
    }
}