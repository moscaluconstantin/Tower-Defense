using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
     private Transform target;
     private Enemy targetEnemy;

     [Header("General")]
     public float range;

     [Header("Use Bullets (default)")]
     public GameObject bulletPrefab;
     public float fireRate = 1f;
     private float fireCountdown = 0f;

     [Header("Use Laser")]
     public bool useLaser = false;
     public int damageOverTime = 30;
     public float slowAmount = 0.5f;

     public LineRenderer lineRenderer;
     public ParticleSystem impactEffect;

     [Header("Unity Setup Fields")]
     public string enemyTag = "Enemy";
     public Transform partToRotate;
     public float turnSpeed = 10f;

     public Transform firePoint;

     private void Start()
     {
          InvokeRepeating("UpdateTarget", 0f, 0.5f);
     }
     private void Update()
     {
          if (target == null)
          {
               if (useLaser && lineRenderer.enabled)
               {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
               }

               return;
          }

          LockOnTarget();

          if (useLaser)
          {
               Laser();
          }
          else
          {
               if (fireCountdown <= 0f)
               {
                    Shoot();
                    fireCountdown = 1f / fireRate;
               }

               fireCountdown -= Time.deltaTime;
          }
     }
     private void OnDrawGizmosSelected()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, range);
     }

     private void LockOnTarget()
     {
          Vector3 dir = target.position - transform.position;
          Quaternion lookRotation = Quaternion.LookRotation(dir);
          Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
          partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
     }
     private void UpdateTarget()
     {
          GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
          float shortestDistance = Mathf.Infinity;
          GameObject nearestEnemy = null;

          foreach (GameObject enemy in enemies)
          {
               float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
               if (distanceToEnemy < shortestDistance)
               {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
               }
          }

          if (nearestEnemy != null && shortestDistance < range)
          {
               target = nearestEnemy.transform;
               targetEnemy = target.GetComponent<Enemy>();
          }
          else
          {
               target = null;
          }
               
     }
     private void Shoot()
     {
          GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
          Bullet bullet = bulletGO.GetComponent<Bullet>();
          if (bullet != null)
               bullet.Seek(target);
     }
     private void Laser()
     {
          targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
          targetEnemy.Slow(slowAmount);

          if (!lineRenderer.enabled)
          {
               lineRenderer.enabled = true;
               impactEffect.Play();
          }
               

          lineRenderer.SetPosition(0, firePoint.position);
          lineRenderer.SetPosition(1, target.position);

          Vector3 dir = firePoint.position - target.position;

          impactEffect.transform.rotation = Quaternion.LookRotation(dir);
          impactEffect.transform.position = target.position + dir.normalized;

     }
}
