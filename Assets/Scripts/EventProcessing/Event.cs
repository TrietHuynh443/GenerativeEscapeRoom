using UnityEngine;

namespace EventProcessing
{
    public interface IEvent 
    {

    }

    public class OnCreateNewModelEvent : IEvent
    {
        public string Prompt { get; set; } = "monkey";
    }

    public class OnSubmisionSuccess : IEvent
    {
        public int Level { get; set; } = 1;
        public int Count { get; set; } = 0;
    }

    public class OnDraggingInteractableObjEvent : IEvent
    {
        public InteractableGameObject InteractableObj { get; set; } 
    }
}