using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private Transform[] eyes;
    [SerializeField] private VisualEffect vfxGraph;

    [SerializeField] private float refereshRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateVFXGraph());
    }

    private IEnumerator UpdateVFXGraph()
    {
        WaitForSeconds wait = new WaitForSeconds(refereshRate);
        Mesh mesh1 = new Mesh();
        Mesh mesh2 = new Mesh();
        while(gameObject.activeSelf)
        {
            skinnedMesh.BakeMesh(mesh1);

            // This is weird, but bake mesh seems to add some weird garbage
            // that this gets rid of for some reason...
            Vector3[] verts = mesh1.vertices;
            mesh2.vertices = verts;


            vfxGraph.SetMesh("Mesh", mesh2);
            vfxGraph.SetVector3("Eye1", eyes[0].position);
            vfxGraph.SetVector3("Eye2", eyes[1].position);

            yield return wait;
        }
    }
}
