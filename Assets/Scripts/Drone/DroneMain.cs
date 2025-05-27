using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class DroneMain : MonoBehaviour
{
    [Header ("___ Settings : drone ___")]
    [SerializeField] private ParticleSystem particles;

    private int _id = 0;
    private float _speed = 1f;
    private bool _Stopped = false;
    
    private int _resourceCount = 0;
    private Transform _target;
    private Sklad sklad;

    private bool _blockSecondMethod = false;

    private void Start() {
        if(_target == null) {
            float beforeSearch = Random.Range(0f, 1f);
            Invoke(nameof(SearchForTarget), beforeSearch);
        }
    }

#region Search target

    private void SearchForTarget() {
        if(_resourceCount != 0) {
            _target = sklad.GetPointById(_id);
        } else {
            ResourceCollider[] targets = GameObject.FindObjectsByType<ResourceCollider>(FindObjectsSortMode.None);
            List<GameObject> potentialTargets = new List<GameObject>();
            GameObject newTarget = null;

            for (int i = 0; i < targets.Length; i++) {
                if (!targets[i].isTargeted) {
                    potentialTargets.Add(targets[i].gameObject);
                }
            }

            float minDistance = 100;

            for (int i = 0; i < potentialTargets.Count; i++) {
                float dist = Vector3.Distance(transform.position, potentialTargets[i].transform.position);
                if (dist < minDistance) {
                    minDistance = dist;
                    newTarget = potentialTargets[i];
                }
            }

            if (newTarget != null) {
                newTarget.GetComponent<ResourceCollider>().isTargeted = true;
                _target = newTarget.transform;
            } else {
                float beforeSearch = Random.Range(0f, 1f);
                Invoke(nameof(SearchForTarget), beforeSearch);
            }
        }

        _blockSecondMethod = false;
    }

#endregion

#region Movement

    private void Update()
    {
        if(_target != null && !_Stopped) {
            var step =  _speed * Time.deltaTime;
            
            Vector3 desiredDirection = (_target.position - transform.position).normalized;
            Vector3 avoidance = Vector3.zero;

            RaycastHit hit;
            float castRadius = 1.5f;
            float castDistance = 3.5f;

            if (Physics.SphereCast(transform.position, castRadius, desiredDirection, out hit, castDistance)) {
                Vector3 right = Vector3.Cross(Vector3.up, desiredDirection);
                avoidance = Vector3.Cross(hit.normal, Vector3.up) * 2f;
            }

            Vector3 finalDirection = (desiredDirection + avoidance).normalized;
            transform.position += finalDirection * _speed * Time.deltaTime;

            if(Vector3.Distance(transform.position, _target.position) < 0.01f)
            {
                if(_resourceCount != 0) { Unload(); }
                if(_resourceCount == 0) { Load(); }
            }

            Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        } else {
            var temp = sklad.GetPointById(_id);
            Quaternion targetRotation = Quaternion.LookRotation(temp.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

#endregion

#region Load / Unload resources

    private void Load() {
        if(!_blockSecondMethod) {

            _resourceCount = Random.Range(1, 3);

            var zeroScale = new Vector3(0, 0, 0);
            float beforeSearch = Random.Range(0f, 1f) + 2f; // те две секунды, что были в ТЗ

            Invoke(nameof(SearchForTarget), beforeSearch);
            _target.DOScale(zeroScale, beforeSearch);

            _blockSecondMethod = true;
        }
    }

    private void Unload() {
        if(!_blockSecondMethod) {
            particles.Play();

            if(sklad != null) {
                sklad.GetResource(_resourceCount);
            }
            _resourceCount = 0;

            _target = null;
            float beforeSearch = Random.Range(0f, 1f);
            Invoke(nameof(SearchForTarget), beforeSearch);
            
            _blockSecondMethod = true;
        }
    }

#endregion

#region Set id and team

    internal void SetId(int index, Sklad skladManager) {
        _id = index;
        sklad = skladManager;
    }

#endregion

#region Speed

    internal void SetSpeed(float newSpeedLimit) {
        _speed = newSpeedLimit;
    }

#endregion
}
