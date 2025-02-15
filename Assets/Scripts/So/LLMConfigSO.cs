using System.Collections;
using System.Collections.Generic;
using HttpCommand;
using UnityEngine;

[CreateAssetMenu(fileName = "LLMConfigSO", menuName = "Configs/LLMConfigSO", order = 1)]
public class LLMConfigSO : ScriptableObject
{
    public string ApiKey;
    public ELLMApiType ApiType;

    public string Url
    {
        get
        {
            switch (ApiType)
            {
                case ELLMApiType.OpenAi:
                    return "https://api.openai.com/v1/chat/completions";
                case ELLMApiType.Gemini:
                    return "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";
                case ELLMApiType.DeepSeek:
                    return "https://api.deepseek.com/chat/completions";
            }

            return "none";
        }
    }

    public LLMRequest Request
    {
        get
        {
            switch (ApiType)
            {
                case ELLMApiType.OpenAi:
                    return new OpenAiRequest()
                    {
                        Model = "gpt-3.5-turbo",
                        Messages = new ()
                        {
                            new Dictionary<string, string>
                            {
                                {"role", "developer"},
                                {"content", "You are a helpful assistant."}
                            }
                        }
                    };
                case ELLMApiType.Gemini:
                    return new GeminiRequest();
                case ELLMApiType.DeepSeek:
                    return new DeepSeekRequest();
            }

            return new OpenAiRequest();
        }
    }
}
public enum ELLMApiType
{
    OpenAi = 0,
    Gemini = 1,
    DeepSeek = 2,
}

