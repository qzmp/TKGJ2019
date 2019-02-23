using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField]
    private float _dashSpeed;

    [SerializeField]
    private float dashCooldown;

    private float lastDashTime;

    public bool canDash = false;

    private Rigidbody2D rigidbody;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        this.lastDashTime = float.NegativeInfinity;
        if (this.canDash)
        {
            AbilityDisplayController.Instance.ShowDashDisplay();
        }
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.player = GetComponent<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        AbilityDisplayController.Instance.SetDashDisplay((Time.time - this.lastDashTime) / this.dashCooldown);

        if (Input.GetKeyDown(KeyCode.Space) && IsDashReady())
        {
            DoDash();
        }
    }

    private bool IsDashReady()
    {
        return Time.time > this.lastDashTime + this.dashCooldown;
    }

    public void DoDash()
    {
        this.lastDashTime = Time.time;
        AbilityDisplayController.Instance.ActivateDashDisplay();
        //Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mouseDirection = (worldMousePosition - this.transform.position).normalized;
        Vector2 movementInput = this.player.GetMovementInput();
        float movementAngle = Vector2.SignedAngle(Vector2.up, movementInput);


        this.rigidbody.AddForce(Quaternion.Euler(0, 0, movementAngle) * Vector2.up * this._dashSpeed, ForceMode2D.Impulse);

    }
    public void TeachDash()
    {
        this.canDash = true;
        AbilityDisplayController.Instance.ShowDashDisplay();
    }
}
