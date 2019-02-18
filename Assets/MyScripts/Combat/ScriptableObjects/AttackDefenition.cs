using UnityEditor.Experimental.UIElements;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")]
public class AttackDefenition : ScriptableObject
{
    public float Cooldown;
    public float Range;
    public float minDamage;
    public float maxDamage;
    public float criticalMultyplier;
    public float criticalChance;
    
    public Attack CreateAttack(CharacterStats wielderStats, CharacterStats defenderStats)
    {
        float coreDamage = wielderStats.GetDamage();
        coreDamage += Random.Range(minDamage, maxDamage);

        bool isCritical = Random.value < criticalChance;
        if (isCritical)
            coreDamage *= criticalMultyplier;
        if (defenderStats != null)
            coreDamage -= defenderStats.GetResistance();

        return new Attack((int)coreDamage, isCritical);
    }


}

