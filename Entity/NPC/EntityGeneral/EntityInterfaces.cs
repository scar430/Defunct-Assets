using DefunctLib.UI;
using DefunctLib.Entity;
using UnityEngine;
using System.Collections.Generic;

namespace DefunctLib.EventSystems
{
    public interface ISavior
    {

    }

    public interface IKillable
    {
        float _health { get; set; }

        void SubtractHealth(EntityState entityState, float reduce);
        void AddHealth(EntityState entityState, float add);
    }

    public interface IPickupable
    {
        Item _item { get; set; }
        bool _pickupable { get; set; }

        void Pickup(Item item, InventoryManager inventoryManager);
    }

    public interface IHungry
    {

    }

    public interface IThirsty
    {

    }

    public interface IEdible
    {

    }

    public interface IDrinkable
    {

    }

    public interface IDestructible
    {
        void TearMesh(float damage, float radius, Collision contactPoint, MeshFilter effectedMesh);
    }

    public interface IMenu
    {
        GameObject _billBoard
        {
            get;
        }

        List<GameObject> _buttons
        {
            get;
        }
    }
}