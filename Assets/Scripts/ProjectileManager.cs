using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{

    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _playerPrefab;
    private Transform _firingPoint;
    private Collider2D[] ignoreColliders;

    public static ProjectileManager instance;
    void Awake()
    {
        // Set the static instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public Vector2 VectorDirectionAssignment()
    {

        if (PlayerControls.instance.GetHasFlipped())
        {
            return Vector2.left;
        }
        return Vector2.right;

    }

    public void IgnoreCollisionWithPlayer(GameObject projectile)
    {
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();

        foreach(Collider2D ignoreCollider in this.ignoreColliders)
        {
            Physics2D.IgnoreCollision(ignoreCollider, projectileCollider);
        }
    }
    public void FireProjectile()
    {
        _firingPoint = PlayerControls.instance.GetComponent<Transform>(); // getting the player's position

        GameObject projectile = Instantiate(this._projectilePrefab, this._firingPoint.position, this._firingPoint.rotation); // instantiating the prefab
        
        projectile.transform.parent = null; // detach the projectile from parent

        this.IgnoreCollisionWithPlayer(projectile);

        Vector2 fireDirection = this.VectorDirectionAssignment(); // get the player's direction it's facing :D

        projectile.GetComponent<GuitarProjectile>().Launch(fireDirection);
    
    }

    private void Start()
    {
        ignoreColliders = this._playerPrefab.GetComponentsInChildren<Collider2D>();
    }
}
