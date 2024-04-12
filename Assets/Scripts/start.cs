using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using log4net;
using log4net.Appender;
using UnityEngine;

// todo 로거 쓰레드 생성하고 비동기 파일 입출력
public class start : MonoBehaviour
{
    public static ILog iLog;
    // Start is called before the first frame update
    void Start()
    {
        FileInfo fileInfo = new FileInfo(Path.Combine(Application.persistentDataPath, "log4net4unityconfig.xml"));
        Debug.Log("파일유무" + fileInfo.Exists);
        Debug.Log("path" + Path.Combine(Application.persistentDataPath, "log4net4unityconfig.xml"));
        if (!fileInfo.Exists)
        {
            CreateConfig();
            fileInfo = new FileInfo(Path.Combine(Application.persistentDataPath, "log4net4unityconfig.xml"));
        }

        log4net.Config.XmlConfigurator.Configure(fileInfo);
        iLog = LogManager.GetLogger("Logger");


        iLog.Debug("debug 한글");
        iLog.Info("hello");
        iLog.Info("world");
        iLog.Warn("iam ");
        iLog.Error("cho");
        LogManager.Flush(0);

    }

    void CreateConfig()
    {
        // Create xml file
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes"));
        XmlNode root = xmlDoc.CreateElement("log4net");

        XmlNode fileAppender = SetFileAppender(xmlDoc);
        XmlNode unityAppender = SetUnityAppender(xmlDoc);
        XmlNode logger = SetLogger(xmlDoc);
        root.AppendChild(fileAppender);
        root.AppendChild(unityAppender);
        root.AppendChild(logger);
        xmlDoc.AppendChild(root);
        
        // todo 읽고쓰는경로 정해야한다
        xmlDoc.Save(Path.Combine(Application.persistentDataPath, "log4net4unityconfig.xml"));
    }

    XmlNode SetFileAppender(XmlDocument xmlDoc)
    {
        // Create file appender
        XmlNode fileAppender = xmlDoc.CreateElement("appender");
        XmlAttribute fileAppender_name = xmlDoc.CreateAttribute("name");
        fileAppender_name.Value = "FileAppenderXml";
        XmlAttribute fileAppender_type = xmlDoc.CreateAttribute("type");
        fileAppender_type.Value = "log4net.Appender.FileAppender";
        fileAppender.Attributes.Append(fileAppender_name);
        fileAppender.Attributes.Append(fileAppender_type);

        // File appender - file
        XmlNode fileAppender_file = xmlDoc.CreateElement("file");
        XmlAttribute fileAppender_file_type = xmlDoc.CreateAttribute("type");
        fileAppender_file_type.Value = "log4net.Util.PatternString";
        XmlAttribute fileAppender_file_value = xmlDoc.CreateAttribute("value");
        fileAppender_file_value.Value = Path.Combine(Application.persistentDataPath, "Log.xml");
        fileAppender_file.Attributes.Append(fileAppender_file_type);
        fileAppender_file.Attributes.Append(fileAppender_file_value);

        // File appender - appendToFile
        XmlNode fileAppender_appendToFile = xmlDoc.CreateElement("appendToFile");
        XmlAttribute fileAppender_appendToFile_value = xmlDoc.CreateAttribute("value");
        fileAppender_appendToFile_value.Value = "true";
        fileAppender_appendToFile.Attributes.Append(fileAppender_appendToFile_value);

        // File appender - layout
        XmlNode fileAppender_layout = xmlDoc.CreateElement("layout");
        XmlAttribute fileAppender_layout_type = xmlDoc.CreateAttribute("type");
        fileAppender_layout_type.Value = "log4net.Layout.XmlLayoutSchemaLog4j";
        fileAppender_layout.Attributes.Append(fileAppender_layout_type);


        // File appender - layout - locationInfo
        XmlNode fileAppender_layout_locationInfo = xmlDoc.CreateElement("locationInfo");
        XmlAttribute fileAppender_layout_locationInfo_value = xmlDoc.CreateAttribute("value");
        fileAppender_layout_locationInfo_value.Value = "true";
        fileAppender_layout_locationInfo.Attributes.Append(fileAppender_layout_locationInfo_value);
        fileAppender_layout.AppendChild(fileAppender_layout_locationInfo);

        // File appender - parameter
        XmlNode fileAppender_parameter = xmlDoc.CreateElement("param");
        XmlAttribute fileAppender_parameter_name = xmlDoc.CreateAttribute("name");
        fileAppender_parameter_name.Value = "Encoding";
        XmlAttribute fileAppender_parameter_value = xmlDoc.CreateAttribute("value");
        fileAppender_parameter_value.Value = "utf-8";
        fileAppender_parameter.Attributes.Append(fileAppender_parameter_name);
        fileAppender_parameter.Attributes.Append(fileAppender_parameter_value);

        // Bind
        fileAppender.AppendChild(fileAppender_file);
        fileAppender.AppendChild(fileAppender_appendToFile);
        fileAppender.AppendChild(fileAppender_layout);
        fileAppender.AppendChild(fileAppender_parameter);

        return fileAppender;
    }

