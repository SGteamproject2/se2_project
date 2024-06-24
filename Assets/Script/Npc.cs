using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Player.Instance.Target_NPC = gameObject;
        DialogManager.Instance.Start_Dialog();
    }
}
