using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class TowerController : MonoBehaviour
{
    public enum SearchType
    {
        Closest, HighestHP, LowestHP, Strongest, Weakest
    }

    public BasicTowerSO TowerData;
    public SearchType searchType = SearchType.LowestHP;

    // 
    GameObject currentTarget;
    EnemyController currentTargetController;

    float eTime = 0;
    // read that this was much faster
    Vector3 v3zero = new Vector3(0, 0, 0);

    // TODO: Probably better to check range and hp in update than test range on a scheduler
    protected virtual void SearchTarget()
    {
        // Target destroyed, not an enemy controller, has no hitpoints
        if(currentTarget == null || currentTargetController == null || currentTargetController.EnemyData.HP <= 0)
        {
            List<GameObject> inRange = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
            for(int i = inRange.Count - 1; i >= 0; i--)
            {
                // Attempting sqr distance for performance
                float distance = Vector3.SqrMagnitude(inRange[i].transform.position - transform.position);
                //float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance > TowerData.Range * TowerData.Range)
                {
                    inRange.RemoveAt(i);
                }
            }

            switch (searchType)
            {
                case SearchType.HighestHP:
                    inRange.OrderByDescending((o) =>
                    {
                        EnemyController eCtrl = o.GetComponent<EnemyController>();
                        return eCtrl.HP;
                    });
                    break;
                case SearchType.LowestHP:
                    inRange.OrderBy((o) =>
                    {
                        EnemyController eCtrl = o.GetComponent<EnemyController>();
                        return eCtrl.HP;
                    });
                    break;
                case SearchType.Strongest:
                    inRange.OrderByDescending((o) =>
                    {
                        EnemyController eCtrl = o.GetComponent<EnemyController>();
                        return eCtrl.Armor;
                    });
                    break;
                case SearchType.Weakest:
                    inRange.OrderBy((o) =>
                    {
                        EnemyController eCtrl = o.GetComponent<EnemyController>();
                        if (eCtrl == null) return 0;
                        return eCtrl.Armor;
                    });
                    break;
                case SearchType.Closest:
                    // Fallthrough to default
                default:
                    // Closest by default
                    // Naieve: Get distance from tgt to end and pick closest
                    // Developed: Get distance from pathfinder progress
                    break;
            }

            if(inRange.Count > 0)
            {
                currentTarget = inRange.First();
                currentTargetController = currentTarget.GetComponent<EnemyController>();
            }

            return;

        }

    }

    // Use this for initialization
    void Start()
    {
        //DOTween.Init(true, false, LogBehaviour.ErrorsOnly);
        // Look for a new target every 3rd of a second
        InvokeRepeating("SearchTarget", 1f, 1.0f / 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        eTime += Time.deltaTime;
        if(eTime >= TowerData.CoolDown)
        {
            eTime -= TowerData.CoolDown;

            if(currentTarget != null && currentTargetController != null)
            {
                // Move to fire coroutine
                GameObject bullet = Instantiate(TowerData.ProjectileData.Projectile, this.transform);
                bullet.transform.position = this.transform.position;
                Tweener t = bullet.transform.DOMove(currentTarget.transform.position, TowerData.ProjectileData.TravelTime, false)
                    .SetEase(TowerData.ProjectileData.EaseType)
                    .OnComplete(() =>
                    {
                        // Remove instance - Is the scope correct?
                        //t = null;
                        bullet.active = false;
                        GameObject.Destroy(bullet);

                        currentTargetController.HP -= TowerData.ProjectileData.Damage;
                        if (currentTargetController.HP <= 0)
                        {
                            currentTarget.active = false;
                            GameObject.Destroy(currentTarget);
                            currentTarget = null;
                            currentTargetController = null;
                        }
                    });
            }
        }
    }



}
