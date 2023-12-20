using UnityEngine;

public abstract class BossEtatBase
{
   public abstract void InitEtat(BossEtatManager boss);
   public abstract void UpdateEtat(BossEtatManager boss);
   public abstract void TriggerEnterEtat(BossEtatManager boss, Collider col);
}

