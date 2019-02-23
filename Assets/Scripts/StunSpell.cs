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

    public bool canStun;

    // Start is called before the first frame update
    void Start()
    {
        this._lastUse = float.NegativeInfinity;

        _player = GetComponent<Player>();
        if (this.canStun)
        {
            AbilityDisplayController.Instance.ShowStunDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AbilityDisplayController.Instance.SetStunDisplay((Time.time - this._lastUse) / this._stunCooldown);

        if (Input.GetAxis("Fire2") != 0 && canStun && IsStunReady())
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
    private bool IsStunReady()
    {
        return Time.time > this._lastUse + this._stunCooldown;
    }


    public void Stun(Vector3 point)
    {
        Collider2D[] stunnedEnemiesColliders = Physics2D.OverlapCircleAll(point, _stunRadius, LayerMask.GetMask("Enemy"));//.OverlapSphere(point, _stunRadius/*, LayerMask.GetMask("Enemy"), QueryTriggerInteraction.Collide*/);
        AbilityDisplayController.Instance.ActivateStunDisplay();
        this._lastUse = Time.time;

        Debug.Log(Time.time + ", ogluszonych: " + stunnedEnemiesColliders.Length);

        foreach (Collider2D collider in stunnedEnemiesColliders)
        {
            AIPath enemyAI = collider.GetComponent<AIPath>();
            Transform enemyTransform = collider.GetComponent<Transform>();

            enemyAI.canMove = false;
            enemyTransform.DORotate(enemyAI.transform.rotation.eulerAngles, _stunDuration).OnComplete(() => enemyAI.canMove = true);
        }
    }

    public void TeachStun()
    {
        this.canStun = true;
        AbilityDisplayController.Instance.ShowStunDisplay();
    }
}
