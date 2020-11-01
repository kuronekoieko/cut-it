using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MeshCutter
{
    /// <summary>
    /// Represent target for mesh cutter.
    /// </summary>
    //[RequireComponent(typeof(Rigidbody))]
    public class CutterTarget : MonoBehaviour
    {

        #region ### Events ###
        public System.Action<CutterTarget, GameObject[]> OnCutted;
        #endregion ### Events ###

        [Tooltip("Material for cutted surface.")]
        [Header("===========")]
        [Header("・親オブジェクトにrigidbodyを付ける")]
        [Header("・SkinnedMeshRendererと同階層にCutterTarget、コライダーを付ける")]
        [Header("2、SkinnedMeshRendererの場合")]
        [Header("・MeshRendererと同階層にCutterTarget、rigidbody、コライダーをを付ける")]
        [Header("1、MeshRendererの場合")]
        [Header("===========")]
        [SerializeField]
        private Material _cutMaterial;
        public Material CutMaterial
        {
            set { _cutMaterial = value; }
            get { return _cutMaterial; }
        }

        private Transform _mesh;
        public Transform Mesh
        {
            get
            {
                return _mesh;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                _mesh = value;
            }
        }

        /// <summary>
        /// In cutting, check position and normal to convert to local.
        /// </summary>
        public bool NeedsConvertLocal
        {
            get { return _mesh.GetComponent<SkinnedMeshRenderer>() != null; }
        }

        /// <summary>
        /// GameObject of mesh
        /// </summary>
        public GameObject GameObject
        {
            get { return _mesh.gameObject; }
        }

        /// <summary>
        /// Transform of mesh
        /// </summary>
        public Transform MeshTransform
        {
            get { return _mesh.transform; }
        }

        /// <summary>
        /// Mesh name
        /// </summary>
        public string Name
        {
            get { return _mesh.name; }
        }

        public Rigidbody Rigidbody => _rigidbody;
        private Rigidbody _rigidbody;
        public Renderer Renderer => _renderer;
        private Renderer _renderer;
        /// <summary>
        /// Will call back when cutting has been finished.
        /// </summary>
        public void Cutted(GameObject[] cuttedObjects)
        {
            if (OnCutted != null)
            {
                OnCutted.Invoke(this, cuttedObjects);
            }
        }

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            if (_cutMaterial == null) _cutMaterial = _renderer.material;
            _mesh = transform;
            _rigidbody = GetComponentInParent<Rigidbody>();
        }
    }
}
