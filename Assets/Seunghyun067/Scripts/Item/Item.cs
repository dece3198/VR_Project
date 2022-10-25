using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using System.Reflection;



public class Item : MonoBehaviour
{
    private ItemOption option;
    private XRGrabInteractable grabInter;

    private Dictionary<ItemOption, ItemComponent> itemComponents = new Dictionary<ItemOption, ItemComponent>();

    private void Awake()
    {
        grabInter = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        //grabInter?.hoverEntered.AddListener((HoverEnterEventArgs) => ((Hoverable)itemComponents[ItemOption.HOVER]).HoverOn());
        //grabInter?.hoverExited.AddListener((HoverEnterEventArgs) => ((Hoverable)itemComponents[ItemOption.HOVER]).HoverOff());
    }

    public void AddItemComponent(ItemOption _option, ItemComponent com)
    {
        itemComponents[_option] = com;
        option |= _option;
    }

    public T GetItemComponent<T>() where T : ItemComponent
    {             
        if (typeof(T) == typeof(Hoverable))
            return (T)itemComponents[ItemOption.HOVER];

        if (typeof(T) == typeof(CloseUpable))
            return (T)itemComponents[ItemOption.CLOSEUP];

        if (typeof(T) == typeof(SH.Interactionable))
            return (T)itemComponents[ItemOption.INTERACTION];

        return null;
    }

    public bool IsOption(ItemOption _option) => (option & _option) == _option;

    public void DeleteOption(ItemOption _option)
    {
        itemComponents.Remove(_option);
        option -= _option;

        if (option == ItemOption.HOVER)
        {
            ((Hoverable)itemComponents[ItemOption.HOVER]).HoverOff();
            option = ItemOption.NONE;
        }
    }

    public void CloseUp()
    {
        if ((option & ItemOption.CLOSEUP) != ItemOption.CLOSEUP)
            return;


    }
}
