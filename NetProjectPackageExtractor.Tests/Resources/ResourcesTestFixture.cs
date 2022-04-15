// -------------------------------------------------------------------------------------------------
// <copyright file="ResourcesTestFixture.cs" company="RHEA System S.A.">
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

namespace NetProjectPackageExtractor.Tests.Resources
{
    using System;
    using System.Resources;

    using NetProjectPackageExtractor.Resources;

    using NUnit.Framework;

    [TestFixture]
    public class ResourcesTestFixture
    {
        [SetUp]
        public void Setup()
        {
            this.path = "NetProjectPackageExtractor.Resources.ascii-art.txt";
            this.resourceLoader = new ResourceLoader();
        }

        private string path;
        private ResourceLoader resourceLoader;

        [Test]
        public void LoadEmbeddedResource()
        {
            var resource = this.resourceLoader.LoadEmbeddedResource(this.path);
            Assert.IsNotNull(resource);
            Assert.IsNotEmpty(resource);
            Assert.Throws<ArgumentNullException>(() => this.resourceLoader.LoadEmbeddedResource(null));
            Assert.Throws<MissingManifestResourceException>(() => this.resourceLoader.LoadEmbeddedResource("thispathdoesnotexist"));
        }
    }
}
