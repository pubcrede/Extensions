﻿//-----------------------------------------------------------------------
// <copyright file="FileSearcherFull.cs" company="Genesys Source">
//      Copyright (c) 2017 Genesys Source. All rights reserved.
// 
//      All rights are reserved. Reproduction or transmission in whole or in part, in
//      any form or by any means, electronic, mechanical or otherwise, is prohibited
//      without the prior written consent of the copyright owner.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Genesys.Extensions;

namespace Genesys.Extras.IO
{
    /// <summary>
    /// Search a set of paths on a drive for a folder. 
    ///     Configure with auto search parent and children a certain levels in.
    /// </summary>
    [CLSCompliant(true)]
    public class FileSearcher
    {
        private List<DirectoryInfo> pathsField = new List<DirectoryInfo>();
        private List<FileInfo> foundFilesField = new List<FileInfo>();
        
        /// <summary>
        /// Paths
        /// </summary>
        public IEnumerable<DirectoryInfo> Paths { get { return pathsField; } }
        /// <summary>
        /// FoundFiles
        /// </summary>
        public List<FileInfo> FoundFiles { get { return foundFilesField; } }
        /// <summary>
        /// FileNameOrMask
        /// </summary>
        public string FileNameOrMask { get; private set; } = TypeExtension.DefaultString;
        /// <summary>
        /// ParentLevels
        /// </summary>
        public int ParentLevels { get; private set; } = TypeExtension.DefaultInteger;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public FileSearcher() : base() { FileNameOrMask = "*.*"; this.ParentLevels = 2; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        public FileSearcher(string pathToSearch, string fileOrMaskToSearch)
            : this()
        {
            pathsField.Add(new DirectoryInfo(pathToSearch));
            FileNameOrMask = fileOrMaskToSearch;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathsToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        public FileSearcher(List<string> pathsToSearch, string fileOrMaskToSearch)
            : this()
        {
            foreach (var item in pathsToSearch)
            {
                pathsField.Add(new DirectoryInfo(item));
            }
            FileNameOrMask = fileOrMaskToSearch;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        /// <param name="levelsUpToSearch"></param>
        public FileSearcher(string pathToSearch, string fileOrMaskToSearch, int levelsUpToSearch) : this(new List<string>() { pathToSearch }, fileOrMaskToSearch, levelsUpToSearch) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pathsToSearch"></param>
        /// <param name="fileOrMaskToSearch"></param>
        /// <param name="levelsUpToSearch"></param>
        public FileSearcher(List<string> pathsToSearch, string fileOrMaskToSearch, int levelsUpToSearch = 2) 
            : this(pathsToSearch, fileOrMaskToSearch)
        {
            DirectoryInfo currentPath;
            ParentLevels = levelsUpToSearch;
            // Add new paths to search
            foreach (var item in this.pathsField.ToList())
            {
                currentPath = new DirectoryInfo(item.ToString());
                for (var Count = 0; Count < this.ParentLevels; Count++)
                {
                    currentPath = new DirectoryInfo(currentPath.Parent.FullName); // Break reference chain with new instance
                    pathsField.Add(new DirectoryInfo(currentPath.ToString()));                    
                }
            }
        }
        
        /// <summary>
        /// Search
        /// </summary>
        public List<FileInfo> Search()
        {
            var returnValue = new List<FileInfo>();

            foundFilesField = new List<FileInfo>();
            foreach (var Item in this.Paths)
            {
                foundFilesField.AddRange(Item.GetFiles(this.FileNameOrMask));
            }

            return FoundFiles;
        }
        
        /// <summary>
        /// Sets a drive for search, with validation that drive exists
        /// </summary>
        /// <param name="drive">Drive to search</param>
        /// <returns>Drive class for searching</returns>
        private DriveInfo DriveSet(string drive)
        {
            var returnValue = new DriveInfo(@"C:\\");

            // Validate and set
            if (Directory.Exists(drive))
            {
                returnValue = new DriveInfo(drive);
            }
            
            return returnValue;
        }

        /// <summary>
        /// Safe method for setting drive for search
        /// </summary>
        /// <param name="volumeLabel">Pulls drive by volume label</param>
        /// <returns>Drive that matches volume label</returns>
        private DriveInfo DriveGetByVolume(string volumeLabel)
        {
            var returnValue = new DriveInfo(@"C:\\");
            var drivesToSearch = DriveInfo.GetDrives();

            foreach (var Item in drivesToSearch)
            {
                if (Item.VolumeLabel == volumeLabel)
                {
                    returnValue = Item;
                    break;
                }
            }
            
            return returnValue;
        }
    }
}
