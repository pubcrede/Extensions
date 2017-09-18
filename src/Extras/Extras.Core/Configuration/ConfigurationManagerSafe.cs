//-----------------------------------------------------------------------
// <copyright file="ConfigurationManagerSafe.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
//      Licensed to the Apache Software Foundation (ASF) under one or more 
//      contributor license agreements.  See the NOTICE file distributed with 
//      this work for additional information regarding copyright ownership.
//      The ASF licenses this file to You under the Apache License, Version 2.0 
//      (the 'License'); you may not use this file except in compliance with 
//      the License.  You may obtain a copy of the License at 
//       
//        http://www.apache.org/licenses/LICENSE-2.0 
//       
//       Unless required by applicable law or agreed to in writing, software  
//       distributed under the License is distributed on an 'AS IS' BASIS, 
//       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  
//       See the License for the specific language governing permissions and  
//       limitations under the License. 
// </copyright>
//-----------------------------------------------------------------------
using System;
using Genesys.Extensions;
using System.IO;
using System.Xml.Linq;

namespace Genesys.Extras.Configuration
{
    /// <summary>
    /// Cross-platform emulator of System.Configuration.ConfigurationManager, for local .config files
    /// Target Projects: Universal Windows 10/8.1, Xamarin Forms, Windows Phone, Portable Class Library
    /// Usage: From the Application project, 1. Read .config XML from filesystem, 2. Pass xml into constructor as strings
    ///     var localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
    ///     localFolder = await localFolder.GetFolderAsync("App_Data");
    ///     var appSettingsFile = await localFolder.GetFileAsync("AppSettings.config");
    ///     var appSettingsXml = await Windows.Storage.FileIO.ReadTextAsync(appSettingsFile);
    ///     var connectStringsFile = await localFolder.GetFileAsync("ConnectionStrings.config");
    ///     var connectStringsXml = await Windows.Storage.FileIO.ReadTextAsync(connectStringsFile);
    ///     var configManager = new ConfigurationManagerSafe(appSettingsXml, connectStringsXml);
    /// </summary>
    [CLSCompliant(true)]
    public class ConfigurationManagerSafe : IConfigurationManager
    {
        /// <summary>
        /// All application settings in the referenced config file
        /// </summary>
        public AppSettingList AppSettings { get; } = new AppSettingList();

        /// <summary>
        /// All connection strings in the referenced config file
        /// </summary>
        public ConnectionStringList ConnectionStrings { get; } = new ConnectionStringList();

        /// <summary>
        /// ThrowException
        /// </summary>
        public bool ThrowException { get; set; } = TypeExtension.DefaultBoolean;

        /// <summary>
        /// StatusMessage
        /// </summary>
        public string StatusMessage { get; set; } = TypeExtension.DefaultString;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationManagerSafe() : base() { ThrowException = true; }

        /// <summary>
        /// Constructor that accepts AppSettings and ConnectionStrings XML data in string form
        /// Usage: From the Application project, 1. Read .config XML from filesystem, 2. Pass xml into constructor as strings
        ///     var localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
        ///     localFolder = await localFolder.GetFolderAsync("App_Data");
        ///     var appSettingsFile = await localFolder.GetFileAsync("AppSettings.config");
        ///     var appSettingsXml = await Windows.Storage.FileIO.ReadTextAsync(appSettingsFile);
        ///     var connectStringsFile = await localFolder.GetFileAsync("ConnectionStrings.config");
        ///     var connectStringsXml = await Windows.Storage.FileIO.ReadTextAsync(connectStringsFile);
        ///     var configManager = new ConfigurationManagerSafe(appSettingsXml, connectStringsXml);
        /// </summary>
        /// <param name="appSettings">Raw XML from AppSettings.config</param>
        /// <param name="connectionStrings">Raw XML from ConnectionStrings.config</param>
        public ConfigurationManagerSafe(string appSettings, string connectionStrings) : this()
        {
            AppSettings = new AppSettingList(appSettings);
            ConnectionStrings = new ConnectionStringList(connectionStrings);
        }

        /// <summary>
        /// Constructor that accepts Stream, for .NET Core/PCL/Universal apps
        /// </summary>
        /// <param name="configFile">Raw XML from Web.config, AppSettings.config and ConnectionStrings.config</param>
        public ConfigurationManagerSafe(Stream configFile) : this()
        {
            XDocument xdoc = configFile.ToXDocument();

            AppSettings = new AppSettingList(xdoc.ToString());
            ConnectionStrings = new ConnectionStringList(xdoc.ToString());
        }

        /// <summary>
        /// Constructor that accepts AppSettings and ConnectionStrings XML data in array form
        /// This method is to support ConfigurationManagerFull() construction
        /// </summary>
        /// <param name="appSettings">ConfigurationManager.AppSettings.ToArraySafe()</param>
        /// <param name="connectionStrings">ConfigurationManager.ConnectionStrings.ToArraySafe()</param>
        public ConfigurationManagerSafe(string[,] appSettings, string[,] connectionStrings) : this()
        {
            connectionStrings = connectionStrings ?? new string[0, 2];
            for (var itemCount = 0; itemCount < connectionStrings.GetLength(0); itemCount++)
            {
                ConnectionStrings.Add(connectionStrings[itemCount, 0], connectionStrings[itemCount, 1]);
            }
            appSettings = appSettings ?? new string[0, 2];
            for (var itemCount = 0; itemCount < appSettings.GetLength(0); itemCount++)
            {
                AppSettings.Add(appSettings[itemCount, 0], appSettings[itemCount, 1]);
            }
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of app setting to retrieve</param>
        /// <returns>App setting that matches the key</returns>
        public AppSettingSafe AppSetting(string key)
        {
            AppSettingSafe ReturnData = AppSettings.Find(x => x.Key == key).CastSafe<AppSettingSafe>();

            if (ThrowException && ReturnData.Value == TypeExtension.DefaultString)
            {
                throw new System.DataMisalignedException(String.Format("App Setting is missing or has an empty value. {0}", key));
            }

            return ReturnData;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public string AppSettingValue(string key)
        {
            return AppSetting(key).Value;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public ConnectionStringSafe ConnectionString(string key)
        {
            ConnectionStringSafe ReturnData = ConnectionStrings.Find(x => x.Key == key).CastSafe<ConnectionStringSafe>();

            if (ThrowException && ReturnData.Value == TypeExtension.DefaultString)
            {
                throw new System.DataMisalignedException(String.Format("Connection string is missing or has an empty value. {0}", key));
            }

            return ReturnData;
        }

        /// <summary>
        /// Allows for return of setting when instantiated
        /// </summary>
        /// <param name="key">Key of item to retrieve the value</param>
        /// <returns>Value contents</returns>
        public string ConnectionStringValue(string key)
        {
            return ConnectionString(key).Value;
        }

    }
}
