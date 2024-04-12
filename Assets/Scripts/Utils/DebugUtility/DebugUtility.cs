namespace CSC
{
    // File Path
    // Windows : C:\Users\<Username>\AppData\LocalLow\<CompanyName>/<ProductName>/~.txt
    // Mac : ~/Library/ApplicationSupport/<CompanyName>/<ProductName>/Log/~.txt
    // Android : 로컬 저장소 경로/Android/data/<PackageName>/files/Log/~.txt
    public class DebugUtility
    {
        public const string DEFAULT_TAG = "CSC";

        public static void Log(object content, bool writeToFile = false)
        {
            UnityEngine.Debug.Log(content);
            if (writeToFile)
                Logger.Append(DEFAULT_TAG, content.ToString(), LogLevel.Debug);
        }

        public static void Log(string tag, object content, bool writeToFile = false)
        {
            UnityEngine.Debug.Log(content);
            if (writeToFile)
                Logger.Append(tag, content.ToString(), LogLevel.Debug);
        }

        public static void LogWarning(object content, bool writeToFile = false)
        {
            UnityEngine.Debug.LogWarning(content);
            if (writeToFile)
                Logger.Append(DEFAULT_TAG, content.ToString(), LogLevel.Warning);
        }

        public static void LogWarning(string tag, object content, bool writeToFile = false)
        {
            UnityEngine.Debug.LogWarning(content);
            if (writeToFile)
                Logger.Append(tag, content.ToString(), LogLevel.Warning);
        }

        public static void LogError(object content, bool writeToFile = false)
        {
            UnityEngine.Debug.LogError(content);
            if (writeToFile)
                Logger.Append(DEFAULT_TAG, content.ToString(), LogLevel.Error);
        }

        public static void LogError(string tag, object content, bool writeToFile = false)
        {
            UnityEngine.Debug.LogError(content);
            if (writeToFile)
                Logger.Append(tag, content.ToString(), LogLevel.Error);
        }
    }
}