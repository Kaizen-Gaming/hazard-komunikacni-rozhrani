using System;
using System.Globalization;
using System.IO;
using System.Threading;
using ValidationPilotServices.ConfigService;
using ValidationPilotServices.DataReader;
using Xunit;
using Xunit.Abstractions;

namespace ValidationPilotTests
{
    public class FullValidationTests : CoreTest
    {
        public FullValidationTests(ITestOutputHelper output) : base(output)
        {
        }

        [Theory]
        [InlineData("DataSource/28934929-V-2019061208-B-01")]
        [InlineData("DataSource/28934929-V-2019061208-K-01")]
        [InlineData("DataSource/28934929-V-2019061208-K-02")]
        [InlineData("DataSource/28934929-V-2019061208-L-01")]
        [InlineData("DataSource/28934929-V-2019061208-R-01")]
        [InlineData("DataSource/28934929-V-2019061208-T-01")]
        [InlineData("DataSource/28934929-V-2019061208-Z-01")]
        public void Validate_DataSource_ShouldSucceed(string packagePath)
        {
            var dataReaderService = SetupDataReaderService(packagePath);

            try
            {
                dataReaderService.StartValidationProcess();
            }
            catch
            {
                Output.WriteLine($"DataReaderService.Output:\n\n{dataReaderService.ValidationOutput}");
                throw;
            }

            Assert.True(dataReaderService.IsValid, $"DataReaderService failed because:\n{dataReaderService.ValidationOutput}");
        }

        [Theory]
        [InlineData("DataSource/30030030-V-2019012108-R-01")]
        public void Validate_DataSource_ShouldFailWithException(string packagePath)
        {
            var dataReaderService = SetupDataReaderService(packagePath);

            Assert.Throws<ArgumentException>(() => dataReaderService.StartValidationProcess());
        }

        [Theory]
        [InlineData("DataSource/28934929-V-2019063016-K-01")]
        [InlineData("DataSource/28934929-V-2019063016-K-02")]
        public void Validate_DataSource_ShouldFailWithNotValid(string packagePath)
        {
            var dataReaderService = SetupDataReaderService(packagePath);

            dataReaderService.StartValidationProcess();

            Output.WriteLine(dataReaderService.ValidationOutput);

            Assert.False(dataReaderService.IsValid);
        }

        private DataReaderService SetupDataReaderService(string packagePath)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            var packagePathInfo = new DirectoryInfo(packagePath);

            Assert.True(packagePathInfo.Exists, $"Package dir does not exist {packagePathInfo.FullName}");

            var metaDataCheckService = new MetaDataCheckService();
            var metaDataCheckSuccess = metaDataCheckService.Ini();

            Assert.True(metaDataCheckSuccess, $"MetaDataCheckService failed because {metaDataCheckService.ErrorMessage}");

            var packageReaderService = new PackageReaderService(packagePathInfo.FullName);
            var packageReaderSuccess = packageReaderService.Ini();

            Assert.True(packageReaderSuccess, $"PackageReaderService failed because {packageReaderService.ErrorMessage}");

            return new DataReaderService(packageReaderService);;
        }
    }
}
