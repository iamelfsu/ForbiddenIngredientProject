using UnityEditor;
using UnityEngine;

public class CarryManager : MonoBehaviour
{
    public enum HeldType {None, Knife, Body, Meatball, BodyPieceA, BodyPieceB, Leg, Hand, Burger, Head,CookedMeatBall}

    public static HeldType HeldItemType = HeldType.None;
    public static GameObject HeldItem = null;
    public static bool HoldingItem;
    public static object FirstPickupInRange;
}
