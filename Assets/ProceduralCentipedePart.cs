using UnityEngine;

public partial class ProceduralCentipede
{
    [System.Serializable]
    public class Part
    {
        #region Field

        public Vector2    position;
        public Quaternion rotation;
        public Color      color;
        public float      scale;
        public float      moves;
        public bool       even;

        #endregion Field

        #region Method

        public bool Collide(Part target)
        {
            Vector2 diff   = this.position - target.position;
            float   radius = this.scale + target.scale;
                    radius = radius * radius;
            float   square = diff.x * diff.x + diff.y * diff.y;

            return radius > square;
        }

        public void Draw(Mesh mesh)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawMesh(mesh, position, rotation, new Vector3(scale * 6f, scale, scale * 0.5f));
            Gizmos.color = color;
            Gizmos.DrawSphere(position, scale);
        }

        #endregion Method
    }
}