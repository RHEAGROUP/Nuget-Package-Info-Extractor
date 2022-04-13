// -------------------------------------------------------------------------------------------------
// <copyright file="NuGetReaderTestFixture.cs" company="RHEA System S.A.">
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

namespace NetProjectPackageExtractor.Tests
{
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NetProjectPackageExtractor;

    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="NuGetReader"/> class.
    /// </summary>
    [TestFixture]
    public class NuGetReaderTestFixture
    {
        private NuGetReader nuGetReader;

        [SetUp]
        public void SetUp()
        { 
            this.nuGetReader = new NuGetReader();
        }

        [Test]
        public void Verify_that_nusepc_details_can_be_read_and_packages_are_update()
        {
            var package = new Package 
            { 
                Name = "Microsoft.NET.Test.Sdk",
                Version = "16.11.0"
            };

            var packages = new List<Package>() { package };

            this.nuGetReader.Update(packages);

            Assert.That(package.LicenseUrl, Is.EqualTo("https://aka.ms/deprecateLicenseUrl"));
        }

        [Test]
        public void Verify_that_nusepc_details_can_be_read_and_packages_are_update_2()
        {
            var package = new Package
            {
                Name = "cdp4common-ce",
                Version = "8.1.0"
            };

            var packages = new List<Package>() { package };

            this.nuGetReader.Update(packages);

            Assert.That(package.LicenseUrl, Is.EqualTo("https://licenses.nuget.org/LGPL-3.0-only"));
        }

    }
}
