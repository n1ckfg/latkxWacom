/*
 * free to use drawing app 
 * zbuffers copyright 2013
 * 
 * */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using PressureLib;

class PressurePoint
{
	public Vector2 sp;
	public Vector3 p;
	public PressurePoint next;
}

public class drawApp : MonoBehaviour
{
	public Shader shader;
	private Material penmat;
	
	private Mesh meshPen;
	
	public Mesh MeshPen{
		get{return meshPen;}
		set{meshPen = value;}
	}
	
	
	private List<Mesh> mesh1array = new List<Mesh> ();
	private Material wiresMat;
	private Vector3 ppoint;
	private float lineSize = 1.0f;
	private PressurePoint firstpoint;

	void Start ()
	{			
		if (PressureManager.instance==null) Debug.LogError("PressureManager not found, please drag the prefab to your hierarchy");
		meshPen = new Mesh ();
		penmat = new Material (shader);
		penmat.color = new Color (0, 0, 0, 0.5f);
		
	}
	
	private Vector3 previouspoint;
	
	void Update ()
	{
		float pressure = 1;
		if (Input.GetMouseButton(0) && PressureManager.normalPressure == 0) pressure = 1;
		else pressure = PressureManager.normalPressure;

		
		
		lineSize = pressure * 0.2f; 
				
		// if "C" then clear drawing
		if (Input.GetKey (KeyCode.Menu) || Input.GetKey (KeyCode.C)) {
			//Application.LoadLevel (0);
			meshPen.Clear();
			foreach(Mesh m in mesh1array){
				m.Clear ();
			}
			mesh1array.Clear();
			firstpoint = null;
			transform.rotation = Quaternion.identity;
			
		} else if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	
		// do not draw if cursor not moving
		if (Input.mousePosition != previouspoint) {
			if (Input.GetMouseButton (0)) {
			
				Vector3 newpoint = GetWorldPoint ();
			
				if (firstpoint == null) {
					firstpoint = new PressurePoint ();
					firstpoint.p = transform.InverseTransformPoint (newpoint);
					firstpoint.sp = Input.mousePosition;
				}
			
				if (ppoint != Vector3.zero) {
					Vector3 ls = transform.TransformPoint (ppoint);
					AddSegment (meshPen, pointsToQuad (ls, newpoint, lineSize), false);
				
					PressurePoint points = firstpoint;
										
					PressurePoint np = new PressurePoint ();
					np.p = transform.InverseTransformPoint (newpoint);
					np.sp = Input.mousePosition;
					points.next = np;

				}
			
				ppoint = transform.InverseTransformPoint (newpoint);
			} else {
				ppoint = Vector3.zero;
			}
		
		}
		
		previouspoint = Input.mousePosition;
		
		// test if mesh has more than 60000
		if (meshPen.vertices.Length > 60000) {
			Mesh m1 = DuplicateMesh (meshPen);
			mesh1array.Add (m1);
			meshPen.Clear ();
		}
		
		DrawMeshes ();
		
	}
	
	
	public void clear(){
			meshPen.Clear();
			foreach(Mesh m in mesh1array){
				m.Clear ();
			}
			mesh1array.Clear();
			firstpoint = null;
	}
	
	
	void DrawMeshes ()
	{
		// draw the current meshes
		Graphics.DrawMesh (meshPen, transform.localToWorldMatrix, penmat, 0);
		
		// draw the past meshes
		foreach (Mesh m in mesh1array) {	
			Graphics.DrawMesh (m, transform.localToWorldMatrix, penmat, 0);
		}		
	}
	
	Vector3[] pointsToQuad (Vector3 s, Vector3 e, float w)
	{
		w = w * 0.5f;
		Vector3[] q = new Vector3[4];
		
		// make a fake normal through segment
		// should be smoothed using bezier or mean...
		Vector3 n = Vector3.Cross (s, e);
		Vector3 l = Vector3.Cross (n, e - s);
		l.Normalize ();
		
		q [0] = transform.InverseTransformPoint (s + l * w);
		q [1] = transform.InverseTransformPoint (s + l * -w);
		q [2] = transform.InverseTransformPoint (e + l * w);
		q [3] = transform.InverseTransformPoint (e + l * -w);

		return q;
	}
	
	void AddSegment (Mesh m, Vector3[] quad, bool tmp)
	{
		int vl = m.vertices.Length;
			
		Vector3[] vs = m.vertices;
		if (!tmp || vl == 0)
			vs = resizeVertices (vs, 4);
		else
			vl -= 4;
			
		vs [vl] = quad [0];
		vs [vl + 1] = quad [1];
		vs [vl + 2] = quad [2];
		vs [vl + 3] = quad [3];
			
		int tl = m.triangles.Length;
			
		int[] ts = m.triangles;
		if (!tmp || tl == 0)
			ts = resizeTriangles (ts, 6);
		else
			tl -= 6;
		ts [tl] = vl;
		ts [tl + 1] = vl + 1;
		ts [tl + 2] = vl + 2;
		ts [tl + 3] = vl + 1;
		ts [tl + 4] = vl + 3;
		ts [tl + 5] = vl + 2;
			
		m.vertices = vs;
		m.triangles = ts;
		m.RecalculateBounds ();
	}
	
	Vector3 GetWorldPoint ()
	{
		return Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1.0f));
	}
	
	Vector3[] resizeVertices (Vector3[] ovs, int ns)
	{
		Vector3[] nvs = new Vector3[ovs.Length + ns];
		for (int i = 0; i < ovs.Length; i++)
			nvs [i] = ovs [i];
		return nvs;
	}
	
	int[] resizeTriangles (int[] ovs, int ns)
	{
		int[] nvs = new int[ovs.Length + ns];
		for (int i = 0; i < ovs.Length; i++)
			nvs [i] = ovs [i];
		return nvs;
	}
	


	
	public static Mesh DuplicateMesh (Mesh mesh)
	{
		Mesh newmesh = new Mesh ();

		newmesh.vertices = mesh.vertices;

		newmesh.triangles = mesh.triangles;

		newmesh.uv = mesh.uv;

		newmesh.normals = mesh.normals;

		newmesh.colors = mesh.colors;

		newmesh.tangents = mesh.tangents;
		return newmesh;
	}
	
	
	
}







