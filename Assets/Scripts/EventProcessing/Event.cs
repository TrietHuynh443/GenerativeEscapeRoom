namespace EventProcessing
{
    public interface IEvent 
    {

    }

    public class OnCreateNewModelEvent : IEvent
    {
        public string Prompt { get; set; } = "monkey";
    }
}