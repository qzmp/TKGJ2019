using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAudioController _playerAudioController;
    [SerializeField] private bool isInLight;

    public bool IsInLight {
        get { return this.isInLight; }
        private set { this.isInLight = value; }
    }
    public LayerMask occludingLayers;

    private LightSource[] lightSourcesOnMap;
    private CircleCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        this.lightSourcesOnMap = FindObjectsOfType<LightSource>();
        this.collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfIsInLight();
    }


    private void CheckIfIsInLight()
    {
        this.IsInLight = false;
        for(int i = 0; i < this.lightSourcesOnMap.Length; i++)
        {
            this.IsInLight |= CheckIfIsInLight(this.lightSourcesOnMap[i]);
            if (isInLight)
            {
                _playerAudioController.PlayDetectedAudioSource();
            }
        }
    }

    private bool CheckIfIsInLight(LightSource source)
    {
        float distToPlayer = Vector3.Distance(source.transform.position, this.transform.position);
        if (source.on && distToPlayer < source.size)
        {
            Vector3 dirToLight = source.transform.position - this.transform.position;
            Vector3 rightVector = Vector3.Normalize(Quaternion.Euler(0, 0, 90) * dirToLight);
            Vector3 leftVector = -rightVector;
            Vector3 rightPoint = this.transform.position + rightVector * this.collider.radius;
            Vector3 leftPoint = this.transform.position + leftVector * this.collider.radius;

            Vector3 dirToRightPoint = rightPoint - source.transform.position;
            Vector3 dirToLeftPoint = leftPoint - source.transform.position;

            Debug.DrawRay(source.transform.position, dirToRightPoint);
            Debug.DrawRay(source.transform.position, dirToLeftPoint);
            Debug.DrawRay(source.transform.position, -dirToLight);


            if (!Physics2D.Raycast(source.transform.position, dirToRightPoint, distToPlayer, this.occludingLayers))
            {
                return true;
            }
            if (!Physics2D.Raycast(source.transform.position, dirToLeftPoint, distToPlayer, this.occludingLayers))
            {
                return true;
            }
            if (!Physics2D.Raycast(source.transform.position, -dirToLight, distToPlayer, this.occludingLayers))
            {
                return true;
            }

            
        }
        return false;
    }
}