    XmlNode SetUnityAppender(XmlDocument xmlDoc)
    {
        // Create unity appender
        XmlNode unityAppender = xmlDoc.CreateElement("appender");
        XmlAttribute unityAppender_name = xmlDoc.CreateAttribute("name");
        unityAppender_name.Value = "UnityAppender";
        XmlAttribute unityAppender_type = xmlDoc.CreateAttribute("type");
        unityAppender_type.Value = "UnityAppender";
        unityAppender.Attributes.Append(unityAppender_name);
        unityAppender.Attributes.Append(unityAppender_type);

        // UnityAppender - layout
        XmlNode unityAppender_layout = xmlDoc.CreateElement("layout");
        XmlAttribute unityAppender_layout_type = xmlDoc.CreateAttribute("type");
        unityAppender_layout_type.Value = "log4net.Layout.PatternLayout,log4net";
        unityAppender_layout.Attributes.Append(unityAppender_layout_type);

        // UnityAppender - layout - parameter
        XmlNode unityAppender_layout_parameter = xmlDoc.CreateElement("param");
        XmlAttribute unityAppender_layout_parameter_name = xmlDoc.CreateAttribute("name");
        unityAppender_layout_parameter_name.Value = "ConversionPattern";
        XmlAttribute unityAppender_layout_parameter_value = xmlDoc.CreateAttribute("value");
        unityAppender_layout_parameter_value.Value = "%d{ABSOLUTE} %-5p %c{1}:%L - %m%n";
        unityAppender_layout_parameter.Attributes.Append(unityAppender_layout_parameter_name);
        unityAppender_layout_parameter.Attributes.Append(unityAppender_layout_parameter_value);

        unityAppender_layout.AppendChild(unityAppender_layout_parameter);

        unityAppender.AppendChild(unityAppender_layout);


        return unityAppender;
    }

    XmlNode SetLogger(XmlDocument xmlDoc)
    {
        XmlNode logger = xmlDoc.CreateElement("logger");

        XmlAttribute logger_name = xmlDoc.CreateAttribute("name");
        logger_name.Value = "Logger";
        logger.Attributes.Append(logger_name);

        XmlNode logger_level = xmlDoc.CreateElement("level");
        XmlAttribute logger_level_value = xmlDoc.CreateAttribute("value");
        logger_level_value.Value = "ALL";
        logger_level.Attributes.Append(logger_level_value);

        XmlNode logger_ref_fileAppender = xmlDoc.CreateElement("appender-ref");
        XmlNode logger_ref_unityAppneder = xmlDoc.CreateElement("appender-ref");

        XmlAttribute fileRef = xmlDoc.CreateAttribute("ref");
        XmlAttribute unityRef = xmlDoc.CreateAttribute("ref");

        fileRef.Value = "FileAppenderXml";
        unityRef.Value = "UnityAppender";

        logger_ref_fileAppender.Attributes.Append(fileRef);
        logger_ref_unityAppneder.Attributes.Append(unityRef);

        logger.AppendChild(logger_level);
        logger.AppendChild(logger_ref_fileAppender);
        logger.AppendChild(logger_ref_unityAppneder);

        return logger;
    }

}
