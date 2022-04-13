// -------------------------------------------------------------------------------------------------
// <copyright file="PackageToExcelWriter.cs" company="RHEA System S.A.">
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
    using ClosedXML.Excel;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    /// <summary>
    /// Parses the project file and extracts the referenced nuget packages
    /// </summary>
    public class PackageToExcelWriter
    {
        /// <summary>
        /// Writes specific SRF data to an Excel file
        /// </summary>
        /// <param name="packages">
        /// The project files to parse
        /// </param>
        /// <param name="fileLocation">
        /// The location where the file should be written to (folder + filename)
        /// </param>
        /// <returns>
        /// <see cref="true"/> if succeeded, otherwise false
        /// </returns>
        public static void WriteSRF(IEnumerable<Package> packages, string fileLocation)
        {
            using (var workbook = new XLWorkbook())
            {
                AddLibrariesSheet(packages, workbook);

                AddNugetSheet(packages, workbook);

                workbook.SaveAs(fileLocation);
            }

        }

        /// <summary>
        /// Add a sheet that contains all Library information to an <see cref="XLWorkBook"/>
        /// </summary>
        /// <param name="packages">The <see cref="Package"/>s to get data from</param>
        /// <param name="workbook">The <see cref="XLWorkbook"/></param>
        private static void AddLibrariesSheet(IEnumerable<Package> packages, XLWorkbook workbook)
        {
            var nugetPackageData =
                packages
                .Select(x => new
                {
                    Id = x.Name,
                    Version = x.Version,
                    License = x.License,
                    LicenseUrl = x.LicenseUrl,
                    ProjectName = x.ProjectTitle,
                    ProjectUrl = x.ProjectUrl
                }
                )
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Version)
                .ThenBy(x => x.ProjectName);


            var pivotWorksheet = workbook.Worksheets.Add("SRF Annex A - Libraries");
            var dataTable = new DataTable();
            dataTable.Columns.Add("library", typeof(string));
            dataTable.Columns.Add("license", typeof(string));

            var projects = nugetPackageData.Select(x => x.ProjectName).Distinct().OrderBy(x => x);
            foreach (var columnName in projects)
            {
                var column = dataTable.Columns.Add(columnName, typeof(string));
                column.DefaultValue = "-";
            }

            var groups = nugetPackageData.GroupBy(x => new { x.Id, x.Version, x.License }, x => x.ProjectName);

            foreach (var group in groups)
            {
                var dataRow = dataTable.NewRow();
                dataRow["library"] = group.Key.Id;
                dataRow["license"] = group.Key.License;

                foreach (var columnName in group)
                {
                    dataRow[columnName] = group.Key.Version;
                }

                dataTable.Rows.Add(dataRow);
            }

            var table = pivotWorksheet.Cell(1, 1).InsertTable(dataTable, "Libraries", true);
            pivotWorksheet.Rows().AdjustToContents();
            pivotWorksheet.Columns().AdjustToContents();


        }

        /// <summary>
        /// Add a sheet that contains all Nuget information to an <see cref="XLWorkBook"/>
        /// </summary>
        /// <param name="packages">The <see cref="Package"/>s to get data from</param>
        /// <param name="workbook">The <see cref="XLWorkbook"/></param>
        private static void AddNugetSheet(IEnumerable<Package> packages, XLWorkbook workbook)
        {
            var nugetPackageData =
                packages
                .Select(x => new
                {
                    Id = x.Name,
                    Version = x.Version,
                    License = x.License,
                    LicenseUrl = x.LicenseUrl,
                    ProjectName = x.ProjectTitle,
                    ProjectUrl = x.ProjectUrl
                }
                )
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Version)
                .ThenBy(x => x.ProjectName);

            var nugetWorksheet = workbook.Worksheets.Add("NuGet Packages");
            var table = nugetWorksheet.Cell(1, 1).InsertTable(nugetPackageData.AsEnumerable(), "NugetPackages", true);
            nugetWorksheet.Rows().AdjustToContents();
            nugetWorksheet.Columns().AdjustToContents();

            foreach (var wsrow in nugetWorksheet.Rows().Skip(1))
            {
                if (!string.IsNullOrWhiteSpace(wsrow.Cell("D").Value.ToString()))
                {
                    wsrow.Cell("D").Hyperlink = new XLHyperlink(wsrow.Cell("D").Value.ToString());
                }

                if (!string.IsNullOrWhiteSpace(wsrow.Cell("F").Value.ToString()))
                { 
                    wsrow.Cell("F").Hyperlink = new XLHyperlink(wsrow.Cell("F").Value.ToString());
                }
            }
        }
    }
}
