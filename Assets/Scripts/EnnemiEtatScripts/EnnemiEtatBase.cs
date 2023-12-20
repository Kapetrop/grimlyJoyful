using UnityEngine;

public abstract class EnnemiEtatBase
{
   public abstract void InitEtat(EnnemiEtatManager biome);
   public abstract void UpdateEtat(EnnemiEtatManager biome);
   public abstract void TriggerEnterEtat(EnnemiEtatManager biome, Collider col);
}
