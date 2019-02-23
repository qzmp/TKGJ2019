using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunSpell : MonoBehaviour
{
    [SerializeField]
    private float _stunDuration;

    [SerializeField]
    private float _stunRange;

    [SerializeField]
    private float _stunCooldown;

    [SerializeField]
    private float _stunRadius;

    private Player _player;
    private float _lastUse;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire2") != 0 && _lastUse + _stunCooldown < Time.time)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 128))
            {
                if (Vector3.Distance(transform.position, hit.point) < _stunRange)
                {
                    Stun(hit.point);
                }
            }
        }
    }

    public void Stun(Vector3 point)
    {
        Collider2D[] stunnedEnemiesColliders = Physics2D.OverlapCircleAll(point, _stunRadius, LayerMask.GetMask("Enemy"));//.OverlapSphere(point, _stunRadius/*, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide*/);

        Debug.Log(Time.time + ", ogluszonych: " + stunnedEnemiesColliders.Length);

        foreach (Collider2D collider in stunnedEnemiesColliders)
        {
            AILerp enemyAI = collider.GetComponent<AILerp>();
            Transform enemyTransform = collider.GetComponent<Transform>();

            enemyAI.canMove = false;
            enemyTransform.DORotate(enemyAI.transform.rotation.eulerAngles, _stunDuration).OnComplete(() => enemyAI.canMove = true);
        }
    }
}
