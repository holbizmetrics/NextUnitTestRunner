using NextUnit.Core.Asserts;
using NextUnit.Core.TestAttributes;
using NextUnit.HardwareContext.SystemInformation;
using System.Security;

namespace NextUnit.HardwareContext.Tests
{
    /// <summary>
    /// Tests the hardware context.
    /// </summary>
    public class HardwareContextTests
    {
        [Test]
        [Group(nameof(SysInfo))]
        public void AllKnownFoldersTest()
        {
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void AllocationGranularity()
        {
            //Assert.AreEqual<uint>(AllocationGranularity, SysInfo.AllocationGranularity);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void BankLabelsTest()
        {

        }

        [Test]
        [Group(nameof(SysInfo))]
        public void CapacityTest()
        {
            //Assert.AreEqual<ulong>(Capacity, SysInfo.Capacity);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void StackStraceTest()
        {
            Assert.AreEqual<string>(Environment.StackTrace, SysInfo.StackTrace);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDataWidthTest()
        {
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void CurrentDirectoryTest()
        {
            Assert.AreEqual<string>(Environment.CurrentDirectory, SysInfo.CurrentDirectory);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void DirectoryTest()
        {
            Assert.AreEqual<int>(Environment.CurrentManagedThreadId, SysInfo.CurrentManagedThreadId);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDrivesTest()
        {
            Assert.AreEqual<DriveInfo[]>(DriveInfo.GetDrives(), SysInfo.DriveInfos);
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetDiskDrivesTest()
        {
            SysInfo.GetDiskDrives();
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetUSBDevicesTest()
        {
            SysInfo.GetUSBDevices();
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetUserAccountsTest()
        {
            SysInfo.GetUserAccounts();
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetValueTest()
        {
            //SysInfo.GetValue();
        }

        [Group(nameof(SysInfo))]
        [SecurityCritical]
        public void GetWindowsProductKeyTest()
        {
            SysInfo.GetWindowsProductKey();
        }

        [Group(nameof(SysInfo))]
        [Test]
        public void GetUpTimesTest()
        {
            SysInfo.GetUpTime();
        }

        [Test]
        [Group(nameof(SysInfo))]
        public void GetWinRMInformationTest()
        {
            SysInfo.GetWinRMInformation();
        }
    }
}
