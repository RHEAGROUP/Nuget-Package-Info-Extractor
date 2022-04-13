// -------------------------------------------------------------------------------------------------
// <copyright file="ProjectFileExtractor.cs" company="RHEA System S.A.">
//
//   Copyright 2022 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace NetProjectPackageExtractor
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The purpose of the <see cref="ProjectFileExtractor"/> is to recursively iterate through a directory
    /// tree and return all the .csproj files in this directory tree as an List of <see cref="FileInfo"/> objects
    /// </summary>
    public class ProjectFileExtractor
    {
        /// <summary>
        /// Queries the directory structure 
        /// </summary>
        /// <param name="root"></param>
        /// <returns>
        /// An <see cref="IEnumerable{FileInfo}"/> of the found .csproj files
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<FileInfo> QueryProjectFiles(string root)
        {
            var directoryInfo = new DirectoryInfo(root);

            return directoryInfo.EnumerateFiles("*.csproj", SearchOption.AllDirectories);
        }
    }
}
