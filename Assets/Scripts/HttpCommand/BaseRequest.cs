namespace HttpCommand
{
    public class BaseRequest
    {
        public virtual string ToQuery()
        {
            return string.Empty;
        }
    }

    public class UpdateRequest : BaseRequest
    {
        public virtual byte[] ToBody()
        {
            return null;
        }
    }
}