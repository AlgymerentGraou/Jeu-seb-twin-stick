using UnityEngine;

public class FlambeauGroup : MonoBehaviour
{
    [Header("Flambeaux à allumer")]
    public LightToggle[] flambeaux;

    [Header("Portes à ouvrir")]
    public Door[] doors; // ← tableau au lieu d'une seule porte

    private bool _doorOpened = false;

    void Update()
    {
        if (!_doorOpened && AllFlambeauxLit())
        {
            // ouvre toutes les portes
            foreach (Door door in doors)
                door.Open();

            _doorOpened = true;

            // annule le timer d'extinction sur tous les flambeaux
            foreach (LightToggle flambeau in flambeaux)
                flambeau.AnnulerExtinction();
        }
    }

    bool AllFlambeauxLit()
    {
        foreach (LightToggle flambeau in flambeaux)
        {
            if (!flambeau.isLightOn)
                return false;
        }
        return true;
    }
}