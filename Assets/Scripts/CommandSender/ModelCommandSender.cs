using HttpCommand;

namespace CommandSender
{
    public class ModelCommandSender : CommandSender<CreateModelRequest, CreateModelResponse>
    {
        public ModelCommandSender()
        {
            Url = $"{BaseUrl}/model";
        }
    }
}